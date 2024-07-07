using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ShotgunHud : MonoBehaviour
{
    [SerializeField] private Image ammoSprites;
    [SerializeField] private Image Crosshair;
    private Image[] ammoImages;

    //Get player object and scripts
    [SerializeField] private GameObject player;
    private GunController _gunController;
    private PlayerController _playercontroller;

    [SerializeField] private GameObject panel; // Reference to the panel to add the Images

    private void Start()
    {
        player.TryGetComponent<GunController>(out _gunController);
        player.TryGetComponent<PlayerController>(out _playercontroller);

        ammoImages = new Image[_gunController.MaxAmmo];
        // Clone image
        for (int i = 0; i < _gunController.MaxAmmo; i++)
        {
            ammoImages[i] = Instantiate(ammoSprites, panel.transform);
        }
        panel.SetActive(false);
    }

    private void Update()
    {
        if(_playercontroller.ShotgunPickUpAlready == true)
        {
            panel.SetActive(true);
        }
        if (_playercontroller.Shotgun != null && _playercontroller.Shotgun.activeSelf)
        {
            Crosshair.enabled = true;
        }
        else
        {
            Crosshair.enabled = false;
        }
        UpdateFillAmount();
        if(_gunController.Enable == false) 
        {
            for (int i = 0; i < _gunController.MaxAmmo; i++)
            {
                ammoImages[i].fillAmount = 1;
            }
        }
    }

    private void UpdateFillAmount()
    {
        int currentAmmo = _gunController.CurrentAmmo;
        switch (currentAmmo)
        {
            case 1:
                StartCoroutine(FillUI(1));
                break;
            case 0:
                ammoImages[1].fillAmount = 0;
                StartCoroutine(FillUI(0));
                break;
            default:
                break;
        }
    }

    IEnumerator FillUI(int Ammo)
    {
        float fillAmount = _gunController.reloadTimer;
        ammoImages[Ammo].fillAmount = fillAmount / 5f;
        yield return null;
    }
}