using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int score;
    public int passedAsteroids;

    [Header("Current results")]
    [SerializeField] Text currentScoreText;
    [SerializeField] Text highScoreText;
    [SerializeField] Text currentSecondsText;

    [Header("Game over results")]
    [SerializeField] GameObject crashPanel;
    [SerializeField] Text endScoreText;
    [SerializeField] Text endSecondsText;
    [SerializeField] Text endAsteroidsText;

    [Header("Popup elements")]
    [SerializeField] Text gameOverText;
    [SerializeField] Text pressKeyText;
    [SerializeField] Text congratulationsText;
    [SerializeField] Button restartButton;

    [Header("Scripts")]
    [SerializeField] SpaceshipMovement SM;
    [SerializeField] RoadSpawner RS;

    private float _timer = 0;
    private int _highScore;
    private float _passedAsteroids;

    private bool _isGameStopped = true;
    private bool _isGameLounched = true;

    void Start()
    {
        StartCoroutine(Stopwatch());

        crashPanel.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        congratulationsText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);

        score = 0;
        passedAsteroids = 0;

        // check stored data
        if (PlayerPrefs.HasKey("High Score"))
        {
            _highScore = PlayerPrefs.GetInt("High Score");
        } else
        {
            _highScore = 0;
        }
    }

    void Update()
    {
        if (Input.anyKeyDown && _isGameLounched)
        {
            StartGame();
            _isGameLounched = false;
        }

        ChangeDifficulty();
        calculateHighScore();

        currentScoreText.text = "Score: " + score.ToString();
        currentSecondsText.text = "Seconds: " + _timer.ToString();
        highScoreText.text = "High score: " + _highScore;

        endScoreText.text = "Score: " + score.ToString();
        endSecondsText.text = "Seconds: " + _timer.ToString();
        endAsteroidsText.text = "Asteroids passed: " + passedAsteroids.ToString();
    }

    private void ChangeDifficulty()
    {
        if (_timer < 10)
        {
            AsteroidSpawner.dificultyCoef = 1;
        }
        if (_timer > 10)
        {
            AsteroidSpawner.dificultyCoef = 2;
        }
        if (_timer > 20)
        {
            AsteroidSpawner.dificultyCoef = 4;
        }
        if (_timer > 30)
        {
            AsteroidSpawner.dificultyCoef = 5;
        } 
    }
    private void calculateHighScore()
    {
        _highScore = score > _highScore ? score : _highScore;
    }

    public void StartGame()
    {
        _isGameStopped = false;
        SM.canMove = true;
        pressKeyText.gameObject.SetActive(false);
    }

    public void RestartGame()
    {
        RS.StartGame();
        SM.canMove = true;

        gameOverText.gameObject.SetActive(false);
        congratulationsText.gameObject.SetActive(false);
        crashPanel.SetActive(false);
        restartButton.gameObject.SetActive(false);

        _isGameStopped = false;

        score = 0;
        _timer = 0;
        passedAsteroids = 0;
        AsteroidSpawner.dificultyCoef = 0;
    }

    public void StopGame()
    {
        SM.canMove = false;
        _isGameStopped = true;

        gameOverText.gameObject.SetActive(true);
        crashPanel.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);

        if (score >= _highScore)
        {
            congratulationsText.gameObject.SetActive(true);
        }
        // save high score in computer
        PlayerPrefs.SetInt("High Score", _highScore);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    IEnumerator Stopwatch()
    {
        while (true)
        {
            if (!_isGameStopped)
            {
                _timer++;
                score++;

                if (Input.GetKey(KeyCode.Space))
                {
                    score++;
                }
                yield return new WaitForSeconds(1);
            }
            yield return null;
        }
    }
}
