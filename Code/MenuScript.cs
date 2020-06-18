using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

    public GameObject MainMenu;
    public GameObject ControlsMenu;
    public GameObject HelpMenu;

	// Use this for initialization
	void Start () {
        MainMenu.SetActive(true);
        ControlsMenu.SetActive(false);
        HelpMenu.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Play()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void Options()
    {

    }

    public void Controls()
    {
        MainMenu.SetActive(false);
        ControlsMenu.SetActive(true);
    }

    public void Help()
    {
        MainMenu.SetActive(false);
        HelpMenu.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("EXIT");
    }

    public void Back()
    {
        MainMenu.SetActive(true);
        ControlsMenu.SetActive(false);
        HelpMenu.SetActive(false);
    }
}
