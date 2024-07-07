using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HPHud : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI CurHP;
    public void UpdateHP(int curHP)
    {
        CurHP.text = curHP.ToString();
    }
}