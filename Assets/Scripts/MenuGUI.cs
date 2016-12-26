using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuGUI : MonoBehaviour {

    GameObject[] optionObjects;
    GameObject[] mainObjects;

    // Use this for initialization
    void Start () {
        optionObjects = GameObject.FindGameObjectsWithTag("Option menu");
        mainObjects = GameObject.FindGameObjectsWithTag("Main menu");

        foreach (GameObject g in optionObjects )
        {
            g.SetActive(false);

            if(g.name == "Difficulty List")
            {
                Dropdown dd = g.GetComponent<Dropdown>();
                dd.value = Difficulty.DifficultyLevel - 1;
            }
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

    public void onSizeChange(int option)
    {
        BorderSize.sizeOption = option;
    }
    public void Exit()
    {
        Application.Quit();
    }
}
