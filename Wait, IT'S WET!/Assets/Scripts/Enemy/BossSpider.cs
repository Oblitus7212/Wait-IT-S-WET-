using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BossSpider : CharacterStat
{
    public Slider HealthBar;
    public Transform player;
    public GameObject Enemy;
    public GameObject SpiderSpawnPoint;
    public GameObject AreaWall;

    private void Start()
    {
        InitHP();
    }

    private void Update()
    {
        // Rotate enemy to face player
        Vector3 targetLook = new Vector3(player.position.x, player.position.y, player.position.z);
        transform.LookAt(targetLook);
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
        MaxHP = 10;
        SetHP(MaxHP);
        isDead = false;
    }

    private void OnDestroy()
    {
        var clones = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var clone in clones)
        {
            Destroy(clone);
        }
        Destroy(SpiderSpawnPoint);
        Destroy(AreaWall);
    }
}
