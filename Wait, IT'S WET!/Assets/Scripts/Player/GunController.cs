using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GunController : MonoBehaviour
{
    //Shotgun stat
    private int damage = 1;
    private float range = 30f;
    private float coneAngle = 20f;
    public int CurrentAmmo;
    public int MaxAmmo = 2;
    public bool forceJump;
    public LayerMask targetMask;
    public bool Enable = true;

    // Reload timer and time delay
    public float reloadTimer = 5;

    private PlayerController _playercontroller;
    [SerializeField] private AudioSource GunSound;

    private void Start()
    {
        TryGetComponent<PlayerController>(out _playercontroller);
        CurrentAmmo = MaxAmmo;
    }

    private void Update()
    {
        Shoot();
        ReloadShotgun();
    }

    private void OnEnable()
    {
        CurrentAmmo = MaxAmmo;
        reloadTimer = 5;
        Enable = true;
    }

    private void OnDisable()
    {

        Enable = false;
    }

    public void Shoot()
    {
        // Check for enemy within the shoot radius
        Collider[] targets = Physics.OverlapSphere(transform.position, range, targetMask);
        // Check if get left click and player are carry shotgun
        if (Input.GetMouseButtonDown(0) && _playercontroller.Shotgun.activeSelf && CurrentAmmo > 0)
        {
            CurrentAmmo -= 1;
            forceJump = true;
            GunSound.Play();
            // Loop through all in range and angle of enemy and damage them
            foreach (Collider target in targets)
            {
                //Get direction from camera view and target
                Vector3 directionToTarget = (target.transform.position - Camera.main.transform.position).normalized;
                //Get Angle from camera forward
                float angleToTarget = Vector3.Angle(Camera.main.transform.forward, directionToTarget);
                if (angleToTarget <= coneAngle)
                {
                    target.GetComponent<CharacterStat>().TakeDam(damage);
                }
            }
        }
    }

    //Reload section
    private void ReloadShotgun()
    {
        // If ammo is not full and reload timer is not zero
        if (CurrentAmmo < MaxAmmo && reloadTimer == 5)
        {
            StartCoroutine(ReloadTime(0f));
        }
    }

    IEnumerator ReloadTime(float reloadTimeDelay)
    {
        reloadTimer = reloadTimeDelay;

        while (CurrentAmmo < MaxAmmo && reloadTimer < 5)
        {
            // Start reload
            reloadTimer += Time.deltaTime;
            if (CurrentAmmo < MaxAmmo && reloadTimer >= 5)
            {
                CurrentAmmo += 1;
                // Reset the reload timer to 0
                reloadTimer = 5;
            }
            yield return null;
        }
    }
}