using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour {

    public LevelFader fader;

    public Rocket rocket;

    public Button[] levelButtons;

    private int levelReached;
    private int currentLevel;
    private int levelToUnlock;

     void Start()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        levelToUnlock = SceneManager.GetActiveScene().buildIndex + 1;

        levelReached =  PlayerPrefs.GetInt("levelToUnlock", currentLevel);
        Debug.Log("Current: " + PlayerPrefs.GetInt("levelToUnlock"));
        //PlayerPrefs.SetInt("levelToUnlock", levelToUnlock);
        //Debug.Log("Next: " + PlayerPrefs.GetInt("levelToUnlock"));


        /*  for(int i = 0; i < levelButtons.Length ; i++)
           {
               if(i + 1> levelReached)
                   levelButtons[i].interactable = false;
           }*/
    }

    public void SelectLevel (int levelIndex)
    {
        fader.FadeToLevel(levelIndex);
    }

    public void WinLevel()
    {
        PlayerPrefs.SetInt("levelToUnlock", levelToUnlock);
        Debug.Log("Next: " + PlayerPrefs.GetInt("levelToUnlock"));
    }
}
