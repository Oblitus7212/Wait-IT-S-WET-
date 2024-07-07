using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyringeController : MonoBehaviour
{
    //Shotgun stat
    private int Heal = 2;
    public int CurrentAmmo;
    public int MaxAmmo = 5;

    // Reload timer and time delay
    public float reloadTimer = 10;

    private PlayerController _playercontroller;

    //Get GameManager and scripts
    [SerializeField] private GameObject GameManager;
    private PlayerStat _playerStat;
    public bool canUseSyringe = true;
    public bool EnableSignal = true;

    private void Start()
    {
        GameManager.TryGetComponent<PlayerStat>(out _playerStat);
        TryGetComponent<PlayerController>(out _playercontroller);
        CurrentAmmo = MaxAmmo;
    }

    private void Update()
    {
        Stem();
        ReloadStem();
    }

    private void OnEnable()
    {
        CurrentAmmo = MaxAmmo;
        reloadTimer = 10;
        EnableSignal = true;
    }

    private void OnDisable()
    {

        EnableSignal = false;
    }

    private void Stem()
    {
        if (Input.GetMouseButtonDown(0) && _playercontroller.Syringe.activeSelf && CurrentAmmo > 0 && canUseSyringe)
        {
            CurrentAmmo -= 1;
            _playerStat.TakeHeal(Heal);
            StartCoroutine(SyringeUseDelay());
        }
    }

    IEnumerator SyringeUseDelay()
    {
        canUseSyringe = false;
        yield return new WaitForSeconds(2);
        canUseSyringe = true;
    }

    //Reload section
    private void ReloadStem()
    {
        // If ammo is not full and reload timer is not zero
        if (CurrentAmmo < MaxAmmo && reloadTimer == 10)
        {
            StartCoroutine(ReloadTime(0f));
        }
    }

    IEnumerator ReloadTime(float reloadTimeDelay)
    {
        reloadTimer = reloadTimeDelay;

        while (CurrentAmmo < MaxAmmo && reloadTimer < 10)
        {
            // Start reload
            reloadTimer += Time.deltaTime;
            if (CurrentAmmo < MaxAmmo && reloadTimer >= 10)
            {
                CurrentAmmo += 1;
                // Reset the reload timer to 0
                reloadTimer = 10;
            }
            yield return null;
        }
    }
}