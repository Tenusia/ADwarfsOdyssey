using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiController : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainScreen");
    }
    
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    //public void LoadGameOver()
    //{
    //    SceneManager.LoadScene("MainLevel");
    //}

    public void QuitGame()
    {
        Debug.Log("Quiting game");
        Application.Quit();
    }
}
