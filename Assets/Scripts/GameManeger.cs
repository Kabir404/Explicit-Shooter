using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.Utility;

public class GameManeger : MonoBehaviour
{

    public GameObject GameCamera;
    public GameObject WinScreenUI;
    public GameObject LoseScreenUI;
    public GameObject GameUI;
    public bool debugMode = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //to check if the debug mode is active
        if (debugMode) { return; }
        //to check to see if the main cam still exist
        if (GameObject.FindGameObjectsWithTag("MainCamera").Length == 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            GameUI.SetActive(false);
            LoseScreenUI.SetActive(true);
        }

        //to check if the enemy has died 
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            GameCamera.GetComponent<Camera>().enabled = true;
            GameCamera.GetComponent<SmoothFollow>().target = null;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            GameUI.SetActive(false);
            WinScreenUI.SetActive(true);
        }
    }

}
