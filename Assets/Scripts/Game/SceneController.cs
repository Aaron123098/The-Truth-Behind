using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    //public static SceneController instance;

    //private void Awake()
    //{
    //    if(instance == null)
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }else
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    public void LoadScene(string sceneName)
    {
        if (sceneName == "MainMenu") Destroy(GameObject.FindGameObjectWithTag("GameManager"));
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1f;
    }
}
