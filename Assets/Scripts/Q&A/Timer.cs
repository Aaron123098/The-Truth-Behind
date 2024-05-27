using System.Collections; 
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    public float remainingTime;
    int minutes;
    int seconds;

    void Start()
    {
        
    }

    void Update()
    {
        if(remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }else if(minutes < 0)
        {
            remainingTime = 0;
            FindObjectOfType<QuestionSetup>().ShowFailScreen();
        }

        minutes = Mathf.FloorToInt(remainingTime / 60);
        seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = "Tiempo restante: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
