using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Butterfly : CharacterStat
{
    public Slider HealthBar;
    public Transform player;

    public Transform MagicSpawnPoint;
    public GameObject MagicPrefab;

    public GameObject Enemy;

    private float speed = 1f;
    private float radius = 5f;

    public float shootingInterval = 1.0f;
    private float timeSinceLastShot = 0.0f;

    private void Start()
    {
        InitHP();
    }

    private void Update()
    {
        Vector3 offset = new Vector3(Mathf.Sin(Time.time * speed) * radius, 5f, Mathf.Cos(Time.time * speed) * radius);
        transform.position = player.position + offset;
        HealthBar.value = HP;

        timeSinceLastShot += Time.deltaTime;

        if (timeSinceLastShot >= shootingInterval)
        {
            Shoot();
            timeSinceLastShot = 0.0f;
        }
    }

    protected override void CheckHP()
    {
        base.CheckHP();
    }

    protected override void Dead()
    {
        base.Dead();
        Destroy(Enemy);
    }
    void Shoot()
    {
        GameObject MagicClone = Instantiate(MagicPrefab, MagicSpawnPoint.position, MagicSpawnPoint.rotation);
        MagicClone.SetActive(true);
    }
}