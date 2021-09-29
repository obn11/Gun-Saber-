using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Slider slider;
    public static float difficulty;

    private void Start()
    {
        difficulty = slider.value;
    }

    public void Update()
    {
        difficulty = slider.value;
    }

    //Sends to main game
    public void PlayGame()
    {
        SceneManager.LoadScene("Main");
    }

    //Exits game
    public void QuitGame()
    {
        Application.Quit();
    }

    
}
