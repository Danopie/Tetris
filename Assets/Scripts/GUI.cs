using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GUI : MonoBehaviour {

    
    int scoreCount = 0;
    float timeLeft;

    Text time;
    Text score;
    Text scoreToWin;
    GameObject[] pauseObjects;
    GameObject[] gameOverObjects;
    GameObject[] uiObjects;
	// Use this for initialization
	void Start ()
    {
        Grid.OnUserScored += OnGUIScore;
        Group.OnGameOver += OnGUIGameOver;

        uiObjects = GameObject.FindGameObjectsWithTag("UI");
        foreach(GameObject g in uiObjects)
        {
            if(g.name == "Score")
            {
                score = g.GetComponent<Text>();
                score.text = "Score: 0";
            }

            if (g.name == "Time")
            {
                timeLeft = Difficulty.TimeLeft;
                time = g.GetComponent<Text>();
                time.text = "Time: " + timeLeft;
            }

            if(g.name == "ScoreToWin")
            {
                scoreToWin = g.GetComponent<Text>();
                scoreToWin.text = "Goal: " + Difficulty.ScoreToWin;
            }
        }

        Time.timeScale = 1;
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        hidePaused();

        gameOverObjects = GameObject.FindGameObjectsWithTag("GameOver menu");
        foreach(GameObject g in gameOverObjects)
        {
            g.SetActive(false);
        }

        BorderSize.initBorder();
    }

    void updateTime()
    {
        timeLeft -= Time.deltaTime;
        time.text = "Time: " + timeLeft.ToString("0.0");
        
        if(timeLeft <= 0)
        {
            Difficulty.Lower();
            LowerScore();
            timeLeft = Difficulty.TimeLeft;
        }
    }

    void LowerScore()
    {
        scoreCount -= Difficulty.ScorePenalty;
        if (scoreCount < 0)
            scoreCount = 0;
        score.text = "Score: " + scoreCount;
    }
   
    void Update()
    {
        updateTime();

        if(scoreCount == Difficulty.ScoreToWin)
        {
            OnGUIGameOver();
            Time.timeScale = 0;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                showPaused();
            }
            else if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                hidePaused();
            }
        }
    }

    void OnGUIScore()
    {
        scoreCount++;
        score.text = "Score: " + scoreCount;
    }

    void UnsubscribeEvent()
    {
        //Unsubscribe the events so they don't reference to a destroyed object after reload
        Grid.OnUserScored -= OnGUIScore;
        Group.OnGameOver -= OnGUIGameOver;
    }

    void OnGUIGameOver()
    {
        foreach (GameObject g in gameOverObjects)
        {
            g.SetActive(true);

            if(g.name == "Final score")
            {
                Text finalScore = g.GetComponent<Text>();
                finalScore.text = "Score: " + scoreCount;
            }

            if(g.name == "End status")
            {
                Text endStatus = g.GetComponent<Text>();
                endStatus.text = (scoreCount >= Difficulty.ScoreToWin) ? "You Won" : "You Lost";
            }
        }
    }

    public void showPaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(true);
        }
    }

    public void hidePaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
    }

    //Reloads the scene
    public void Reload()
    {
        UnsubscribeEvent();
        Difficulty.ResetVariables();
        SceneManager.LoadScene("Game");
    }
    
    //Resumes the current scene
    public void Resume()
    {
        Time.timeScale = 1;
        hidePaused();
    }
    
    //Switch to menu
    public void ToMenu()
    {
        UnsubscribeEvent();
        Difficulty.ResetVariables();
        SceneManager.LoadScene("Menu");
    }

}
