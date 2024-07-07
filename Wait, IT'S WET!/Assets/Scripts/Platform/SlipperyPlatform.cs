using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlipperyPlatform : MonoBehaviour
{

    public GameObject Player;
    private PlayerController _PlayerController;

    private void Start()
    {
        Player.TryGetComponent<PlayerController>(out _PlayerController);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _PlayerController.IsSlip = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _PlayerController.IsSlip = false;
        }
    }
}
