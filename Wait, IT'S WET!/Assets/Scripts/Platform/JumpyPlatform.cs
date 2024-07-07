using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpyPlatform : MonoBehaviour
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
            _PlayerController.IsJumpy = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _PlayerController.IsJumpy = false;
        }
    }
}
