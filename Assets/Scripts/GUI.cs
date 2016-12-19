using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUI : MonoBehaviour {

    public Text scoreText;
    public Text gameOverText;
    private int scoreCount = 0;
	// Use this for initialization
	void Start () {
        Grid.OnUserScored += OnGUIScore;
        Group.OnGameOver += OnGUIGameOver;
        scoreText.text = "Score: " + scoreCount;
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


}
