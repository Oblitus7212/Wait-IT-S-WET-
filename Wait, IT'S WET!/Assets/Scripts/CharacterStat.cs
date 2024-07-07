using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStat : MonoBehaviour
{
    [SerializeField] protected int HP;
    [SerializeField] protected int MaxHP;
    [SerializeField] protected bool isDead;

    private void Start()
    {
        InitHP();
    }

    protected virtual void CheckHP()
    {
        if(HP <= 0)
        {
            Dead();
        }
        if(HP >= MaxHP) 
        {
            HP = MaxHP;
        }
    }

    protected virtual void Dead()
    {
        isDead = true;
    }

    protected void SetHP(int HPToSet)
    {
        HP = HPToSet;
        CheckHP();
    }

    public void TakeDam(int Damage)
    {
        int HPafterDam = HP - Damage;
        SetHP(HPafterDam);
    }

    public void TakeHeal(int Heal)
    {
        int HPafterHeal = HP + Heal;
        SetHP(HPafterHeal);
    }

    protected virtual void InitHP()
    {
        MaxHP = 10;
        SetHP(MaxHP);
        isDead = false;
    }
}