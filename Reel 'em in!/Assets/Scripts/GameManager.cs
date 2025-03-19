using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public IntData Score;
    public IntData highScoreData;
    public Text highScoreText; // Assign a UI Text component in the Inspector
    private int currentScore;

    void Start()
    {
        // Load and display the high score at the start
        UpdateHighScoreUI();
    }

    public void AddScore()
    {
        currentScore = Score.value;
        Debug.Log("Current Score: " + currentScore);
        
        // Compare and update high score if needed
        if (currentScore > highScoreData.value)
        {
            highScoreData.SetValue(currentScore);
            UpdateHighScoreUI();
        }
    }

    void UpdateHighScoreUI()
    {
        highScoreText.text = "High Score: " + highScoreData.value;
    }
}