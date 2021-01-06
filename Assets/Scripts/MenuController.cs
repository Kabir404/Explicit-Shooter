using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    public bool hasVersionText = false;
    public TMP_Text versionText;
    public bool isPauseMenu = true;
    public GameObject pauseMenuUI;
    public static bool GameIsPaused = false;

    private void Start()
    {
        if (hasVersionText) { versionText.text = Application.version.ToString(); }
        if (isPauseMenu && pauseMenuUI) 
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && isPauseMenu) 
        {
            if (GameIsPaused && isPauseMenu) 
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

    }
    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void LoadMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    public void ExitGame()
    {
        Time.timeScale = 1f;
        Debug.Log("Quitting..");
        Application.Quit();
    }

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0.0f;
        GameIsPaused = true;
        pauseMenuUI.SetActive(true);
    }
    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        GameIsPaused = false;
        pauseMenuUI.SetActive(false);
    }

    public void LoadNextLevel() 
    {
        SceneManager.LoadScene(SceneManager.sceneCount + 1);
    }
}
