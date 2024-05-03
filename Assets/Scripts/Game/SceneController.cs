using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    //public static SceneController instance;

    //private void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    public void LoadScene(string sceneName)
    {
        foreach (GameObject gameMan in GameObject.FindGameObjectsWithTag("GameManager"))
        {
            Destroy(gameMan);
        }

        foreach (GameObject dialogueMan in GameObject.FindGameObjectsWithTag("DialogueManager"))
        {
            Destroy(dialogueMan);
        }

        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1f;
    }
}
