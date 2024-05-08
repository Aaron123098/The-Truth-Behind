using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Question", menuName = "ScriptableObjects/Question", order = 1)]
public class QuestionData : ScriptableObject
{
    public Sprite questionImage;
    [TextArea(3, 10)]
    public string questionText;
    [Tooltip("Correct answer always first, then randomized")]
    [TextArea(3, 10)]
    public string[] answers = new string[4];
}
