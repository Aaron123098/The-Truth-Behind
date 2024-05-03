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

    private SellerDialogue sellerDialogue;
    private GuideDialogue guideDialogue;

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
        BeforeFrstRun,
        AfterFrstRun,
        FrstRunCompleted,
        ScndRunCompleted,
        ThirdRundCompleted
    }

    public DialogueState dialogueState = DialogueState.BeforeFrstRun;

    // Start is called before the first frame update
    void Start()
    {
        sellerDialogue = FindAnyObjectByType<SellerDialogue>();
        guideDialogue = FindAnyObjectByType<GuideDialogue>();

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
            if(sellerDialogue.isPlayerOver && dialogueState == DialogueState.AfterFrstRun)
            {
                StartQuestions("L1Questions");
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
        QuestionSetup questionSetup = FindAnyObjectByType<QuestionSetup>();

        questionSetup.folderToSearch = folder;
        questionSetup.GetQuestionAssets(folder);
        questionSetup.ShowNextQuestion();
        questionsScreen.SetActive(true);
    }
}
