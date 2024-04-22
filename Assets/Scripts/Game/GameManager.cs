using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    void Awake()
    {
        pauseScreen.SetActive(false);
        resultScreen.SetActive(false);
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

}
