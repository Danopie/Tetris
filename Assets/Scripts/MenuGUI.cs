using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGUI : MonoBehaviour {

    GameObject[] optionObjects;
    GameObject[] mainObjects;

    // Use this for initialization
    void Start () {
        Difficulty.DifficultyLevel = 1;
        optionObjects = GameObject.FindGameObjectsWithTag("Option menu");
        mainObjects = GameObject.FindGameObjectsWithTag("Main menu");

        foreach (GameObject g in optionObjects )
        {
            g.SetActive(false);
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void Option()
    {
        foreach (GameObject g in mainObjects)
        {
            g.SetActive(false);
        }

        foreach (GameObject g in optionObjects)
        {
            g.SetActive(true);
        }
    }

    public void Back()
    {
        foreach (GameObject g in mainObjects)
        {
            g.SetActive(true);
        }

        foreach (GameObject g in optionObjects)
        {
            g.SetActive(false);
        }
    }

    public void OnDifficultyChanged(int level)
    {
        Difficulty.DifficultyLevel = level + 1;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
