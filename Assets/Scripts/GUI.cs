using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GUI : MonoBehaviour {

    public Text scoreText;
    public Text gameOverText;
    private int scoreCount = 0;

    GameObject[] pauseObjects;

	// Use this for initialization
	void Start ()
    {
        Grid.OnUserScored += OnGUIScore;
        Group.OnGameOver += OnGUIGameOver;
        scoreText.text = "Score: " + scoreCount;

        Time.timeScale = 1;
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        hidePaused();
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
        scoreText.text = "Score: " + scoreCount;
    }

    void OnGUIGameOver()
    {
        gameOverText.text = "Game Over";
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
        Group.ResetFallInterval();
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
        Group.ResetFallInterval();
        SceneManager.LoadScene("Menu");
    }

}
