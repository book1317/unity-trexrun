using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public enum GameState
    {
        MainMenu, Playing, GameOver
    }

    public enum ObstacleState
    {
        Cactus, Bird
    }

    public GameState currentState = GameState.MainMenu;
    public ObstacleState currentObstacle = ObstacleState.Cactus;
    public int score;
    private int hightScore;
    private float scoreCounter;
    private float scoreTime = 0.01f;

    public List<ObstacleController> allObstacle;

    [Header("Component")]
    [SerializeField] private ObstacleGenerator theObstacleGen;
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private GameObject gameOverText;
    [SerializeField] private GameObject pressSpaceText;
    [SerializeField] private Text hightScoreText;

    void Start()
    {
        allObstacle = new List<ObstacleController>();
        score = 0;
    }

    void Update()
    {
        if (currentState == GameState.MainMenu)
        {
            if (Input.anyKey)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    StartGame();
                }
            }
        }
        else if (currentState == GameState.Playing)
        {
            if (scoreCounter >= 0)
                scoreCounter -= Time.fixedDeltaTime;
            else
            {
                scoreCounter = scoreTime;
                IncreasScore();
                if (score == 500)
                    currentObstacle = ObstacleState.Bird;
                else if (score == 1000)
                {
                    theObstacleGen.generateTime = 1.5f;
                    theObstacleGen.obstacleSpeed = 0.3f;
                }
                else if (score == 1500)
                {
                    theObstacleGen.generateTime = 1f;
                    theObstacleGen.obstacleSpeed = 0.4f;
                }
            }
        }
        else if (currentState == GameState.GameOver)
        {
            if (Input.anyKey)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    RestartGame();
                }
            }
        }
    }

    void StartGame()
    {
        currentState = GameState.Playing;
        pressSpaceText.SetActive(false);
    }

    void IncreasScore()
    {
        score++;
        UpdateScoreText(scoreText, false);
    }

    void UpdateScoreText(Text scoreText, bool isHightScore)
    {
        string prefixScore = "Score";
        if (isHightScore)
            prefixScore = "HScore";

        if (score < 1000)
        {
            if (score >= 100)
                scoreText.text = prefixScore + " : 00" + score;
            else if (score >= 10)
                scoreText.text = prefixScore + " : 000" + score;
            else
                scoreText.text = prefixScore + " : 0000" + score;
        }
        else
        {
            if (score < 10000)
                scoreText.text = prefixScore + " : 0" + score;
            else
                scoreText.text = prefixScore + " : " + score;
        }
    }

    public void RestartGame()
    {
        score = 0;
        theObstacleGen.generateTime = 2.0f;
        theObstacleGen.obstacleSpeed = 0.15f;
        currentObstacle = ObstacleState.Cactus;
        UpdateScoreText(scoreText, false);
        DeltetAllObstacle();
        currentState = GameState.MainMenu;
        gameOverText.SetActive(false);
        restartButton.SetActive(false);
    }


    public void OnDeath()
    {
        currentState = GameState.GameOver;
        gameOverText.SetActive(true);
        restartButton.SetActive(true);
        if (hightScore < score)
        {
            hightScore = score;
            UpdateScoreText(hightScoreText, true);
        }
    }

    public void DeltetAllObstacle()
    {
        for (int i = 0; i < allObstacle.Count; i++)
        {
            Destroy(allObstacle[i].gameObject);
            allObstacle.Remove(allObstacle[i]);
        }
    }

    public void RemoveObstacle(ObstacleController item)
    {
        Destroy(item.gameObject);
        allObstacle.Remove(item);
    }
}
