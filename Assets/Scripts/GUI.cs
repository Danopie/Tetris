using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GUI : MonoBehaviour {

    
    private int scoreCount = 0;

    Text score;
    GameObject[] pauseObjects;
    GameObject[] gameOverObjects;

	// Use this for initialization
	void Start ()
    {
        Grid.OnUserScored += OnGUIScore;
        Group.OnGameOver += OnGUIGameOver;

        score = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
        score.text = "Score: 0";

        Time.timeScale = 1;
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        hidePaused();

        gameOverObjects = GameObject.FindGameObjectsWithTag("GameOver menu");
        foreach(GameObject g in gameOverObjects)
        {
            g.SetActive(false);
        }
    }

    void Update()
    {
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

    void UnsubscribeEvents()
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
        UnsubscribeEvents();
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
        UnsubscribeEvents();
        SceneManager.LoadScene("Menu");
    }

}
