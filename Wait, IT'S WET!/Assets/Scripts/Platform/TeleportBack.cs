using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TeleportBack : MonoBehaviour
{
    public GameObject player;
    public GameObject GameManager;
    public Transform teleportLocation;
    private PlayerController _playerController;
    private PlayerStat _playerStat;

    private void Start()
    {
        player.TryGetComponent<PlayerController>(out _playerController);
        GameManager.TryGetComponent<PlayerStat>(out _playerStat);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerStat.TakeDam(5);
            StartCoroutine(Teleport());
        }
    }

    IEnumerator Teleport()
    {
        _playerController.disabled = true;
        yield return new WaitForSeconds(0.1f);
        player.transform.position = teleportLocation.position;
        yield return new WaitForSeconds(0.1f);
        _playerController.disabled = false;
    }
}
