using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class QuizData
{
    public Sprite image;
    public string correctAnswer;
    public List<string> options; // Include 3 or 4 options
}
