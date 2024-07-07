using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene2Last : MonoBehaviour
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
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(5);
    }
}
