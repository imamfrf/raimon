using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    public string startLevel;
    public string levelSelect;
    public int playerLives;
    public int playerHealth;

    public void NewGame()
    {
        PlayerPrefs.SetInt("PlayerCurrentLives",playerLives);
        PlayerPrefs.SetInt("CurrentPlayerScore", 0);
        PlayerPrefs.SetInt("PlayerCurrentHealth", playerHealth);
        PlayerPrefs.SetInt("PlayerMaxHealth", playerHealth);
        Application.LoadLevel(startLevel);
    }

    public void LevelSelect()
    {
        PlayerPrefs.SetInt("PlayerCurrentLives", playerLives);
        PlayerPrefs.SetInt("CurrentPlayerScore", 0);
        PlayerPrefs.SetInt("PlayerCurrentHealth", playerHealth);
        PlayerPrefs.SetInt("PlayerMaxHealth", playerHealth);
        Application.LoadLevel(levelSelect);
    }
}
