using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterWall : MonoBehaviour
{
    private float speed = 0.5f;
    public GameObject GameManager;
    public GameObject CheckSpeed;
    private speedUP _speedUP;
    private PlayerStat _playerStat;

    private void Start()
    {
        CheckSpeed.TryGetComponent<speedUP>(out _speedUP);
        GameManager.TryGetComponent<PlayerStat>(out _playerStat);
    }

    private void Update()
    {
        // Move the object's position by speed units in the z-axis
        if (_speedUP.SpeedUP == true) 
        {
            transform.position += new Vector3(0, 0, 7.0f * Time.deltaTime);
        }
        else
        {
            transform.position += new Vector3(0, 0, speed * Time.deltaTime);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerStat.TakeDam(1);
        }
    }
}
