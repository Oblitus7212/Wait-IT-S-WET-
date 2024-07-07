using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speedUP : MonoBehaviour
{
    public bool SpeedUP = false;

    private void OnTriggerEnter(Collider other)
    {
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        if (other.CompareTag("Player"))
        {
            SpeedUP = true;
        }
    }
}
