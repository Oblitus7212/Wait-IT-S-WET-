using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCut : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Countdown());
    }

    public virtual IEnumerator Countdown()
    {
        yield return new WaitForSeconds(5f);
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(8);
    }
}
