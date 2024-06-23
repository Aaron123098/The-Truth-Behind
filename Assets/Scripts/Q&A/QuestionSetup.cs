using Cainos.PixelArtTopDown_Basic;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static DialogueManager;

public class QuestionSetup : MonoBehaviour
{
    [SerializeField]
    private List<QuestionData> questions;
    private QuestionData currentQuestion;

    [SerializeField]
    private Image questionImg;
    [SerializeField]
    private TextMeshProUGUI questionText;
    [SerializeField]
    private AnswerButton[] answerButtons;

    [SerializeField]
    private int correctAnswerChoice;

    public GameObject questionScreen;
    public GameObject successScreen;
    public GameObject failScreen;

    public TextMeshProUGUI attemptsText;
    public string folderToSearch;
    public int totalFolderCount = 0;

    //Variables configurables
    public int attemptsNmbr;
    public int questionNmbr;
    public string help;
    public int award;
    public int timeLimit;

    public PropsAltar altar;

    private void Awake()
    {
        folderToSearch = "";
        altar = FindAnyObjectByType<PropsAltar>();
    }

    public void ShowNextQuestion()
    {
        attemptsText.text = "Fallos restantes: " + attemptsNmbr.ToString();
        if (questions.Count > totalFolderCount - questionNmbr)

        {
            SelectNewQuestion();
            SetQuestionValues();
            SetAnswerValues();
            SetTimer();
        }
        else
        {
            ShowSuccessScreen();
        }
        
    }

    private void ShowSuccessScreen()
    {
        questionScreen.SetActive(false);
        successScreen.SetActive(true);
    }

    public void ShowFailScreen()
    {
        questionScreen.SetActive(false);
        failScreen.SetActive(true);
    }

    public void DecreaseAttempts()
    {
        if (attemptsNmbr > 0)
        {
            attemptsNmbr--;
            attemptsText.text = "Fallos restantes: " + attemptsNmbr.ToString();
        }
        else
        {
            ShowFailScreen();
        }
    }

    public void GetQuestionAssets(string folder)
    {
        questions = new List<QuestionData>(Resources.LoadAll<QuestionData>(folder));
        totalFolderCount = questions.Count;
    }

    private void SelectNewQuestion()
    {
        int randomQuestionIndex = Random.Range(0, questions.Count);
        currentQuestion = questions[randomQuestionIndex];
        questions.RemoveAt(randomQuestionIndex);
    }

    private void SetQuestionValues()
    {
        questionImg.sprite = currentQuestion.questionImage;
        questionText.text = currentQuestion.questionText;
    }

    private void SetAnswerValues()
    {
        List<string> answers = RandomizeAnswers(new List<string>(currentQuestion.answers));

        for(int i = 0; i < answerButtons.Length; i++)
        {
            bool isCorrect = false;

            if(i == correctAnswerChoice)
            {
                isCorrect = true;
            }

            answerButtons[i].SetIsCorrect(isCorrect);
            answerButtons[i].SetAnswerText(answers[i]);
        }
    }

    private void SetTimer()
    {
        FindObjectOfType<Timer>().remainingTime = timeLimit;
    }

    private List<string> RandomizeAnswers(List<string> originalList)
    {
        bool correctAnswerChosen = false;

        List<string> newList = new List<string>();

        for(int i = 0; i < answerButtons.Length;i++)
        {
            int random = Random.Range(0, originalList.Count);

            if(random == 0 && !correctAnswerChosen)
            {
                correctAnswerChoice = i;
                correctAnswerChosen = true;
            }

            newList.Add(originalList[random]);
            originalList.RemoveAt(random);
        }

        return newList;
    }

    public void Retry()
    {
        GetQuestionAssets(folderToSearch);
        attemptsNmbr = 5;
        attemptsText.text = "Fallos restantes: " + attemptsNmbr.ToString();
        ShowNextQuestion();
        failScreen.SetActive(false);
        questionScreen.SetActive(true);
    }

    public void Continue()
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
                if(AdminDialogue.triviaNmbr == 0)
                {
                    AdminDialogue.triviaNmbr = 1;
                }
                else if(AdminDialogue.triviaNmbr == 1)
                {
                    AdminDialogue.triviaNmbr = 2;
                }
            }

            altar.altarAv = true;
        }
        else
        {
            FindAnyObjectByType<GameManager>().SendToYardAfterWin();
        }

        this.gameObject.SetActive(false);

        successScreen.SetActive(false);
        questionScreen.SetActive(true);
    }
}
