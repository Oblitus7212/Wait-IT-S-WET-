using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene1First : MonoBehaviour
{
    [SerializeField] private GameObject Tutorial;
    [SerializeField] private GameObject E;
    [SerializeField] private GameObject P1;
    [SerializeField] private GameObject P2;
    [SerializeField] private GameObject P3;
    [SerializeField] private GameObject freind;
    void OnTriggerEnter(Collider other)
    {
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        StartCoroutine(Finish());
    }

    IEnumerator Finish()
    {
        E.SetActive(true);
        yield return new WaitForSeconds(2);
        freind.SetActive(false);
        E.SetActive(false);
        P1.SetActive(true);
        yield return new WaitForSeconds(4);
        P1.SetActive(false);
        P2.SetActive(true);
        yield return new WaitForSeconds(6);
        P2.SetActive(false);
        P3.SetActive(true);
        yield return new WaitForSeconds(6);
        P3.SetActive(false);
        Tutorial.SetActive(false);
    }
}
