using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneLast : CutsceneStart
{
    [SerializeField] private GameObject freind;

    public override IEnumerator Finish()
    {
        freind.SetActive(false);
        yield return new WaitForSeconds(9);
        CutsceneCam.SetActive(false);
        SceneManager.LoadScene(3);
    }
}
