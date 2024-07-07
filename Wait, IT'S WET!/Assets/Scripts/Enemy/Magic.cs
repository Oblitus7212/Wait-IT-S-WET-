using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
    public GameObject GameManager;
    private PlayerStat _playerStat;

    private float speed = 1.0f;
    public int getDam = 1;
    private Transform target;
    private Vector3 direction;

    private void Start()
    {
        GameManager.TryGetComponent<PlayerStat>(out _playerStat);
        target = GameObject.FindGameObjectWithTag("Player").transform;
        direction = (target.position - transform.position).normalized;
    }

    private void Update()
    {
        transform.position += direction * speed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerStat.TakeDam(getDam);
        }
    }
}