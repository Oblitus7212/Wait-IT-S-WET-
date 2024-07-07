using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class StartMenu : MonoBehaviour
{
    private GameObject optionGameObject;

    void Start()
    {
        Setting[] activeAndInactive = GameObject.FindObjectsOfType<Setting>(true);
        Setting Option = activeAndInactive[0]; 
        optionGameObject = Option.gameObject;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(2);
    }
    public void OptionMenu()
    {
        optionGameObject.SetActive(true);
        gameObject.SetActive(false);
    }
    public void CreditPage()
    {
        SceneManager.LoadScene(8);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}