using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour
{
    private bool isCorrect;
    [SerializeField]
    private TextMeshProUGUI answerText;

    public void SetAnswerText(string newAnswTxt)
    {
        answerText.text = newAnswTxt;
    }

    public void SetIsCorrect(bool newBool)
    {
        isCorrect = newBool;
    }

    public void OnClick()
    {
        if (isCorrect)
        {
            gameObject.GetComponent<Image>().color = new Color32(45, 184, 78, 255);
            StartCoroutine(ShowNextQuestionWithDelay());
        }
        else
        {
            gameObject.GetComponent<Image>().color = new Color32(228, 60, 3, 255);
            StartCoroutine(DecreaseAttemptsWithDelay());
        }
    }

    IEnumerator DecreaseAttemptsWithDelay()
    {
        yield return new WaitForSeconds(1f);
        gameObject.GetComponent<Image>().color = new Color32(90, 140, 255, 255);
        FindAnyObjectByType<QuestionSetup>().DecreaseAttempts();
    }

    IEnumerator ShowNextQuestionWithDelay()
    {
        yield return new WaitForSeconds(1f);
        gameObject.GetComponent<Image>().color = new Color32(90, 140, 255, 255);
        FindAnyObjectByType<QuestionSetup>().ShowNextQuestion();
    }
}
