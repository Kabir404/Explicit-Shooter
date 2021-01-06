using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class GameManeger : MonoBehaviour
{

    public GameObject GameCamera;
    public GameObject WinScreenUI;
    public GameObject LoseScreenUI;
    public GameObject GameUI;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            GameUI.SetActive(false);
            GameCamera.SetActive(true);
            WinScreenUI.SetActive(true);
        }
        if (GameObject.FindGameObjectsWithTag("Player").Length == 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            GameUI.SetActive(false);
            GameCamera.SetActive(true);
            LoseScreenUI.SetActive(true);
            player.GetComponent<FirstPersonController>().enabled = false;
            player.transform.Find("GunHolder").gameObject.SetActive(false);
        }
    }

}
