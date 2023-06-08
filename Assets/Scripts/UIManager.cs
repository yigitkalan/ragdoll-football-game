using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //public GameObject pauseMenu;
    public void PlayGame() {  
        SceneManager.LoadScene(1);  
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("MenuScene");
        Application.Quit();

    }

    public void Start()
    {
        //pauseMenu.SetActive(false);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            /*if (pauseMenu.activeSelf)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }*/
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 1;
        //pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        //pauseMenu.SetActive(false);
    }
}
