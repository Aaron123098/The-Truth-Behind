using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DialogueManager;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public enum GameState
    {
        GamePlay,
        GamePaused,
        GameOver
    }

    public GameState currentState;
    public GameState previousState;

    public GameObject pauseScreen;
    public GameObject resultScreen;

    public bool isGameOver = false;

    public static GameManager gameManager;

    public JSONSaveLoadSystem saveLoadSystem;

    void Awake()
    {
        if (gameManager != null)
        {
            Destroy(gameManager);
        }
        else
        {
            gameManager = this;
        }
        DontDestroyOnLoad(this);

        pauseScreen.SetActive(false);
        resultScreen.SetActive(false);

    }

    private void Start()
    {
        
    }

    private void Update()
    {
        switch (currentState)
        {
            case GameState.GamePlay:
                CheckForPauseAndResume();
                break; 
            
            case GameState.GamePaused:
                CheckForPauseAndResume();
                break; 
            
            case GameState.GameOver:
                if (!isGameOver)
                {
                    isGameOver = true;
                    DisplayResults();
                    Time.timeScale = 0f;
                }
                break;

            default:
                break;
        }
    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;
    }

    public void PauseGame()
    {
        if(currentState != GameState.GamePaused)
        {
            previousState = currentState;
            ChangeState(GameState.GamePaused);
            Time.timeScale = 0f;
            pauseScreen.SetActive(true);
        }
        
    }

    public void ResumeGame()
    {
        if(currentState == GameState.GamePaused)
        {
            ChangeState(previousState);
            Time.timeScale = 1f;
            pauseScreen.SetActive(false);
        }
    }

    void CheckForPauseAndResume()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            if(currentState == GameState.GamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void GameOver()
    {
        ChangeState(GameState.GameOver);
    }

    void DisplayResults()
    {
        resultScreen.SetActive(true);

    }

    public void SendToYard()
    {
        Time.timeScale = 1f;
        DialogueManager dialogueManager = FindAnyObjectByType<DialogueManager>();

        if(dialogueManager.dialogueState == DialogueManager.DialogueState.BeforeFrstRun)
        {
            dialogueManager.dialogueState = DialogueManager.DialogueState.AfterFrstRun;
            string saveString = dialogueManager.dialogueState.ToString();
            PlayerPrefs.SetString("DialogueState", saveString);
        }
        FindAnyObjectByType<SceneController>().LoadScene("Yard");
    }

    public void SendToYardAfterWin()
    {
        Time.timeScale = 1f;

        DialogueManager dialogueManager = FindAnyObjectByType<DialogueManager>();

        switch (dialogueManager.dialogueState)
        {
            case DialogueManager.DialogueState.BeforeFrstRun:
                dialogueManager.dialogueState = DialogueManager.DialogueState.FrstRunCompleted;
                break;
            case DialogueManager.DialogueState.AfterFrstRun:
                dialogueManager.dialogueState = DialogueManager.DialogueState.FrstRunCompleted;
                break;

            case DialogueManager.DialogueState.FrstRunCompleted:
                dialogueManager.dialogueState = DialogueManager.DialogueState.ScndRunCompleted;
                break;

            case DialogueManager.DialogueState.ScndRunCompleted:
                dialogueManager.dialogueState = DialogueManager.DialogueState.ThirdRundCompleted;
                break;

            default: break;
        }

        string saveString = dialogueManager.dialogueState.ToString();
        PlayerPrefs.SetString("DialogueState", saveString);

        FindAnyObjectByType<SceneController>().LoadScene("Yard");
    }

}
