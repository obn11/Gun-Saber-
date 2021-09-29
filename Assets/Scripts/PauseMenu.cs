using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool paused = false;
    public GameObject pauseUI;
    public AudioSource audioSource;

    private void Start()
    {
        Resume();
    }
    
    //Check's if the player attempts a pause/resume
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                Resume();
            } else
            {
                Pause();
            }
       
        }
    }

    //Resumes game
    public void Resume()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        audioSource.Play();
    }

    //Pauses game
    void Pause()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        audioSource.Pause();
    }
    
    //Sends player back to the main menu
    public void Menu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    //Restarts the current game from begining of song
    public void ResetGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
