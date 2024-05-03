using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionSetup : MonoBehaviour
{
    [SerializeField]
    private List<QuestionData> questions;
    private QuestionData currentQuestion;

    [SerializeField]
    private Image questionImg;
    [SerializeField]
    private AnswerButton[] answerButtons;

    [SerializeField]
    private int correctAnswerChoice;

    public GameObject questionScreen;
    public GameObject successScreen;
    public GameObject failScreen;

    private int attemptsNmbr = 5;
    public TextMeshProUGUI attemptsText;
    public string folderToSearch;

    private void Awake()
    {
        attemptsText.text = "Fallos restantes: " + attemptsNmbr.ToString();
        folderToSearch = "";
    }

    public void ShowNextQuestion()
    {
        if (questions.Count > 0)
        {
            SelectNewQuestion();
            SetQuestionValues();
            SetAnswerValues();
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
    }

    private void SelectNewQuestion()
    {
        
        int randomQuestionIndex = Random.Range(0, questions.Count);
        currentQuestion = questions[randomQuestionIndex];
        questions.RemoveAt(randomQuestionIndex);
    }

    private void SetQuestionValues()
    {
        questionImg = currentQuestion.questionImage;
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
        this.gameObject.SetActive(false);
    }
}
