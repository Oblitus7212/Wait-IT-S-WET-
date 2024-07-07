using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class SyringeHud : MonoBehaviour
{
    [SerializeField] private Image SyringeSprites;
    private Image[] SyringeImages;

    //Get player object and scripts
    [SerializeField] private GameObject player;
    private SyringeController _syringeController;
    private PlayerController _playercontroller;

    [SerializeField] private GameObject panel; // Reference to the panel to add the Images

    private void Start()
    {
        player.TryGetComponent<SyringeController>(out _syringeController);
        player.TryGetComponent<PlayerController>(out _playercontroller);

        SyringeImages = new Image[_syringeController.MaxAmmo];
        // Clone image
        for (int i = 0; i < _syringeController.MaxAmmo; i++)
        {
            SyringeImages[i] = Instantiate(SyringeSprites, panel.transform);
        }
        panel.SetActive(false);
    }

    private void Update()
    {
        if (_playercontroller.SyringePickUpAlready == true)
        {
            panel.SetActive(true);
        }
        UpdateFillAmount();
        if (_syringeController.EnableSignal == false)
        {
            for (int i = 0; i < _syringeController.MaxAmmo; i++)
            {
                SyringeImages[i].fillAmount = 1;
            }
        }
    }

    private void UpdateFillAmount()
    {
        int currentAmmo = _syringeController.CurrentAmmo;
        switch (currentAmmo)
        {
            case 4:
                StartCoroutine(FillUI(4));
                break;
            case 3:
                SyringeImages[4].fillAmount = 0;
                StartCoroutine(FillUI(3));
                break;
            case 2:
                SyringeImages[3].fillAmount = 0;
                StartCoroutine(FillUI(2));
                break;
            case 1:
                SyringeImages[2].fillAmount = 0;
                StartCoroutine(FillUI(1));
                break;
            case 0:
                SyringeImages[1].fillAmount = 0;
                StartCoroutine(FillUI(0));
                break;
            default:
                break;
        }
    }

    IEnumerator FillUI(int Ammo)
    {
        float fillAmount = _syringeController.reloadTimer;
        SyringeImages[Ammo].fillAmount = fillAmount / 10f;
        yield return null;
    }
}