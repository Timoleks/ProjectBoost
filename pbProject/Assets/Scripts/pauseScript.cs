using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class pauseScript : MonoBehaviour {

    public static bool gameIsPaused = false;

    public Rocket rocket;

    public LevelSelect lvlSelect;

    public GameObject pauseUI;

	void Start () {
        
	}
	
	
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape) && rocket.canPause)
        {
            if (gameIsPaused)
                Resume();
            else
                Pause();
        }

	}

    public void Resume()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
        pauseUI.SetActive(false);
        rocket.enabled = true;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        gameIsPaused = true;
        pauseUI.SetActive(true);
        rocket.enabled = false;
    }

    public void backToMenu()
    {
        lvlSelect.SelectLevel(0);
        gameIsPaused = false;
        Time.timeScale = 1f;

    }
}
