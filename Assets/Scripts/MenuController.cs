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
    
    public GameObject pauseMenuUI;
    public static bool GameIsPaused = false;
    public bool isPauseMenu = true;
    
    private void Awake()
    {
        if (hasVersionText) { versionText.text = Application.version.ToString(); }
        if (isPauseMenu && pauseMenuUI) 
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        //this.fixedDeltaTime = Time.fixedDeltaTime;
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

        //Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
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
    public void LoadPrevLevel() 
    {
        SceneManager.LoadScene(SceneManager.sceneCount - 1);
    }
}
