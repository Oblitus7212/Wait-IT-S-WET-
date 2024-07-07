using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoonSpider : CharacterStat
{
    public Slider HealthBar;
    public Transform player;
    public GameObject Enemy;
    public GameObject GameManager;
    private PlayerStat _playerStat;
    private float speed = 3.5f;
    private int getDam = 1;

    private void Start()
    {
        InitHP();
        GameManager.TryGetComponent<PlayerStat>(out _playerStat);
    }

    private void Update()
    {
        Vector3 direction = player.position - transform.position;

        // Move enemy toward player
        Vector3 movement = new Vector3(direction.x, direction.y, direction.z).normalized;
        transform.position += movement * speed * Time.deltaTime;

        // Rotate enemy to face player
        Vector3 targetLook = new Vector3(player.position.x, player.position.y, player.position.z);
        transform.LookAt(targetLook);
        transform.Rotate(new Vector3(-90, 0, 0));
        HealthBar.value = HP;
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

    protected override void InitHP()
    {
        MaxHP = 1;
        SetHP(MaxHP);
        isDead = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerStat.TakeDam(getDam);
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            StartCoroutine(InvincibleTime(getDam));
        }
    }

    IEnumerator InvincibleTime(int getDam)
    {
        yield return new WaitForSeconds(1f);
        this.gameObject.GetComponent<BoxCollider>().enabled = true;
    }
}
