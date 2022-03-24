using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class GameplayUIManager : MonoBehaviour
{
    public static GameplayUIManager instance;

    [SerializeField] private Text levelText;
    [SerializeField] private GameObject pauseMenuObject;
    [SerializeField] private GameObject endGameMenuObject;
    [SerializeField] private GameObject victoryText;
    [SerializeField] private GameObject loseText;
    [SerializeField] private GameObject[] levelWhiteDots;
    [SerializeField] private AudioSource asource;

    private int levelindex = -1;

    [HideInInspector]
    public bool menuOpen = false;


    private void Awake() 
    {
        instance = this;

        //Deactivate at first.
        for(int i=0; i < 10; i++)
        {
            levelWhiteDots[i].SetActive(false);
        }
    }

    private void Start() 
    {

        pauseMenuObject.SetActive(false);
        endGameMenuObject.SetActive(false);
        victoryText.SetActive(false);
        loseText.SetActive(false);

        levelText.text = GameplayManager.instance.CurrentLevel.Level.ToString();
    }

    public void updateLevelUI()
    {
        levelindex++;
        if(levelindex >= 10) return;

        levelWhiteDots[levelindex].SetActive(true);
    } 


    public void OpenPauseMenu()
    {  
        asource.Play();
        menuOpen = true;
        pauseMenuObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void ClosePauseMenu()
    {
        asource.Play();
        pauseMenuObject.SetActive(false);
        menuOpen = false;
        Time.timeScale = 1;
    }

    public void SkipButton()
    {
        GameplayManager.instance.SkipNextTurn();
    }

    public void RestartButton()
    {
        asource.Play();
        SceneManager.LoadScene(1);
    }

    public void MainMenuButton()
    {
        asource.Play();
        SceneManager.LoadScene(0);
    }

    public void GameWinUI()
    {
        endGameMenuObject.SetActive(true);
        victoryText.SetActive(true);
        menuOpen = true;
        Time.timeScale = 0;
    }

    public void GameLoseUI()
    {
        endGameMenuObject.SetActive(true);
        loseText.SetActive(true);
        menuOpen = true;
        Time.timeScale = 0;
    }



}
