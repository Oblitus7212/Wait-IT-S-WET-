using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.Runtime.CompilerServices.RuntimeHelpers;

public class Cutscene3Last : InputManager
{
    public GameObject Player;
    public GameObject Cutscene;
    public GameObject Piss;
    public GameObject NoPiss;
    public GameObject Accept;
    public GameObject Refuse;
    public GameObject Music;
    private bool canPressR = false;
    private float timeSinceStart = 0.0f;

    void OnTriggerEnter(Collider other)
    {
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        Player.SetActive(false);
        Music.SetActive(false);
        Cutscene.SetActive(true);
        StartCoroutine(Finish());
    }

    public virtual IEnumerator Finish()
    {
        yield return new WaitForSeconds(2f);
        Accept.SetActive(true);
        do
        {
            timeSinceStart += Time.deltaTime;
            if (AcceptBtn)
            {
                Cutscene.SetActive(false);
                Piss.SetActive(true);
                Accept.SetActive(false);
                Refuse.SetActive(false);
                yield return new WaitForSeconds(9f);
                SceneManager.LoadScene(6);
            }
            if (RefuseBtn && canPressR)
            {
                Cutscene.SetActive(false);
                NoPiss.SetActive(true);
                Accept.SetActive(false);
                Refuse.SetActive(false);
                yield return new WaitForSeconds(5f);
                SceneManager.LoadScene(7);
            }
            if (timeSinceStart >= 30.0f)
            {
                canPressR = true;
                Refuse.SetActive(true);
            }
            if (timeSinceStart < 30.0f)
            {
                RefuseBtn = false;
            }
                yield return null;
        } while (!AcceptBtn || !RefuseBtn);
    }
}
