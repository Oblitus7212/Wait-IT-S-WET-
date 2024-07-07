using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastLocation : MonoBehaviour
{
    public GameObject player;
    public GameObject GameManager;
    private PlayerController _playerController;
    private PlayerStat _playerStat;
    public Vector3 lastGroundedPosition;

    private void Start()
    {
        player.TryGetComponent<PlayerController>(out _playerController);
        GameManager.TryGetComponent<PlayerStat>(out _playerStat);
        lastGroundedPosition = player.transform.position;
    }

    private void Update()
    {
        if (_playerController.canJump)
        {
            lastGroundedPosition = player.transform.position;
        }
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
        player.transform.position = lastGroundedPosition;
        yield return new WaitForSeconds(0.1f);
        _playerController.disabled = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            lastGroundedPosition = player.transform.position;
        }
    }
}
