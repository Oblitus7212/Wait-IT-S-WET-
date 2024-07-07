using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausedMenu : MonoBehaviour
{
    private GameObject optionGameObject;

    void Start()
    {
        Setting[] activeAndInactive = GameObject.FindObjectsOfType<Setting>(true);
        Setting Option = activeAndInactive[0];
        optionGameObject = Option.gameObject;
    }

    public void ResumeGame()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
        AudioListener.pause = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void OptionMenu()
    {
        optionGameObject.SetActive(true);
    }

    public void ReturnMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
