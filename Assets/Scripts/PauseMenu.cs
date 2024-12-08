using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausemenu;
    public Pause pause;
    private DontDestroy Des;
    public void Continue()
    {
        pause.ispause = false;
        pausemenu.SetActive(!pausemenu.activeSelf);
        Time.timeScale = 1.0f;
        Cursor.visible = false;
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        Cursor.visible = false;
        Time.timeScale = 1.0f;
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
        Cursor.visible = true;
        Time.timeScale = 1.0f;
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
