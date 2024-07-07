using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutesceneThird : MonoBehaviour
{
    [SerializeField] private GameObject Thirdcut;
    [SerializeField] private GameObject freind;
    void OnTriggerEnter(Collider other)
    {
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        Thirdcut.SetActive(true);
        freind.SetActive(false);
    }
}
