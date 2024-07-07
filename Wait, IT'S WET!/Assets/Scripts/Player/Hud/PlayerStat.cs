using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStat : CharacterStat
{
    [SerializeField] private GameObject player;
    private HPHud hud;

    private void Start()
    {
        GetRef();
        InitHP();
    }

    private void GetRef()
    {
        hud = GetComponent<HPHud>();
    }
    protected override void CheckHP()
    {
        base.CheckHP();
        hud.UpdateHP(HP);
    }

    protected override void Dead()
    {
        base.Dead();
        Destroy(player);
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(1);
    }
}