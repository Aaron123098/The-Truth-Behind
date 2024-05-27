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

    public Animator transition;
    public float transitionTime = 1;

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

        StartCoroutine(LoadTransitionScene(sceneName));
    }

    IEnumerator LoadTransitionScene(string sceneName)
    {
        print(sceneName);

        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1f;
    }

    public void Init()
    {

    }
}
