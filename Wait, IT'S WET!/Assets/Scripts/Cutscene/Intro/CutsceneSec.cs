using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneSec : MonoBehaviour
{
    [SerializeField] private GameObject Tutorial;
    [SerializeField] private GameObject Secondcut;
    [SerializeField] private GameObject freind;
    void OnTriggerEnter(Collider other)
    {
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        Secondcut.SetActive(true);
        freind.SetActive(false);
        StartCoroutine(Finish());
    }

    IEnumerator Finish()
    {
        yield return new WaitForSeconds(1);
        Secondcut.SetActive(false);
        yield return new WaitForSeconds(5);
        Tutorial.SetActive(false);
    }
}
