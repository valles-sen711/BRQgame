using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class QuizManager : MonoBehaviour
{
    public List<QuizData> quizList;
    public Image quizImage;
    public Text feedbackText;
    public List<Button> optionButtons;

    public Text scoreText;
    public Text highScoreText;

    private int currentQuizIndex = 0;
    private int score = 0;
    private int highScore = 0;
    private bool isAnswered = false;

    void Start()
    {
        currentQuizIndex = PlayerPrefs.GetInt("LastQuizIndex", 0);

        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateScoreUI();
        LoadQuiz();
    }

    void LoadQuiz()
    {
        isAnswered = false;
        feedbackText.text = "";
        QuizData quiz = quizList[currentQuizIndex];
        quizImage.sprite = quiz.image;

        // Shuffle options
        List<string> shuffledOptions = new List<string>(quiz.options);
        for (int i = 0; i < shuffledOptions.Count; i++)
        {
            string temp = shuffledOptions[i];
            int randIndex = Random.Range(i, shuffledOptions.Count);
            shuffledOptions[i] = shuffledOptions[randIndex];
            shuffledOptions[randIndex] = temp;
        }

        // Assign options to buttons
        for (int i = 0; i < optionButtons.Count; i++)
        {
            optionButtons[i].interactable = true;
            optionButtons[i].GetComponentInChildren<Text>().text = shuffledOptions[i];
            string chosenAnswer = shuffledOptions[i];
            optionButtons[i].onClick.RemoveAllListeners();
            optionButtons[i].onClick.AddListener(() => OnOptionSelected(chosenAnswer));
        }
    }

    void OnOptionSelected(string selectedAnswer)
    {
        if (isAnswered) return;

        string correctAnswer = quizList[currentQuizIndex].correctAnswer;

        if (selectedAnswer == correctAnswer)
        {
            AudioManager.Instance?.PlayCorrect();

            feedbackText.text = "Correct!";
            isAnswered = true;
            DisableOptionButtons();

            score += 10;
            UpdateScoreUI();

            if (score > highScore)
            {
                highScore = score;
                PlayerPrefs.SetInt("HighScore", highScore);
            }

            Invoke(nameof(NextQuestion), 1.5f);
        }
        else
        {
            AudioManager.Instance?.PlayWrong();

            feedbackText.text = "Try Again!";
        }
    }

    void DisableOptionButtons()
    {
        foreach (Button btn in optionButtons)
        {
            btn.interactable = false;
        }
    }

    void NextQuestion()
    {
        currentQuizIndex++;
        PlayerPrefs.SetInt("LastQuizIndex", currentQuizIndex);
        if (currentQuizIndex < quizList.Count)
        {
            LoadQuiz();
        }
        else
        {
            feedbackText.text = $"Quiz Completed! Final Score: {score}";
        }
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score;
        highScoreText.text = "High Score: " + highScore;
    }
}
