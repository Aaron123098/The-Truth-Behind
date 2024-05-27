using Cainos.PixelArtTopDown_Basic;
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
                else if (adminDialogue.isPlayerOver && dialogueState == DialogueState.ThirdRundCompleted && AdminDialogue.triviaNmbr == 0)
                {
                    StartQuestions("L9Questions");
                }
                else if (adminDialogue.isPlayerOver && dialogueState == DialogueState.ThirdRundCompleted && AdminDialogue.triviaNmbr == 1)
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
        QuestionSetup questionSetup = FindAnyObjectByType<QuestionSetup>();

        bool thisQuestionActive = GetBoolQuestion(folder);

        if(thisQuestionActive)
        {
            questionsScreen.SetActive(true);
            setConfigurableVariables(folder, questionSetup);
            questionSetup.folderToSearch = folder;
            questionSetup.GetQuestionAssets(folder);
            questionSetup.ShowNextQuestion();
        }
        else
        {
            SellerDialogue sellerDialogue = FindAnyObjectByType<SellerDialogue>();
            AdminDialogue adminDialogue = FindAnyObjectByType<AdminDialogue>();

            if (sellerDialogue)
            {
                if (sellerDialogue.isPlayerOver)
                {
                    SellerDialogue.firstTriviaCompletedSeller = true;
                }

                if (adminDialogue.isPlayerOver)
                {
                    if (AdminDialogue.triviaNmbr == 0)
                    {
                        AdminDialogue.triviaNmbr = 1;
                    }
                    else if (AdminDialogue.triviaNmbr == 1)
                    {
                        AdminDialogue.triviaNmbr = 2;
                    }
                }

                FindAnyObjectByType<PropsAltar>().altarAv = true;
            }
            else
            {
                FindAnyObjectByType<GameManager>().SendToYardAfterWin();
            }
        }

        
    }

    public void setConfigurableVariables(string folder,  QuestionSetup questionSetup)
    {
        JSONSaveLoadSystem jsonFile = FindAnyObjectByType<JSONSaveLoadSystem>();

        switch (folder)
        {
            case "L1Questions":
                questionSetup.attemptsNmbr = 5;
                questionSetup.questionNmbr = jsonFile.srcNumbL1;
                questionSetup.help = jsonFile.helpL1;
                questionSetup.award = 10;
                questionSetup.timeLimit = 300;
                break;
            case "L2Questions":
                questionSetup.attemptsNmbr = 5;
                questionSetup.questionNmbr = jsonFile.srcNumbL2;
                questionSetup.help = jsonFile.helpL2;
                questionSetup.award = 10;
                questionSetup.timeLimit = 300;
                break;
            case "L3&4Questions":
                questionSetup.attemptsNmbr = 5;
                questionSetup.questionNmbr = 3;
                questionSetup.help = "";
                questionSetup.award = jsonFile.awardL3;
                questionSetup.timeLimit = 300;
                break;
            case "L5Questions":
                questionSetup.attemptsNmbr = 5;
                questionSetup.questionNmbr = 2;
                questionSetup.help = "";
                questionSetup.award = 10;
                questionSetup.timeLimit = jsonFile.timeLimitL5;
                break;
            case "L6Questions":
                questionSetup.attemptsNmbr = 5;
                questionSetup.questionNmbr = 2;
                questionSetup.help = "";
                questionSetup.award = jsonFile.awardL6;
                questionSetup.timeLimit = 300;
                break;
            case "L7Questions":
                questionSetup.attemptsNmbr = jsonFile.wrongLimitL7;
                questionSetup.questionNmbr = 2;
                questionSetup.help = "";
                questionSetup.award = 10;
                questionSetup.timeLimit = 300;
                break;
            case "L8Questions":
                questionSetup.attemptsNmbr = 5;
                questionSetup.questionNmbr = 2;
                questionSetup.help = "";
                questionSetup.award = jsonFile.awardL8;
                questionSetup.timeLimit = 300;
                break;
            case "L9Questions":
                questionSetup.attemptsNmbr = jsonFile.wrongLimitL9;
                questionSetup.questionNmbr = 2;
                questionSetup.help = "";
                questionSetup.award = 10;
                questionSetup.timeLimit = 300;
                break;
            case "L10Questions":
                questionSetup.attemptsNmbr = 5;
                questionSetup.questionNmbr = 2;
                questionSetup.help = "";
                questionSetup.award = 10;
                questionSetup.timeLimit = jsonFile.timeLimitL10;
                break;
            case "L11Questions":
                questionSetup.attemptsNmbr = 5;
                questionSetup.questionNmbr = 2;
                questionSetup.help = "";
                questionSetup.award = 10;
                questionSetup.timeLimit = jsonFile.timeLimitL11;
                break;
        }
    }

    private bool GetBoolQuestion(string folder)
    {
        JSONSaveLoadSystem jsonFile = FindAnyObjectByType<JSONSaveLoadSystem>();

        switch (folder)
        {
            case "L1Questions":
                return jsonFile.l1act;
            case "L2Questions":
                return jsonFile.l2act;
            case "L3&4Questions":
                return jsonFile.l34act;
            case "L5Questions":
                return jsonFile.l5act;
            case "L6Questions":
                return jsonFile.l6act;
            case "L7Questions":
                return jsonFile.l7act;
            case "L8Questions":
                return jsonFile.l8act;
            case "L9Questions":
                return jsonFile.l9act;
            case "L10Questions":
                return jsonFile.l10act;
            case "L11Questions":
                return jsonFile.l11act;
            default: return false;
        }
    }
}
