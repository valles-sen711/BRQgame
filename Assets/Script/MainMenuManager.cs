using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public Text highScoreText;
    public Button startButton;
    public Button newGameButton;

    void Start()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "High Score: " + highScore;

        startButton.onClick.AddListener(OnStartPressed);
        newGameButton.onClick.AddListener(OnNewGamePressed);
    }

    void OnStartPressed()
    {
        // Continue from saved level
        SceneManager.LoadScene("Game");
    }

    void OnNewGamePressed()
    {
        // Reset progress to question 0
        PlayerPrefs.SetInt("LastQuizIndex", 0);
        PlayerPrefs.SetInt("HighScore", 0);
        SceneManager.LoadScene("Game");
    }
}
