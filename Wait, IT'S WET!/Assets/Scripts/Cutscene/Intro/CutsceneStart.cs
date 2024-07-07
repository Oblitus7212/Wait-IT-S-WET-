using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneStart : MonoBehaviour
{
    [SerializeField] protected GameObject Player;
    [SerializeField] protected GameObject CutsceneCam;

    void OnTriggerEnter(Collider other)
    {
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        CutsceneCam.SetActive(true);
        Player.SetActive(false);
        StartCoroutine(Finish());
    }

    public virtual IEnumerator Finish()
    {
        yield return new WaitForSeconds(6);
        CutsceneCam.SetActive(false);
        Player.SetActive(true);
    }
}
