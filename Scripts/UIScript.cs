using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{

    [SerializeField] private Sprite[] lifeImages;
    [SerializeField] private Image lifeDisplay = null;
    [SerializeField] private Text scoreText = null;
    [SerializeField] private Text highScoreDisplay = null;

    private int score;
    private int highScore = 0;

    [SerializeField] private GameObject titleScreen1 = null;
    [SerializeField] private GameObject titleScreen2 = null;
    [SerializeField] private GameObject paused;

    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt("highScore", 0);
        highScoreDisplay.text = "High Score: " + highScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLives(int lives)
    {
        lifeDisplay.sprite = lifeImages[lives];
    }

    public void UpdateScore(int scoreAmount)
    {
        score += scoreAmount;
        scoreText.text = "Score: " + score;
        CheckForHighScore();
    }

    public void HideTitleScreen()
    {
        titleScreen1.SetActive(false);
        titleScreen2.SetActive(false);
        score = 0;
        scoreText.text = "Score: " + score;
        UpdateLives(3);
    }

    public void ShowTitleScreen()
    {
        titleScreen1.SetActive(true);
        titleScreen2.SetActive(true);
    }

    public void CheckForHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
            highScoreDisplay.text = "High Score: " + highScore;
            PlayerPrefs.SetInt("highScore", highScore);
        }
    }

    public void HidePauseScreen()
    {
        paused.SetActive(false);
    }

    public void ShowPauseScreen()
    {
        paused.SetActive(true);
    }
}
