using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
    public void ExitApp()
    {
        Application.Quit();
    }
}
