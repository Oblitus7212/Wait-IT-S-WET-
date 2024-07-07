using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene2First : MonoBehaviour
{
    [SerializeField] protected GameObject Player;
    [SerializeField] protected GameObject Cutscene;
    [SerializeField] protected GameObject Wall;

    void OnTriggerEnter(Collider other)
    {
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        Player.SetActive(false);
        Cutscene.SetActive(true);
        Wall.SetActive(true);
        StartCoroutine(Finish());
    }

    public virtual IEnumerator Finish()
    {
        yield return new WaitForSeconds(4);
        Cutscene.SetActive(false);
        Player.SetActive(true);
    }
}
