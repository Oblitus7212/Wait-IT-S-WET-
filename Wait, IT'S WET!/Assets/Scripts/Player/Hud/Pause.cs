using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    private GameObject PauseGameObject;

    private GameObject optionGameObject;

    private GameObject Player;
    void Start()
    {
        Player = GameObject.Find("Playerfirst");
        PausedMenu[] PauseMenu = GameObject.FindObjectsOfType<PausedMenu>(true);
        PausedMenu Pause = PauseMenu[0];
        PauseGameObject = Pause.gameObject;
        Setting[] activeAndInactive = GameObject.FindObjectsOfType<Setting>(true);
        Setting Option = activeAndInactive[0];
        optionGameObject = Option.gameObject;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale > 0 && Player.activeSelf)
        {
            PauseGameObject.SetActive(true);
            Time.timeScale = 0;
            AudioListener.pause = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && optionGameObject.activeSelf == false && Time.timeScale == 0)
        {
            PauseGameObject.SetActive(false);
            Time.timeScale = 1;
            AudioListener.pause = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
