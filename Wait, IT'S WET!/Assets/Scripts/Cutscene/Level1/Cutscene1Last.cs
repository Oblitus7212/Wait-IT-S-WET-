using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Cutscene1Last : MonoBehaviour
{
    public GameObject Cutscene;
    public GameObject canvas;
    public GameObject _player;
    public GameObject Enemy;

    void Update()
    {
        if (Enemy == null)
        {
            StartCoroutine(LoadLastscene());
        }
    }

    IEnumerator LoadLastscene()
    {
        canvas.SetActive(false);
        _player.SetActive(false);
        Cutscene.SetActive(true);
        yield return new WaitForSeconds(6);
        SceneManager.LoadScene(4);
    }
}
