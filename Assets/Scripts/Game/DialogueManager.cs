using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DialogueManager : MonoBehaviour
{

    private Queue<string> sentences;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public GameObject dialoguePanel;

    public Animator animator;

    public static DialogueManager dialogueManager;

    public GameObject questionsScreen;

    public SellerDialogue sellerDialogue;
    private AdminDialogue adminDialogue;
    private Neigh1Dialogue neigh1Dialogue;
    private Neigh2Dialogue neigh2Dialogue;
    private GuideDialogue guideDialogue;
    private BossDialogue bossDialogue;

    private void Awake()
    {
        if (dialogueManager != null)
        {
            Destroy(dialogueManager);
        }
        else
        {
            dialogueManager = this;
        }
        DontDestroyOnLoad(this);
    }

    public enum DialogueState
    {
        BeforeFrstRun, //Diálogo introducción
        //RUN: Preguntas L3, L4
        AfterFrstRun, //Introducción de seller, L1
        FrstRunCompleted, //Introducción de seller, L1, L2 | Introducción vecinos, L5, L7
        //RUN: Preguntas L6
        ScndRunCompleted, //Con guía, L11
        //RUN: Preguntas L8
        ThirdRundCompleted //Introducción Admin, L9, L10
    }

    public DialogueState dialogueState = DialogueState.BeforeFrstRun;

    // Start is called before the first frame update
    void Start()
    {
        sellerDialogue = FindAnyObjectByType<SellerDialogue>();
        adminDialogue = FindAnyObjectByType<AdminDialogue>();
        neigh1Dialogue = FindAnyObjectByType<Neigh1Dialogue>();
        neigh2Dialogue = FindAnyObjectByType<Neigh2Dialogue>();
        guideDialogue = FindAnyObjectByType<GuideDialogue>();
        bossDialogue = FindAnyObjectByType<BossDialogue>();

        sentences = new Queue<string>();
        if (PlayerPrefs.HasKey("DialogueState"))
        {
            string auxiliarDialogueState = PlayerPrefs.GetString("DialogueState");
            System.Enum.TryParse(auxiliarDialogueState, out DialogueState loadDialogueState);
            dialogueState = loadDialogueState;
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {

        animator.SetBool("isOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            if (sellerDialogue)
            {
                if (sellerDialogue.isPlayerOver && dialogueState == DialogueState.AfterFrstRun)
                {
                    StartQuestions("L1Questions");
                }
                else if (sellerDialogue.isPlayerOver && dialogueState == DialogueState.FrstRunCompleted && !SellerDialogue.firstTriviaCompletedSeller)
                {
                    StartQuestions("L1Questions");
                }
                else if (sellerDialogue.isPlayerOver && dialogueState == DialogueState.FrstRunCompleted && SellerDialogue.firstTriviaCompletedSeller)
                {
                    StartQuestions("L2Questions");
                }
                else if (neigh1Dialogue.isPlayerOver && dialogueState == DialogueState.FrstRunCompleted)
                {
                    StartQuestions("L5Questions");
                }
                else if (neigh2Dialogue.isPlayerOver && dialogueState == DialogueState.FrstRunCompleted)
                {
                    StartQuestions("L7Questions");
                }
                else if (guideDialogue.isPlayerOver && dialogueState == DialogueState.ScndRunCompleted)
                {
                    StartQuestions("L11Questions");
                }
                else if (adminDialogue.isPlayerOver && dialogueState == DialogueState.ThirdRundCompleted && !AdminDialogue.firstTriviaCompletedAdmin)
                {
                    StartQuestions("L9Questions");
                }
                else if (adminDialogue.isPlayerOver && dialogueState == DialogueState.ThirdRundCompleted && AdminDialogue.firstTriviaCompletedAdmin)
                {
                    StartQuestions("L10Questions");
                }
            }
            else
            {
                if(dialogueState == DialogueState.AfterFrstRun || dialogueState == DialogueState.BeforeFrstRun)
                {
                    StartQuestions("L3&4Questions");
                }
                else if(dialogueState == DialogueState.FrstRunCompleted)
                {
                    StartQuestions("L6Questions");
                }
                else if(dialogueState == DialogueState.ScndRunCompleted)
                {
                    StartQuestions("L8Questions");
                }

            }
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        animator.SetBool("isOpen", false);
    }

    void StartQuestions(string folder)
    {
        questionsScreen.SetActive(true);
        QuestionSetup questionSetup = FindAnyObjectByType<QuestionSetup>();
        questionSetup.folderToSearch = folder;
        questionSetup.GetQuestionAssets(folder);
        questionSetup.ShowNextQuestion();
    }
}
