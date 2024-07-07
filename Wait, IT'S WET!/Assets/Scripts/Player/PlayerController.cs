using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerController : InputManager
{
    [Header("Rotation")]
    [SerializeField] private Transform cameraFollowTarget;
    [SerializeField] private float cameraLookAngle; // Camera Look Up/Down
    [SerializeField] private float cameraPivotAngle; // Camera Look Left/Right
    [SerializeField] private float cameraLookSpeed = 35f;
    [SerializeField] private float cameraPivotSpeed = 35f;
    [SerializeField] private float minimumPivotAngle = -80f;
    [SerializeField] private float maximumPivotAngle = 80f;

    [Header("Movement")]
    [SerializeField] private float moveBaseSpeed = 5;
    [SerializeField] private float moveSprintSpeed = 9;
    public bool disabled = false;
    public bool IsSlip = false;
    public bool IsJumpy = false;

    [Header("Gravity")]
    [SerializeField] private float gravityModifier = 2f;
    [SerializeField] private float jumpPower = 8;

    [Header("Checker")]
    public bool canJump, canDoubleJump; // bool indicating player can perform jump action
    [SerializeField] private float checkGroundSpereRadius = 0.25f; // Make it about character radius
    [SerializeField] private float checkGroundSpereOffset = 0.14f; // offset it by half the radius
    [SerializeField] private LayerMask groundLayers; // for detecting ground object

    [Header("Sound")]
    [SerializeField] private AudioSource walk;
    [SerializeField] private AudioSource run;

    [Header("Arm")]
    public GameObject ArmJump;
    public GameObject ArmRight;

    [Header("Weapon")]
    public GameObject Shotgun; // Shotgun
    public GameObject Syringe; // Syringe
    public bool ShotgunPickUpAlready = false; // Check if player has ever pick shotgun up
    public bool SyringePickUpAlready = false;// Check if player has ever pick syringe up

    [Header("Pickup item")]
    private float pickupRadius = 2f; // Radius around the player within which items can be picked up
    public LayerMask pickupLayer; // Layer containing the items to be picked up

    private float mouseSensitivity;

    private GameObject _mainCamera;
    private CharacterController _characterController;
    private GunController _gunController;
    private SyringeController _syringeController;
    private Vector3 moveDirection;
    private Vector3 previousMoveDirection;
    private Animator animRun;
    [SerializeField] Animator animWeapon;

    // Start is called before the first frame update
    private void Start()
    {
        GetRef();
    }
    // Update is called once per frame
    private void Update()
    {
        mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity");
        HandleMovement();
        HandleCameraRotation();
        AnimationRun();
        ChangeHand();
        PickUpItem();
        AnimationStem();
    }

    private void HandleMovement()
    {
        if (disabled) return;

        // store y value from last frame
        float yPrevious = moveDirection.y;

        // Calculate movement direction based on camera and player directions
        // Forward
        Vector3 forward = _mainCamera.transform.forward;
        forward.y = 0f;
        // Right
        Vector3 right = _mainCamera.transform.right;
        right.y = 0f;

        // Use the previous move direction if the current input is zero
        if (movementInput == Vector2.zero && IsSlip)
        {
            moveDirection = previousMoveDirection;
        }
        else
        {
            moveDirection = forward.normalized * movementInput.y + right.normalized * movementInput.x;
            moveDirection.Normalize();
            previousMoveDirection = moveDirection;
        }

        // Calculate base speed here
        HandleMoveSpeed();

        // Don't use camera direction for y-axis
        moveDirection.y = 0f;
        // Assign previous y-value after move Direction has been calculated
        moveDirection.y = yPrevious;
        moveDirection.y += Physics.gravity.y * gravityModifier * Time.deltaTime;

        if (_characterController.isGrounded && !disabled)
        {
            moveDirection.y = Physics.gravity.y * gravityModifier * Time.deltaTime;
        }
        // Detect whether the player can jump or not
        CheckCanJump();

        HandleJump();

        if (moveDirection == Vector3.zero || disabled) return;

        _characterController.Move(moveDirection * Time.deltaTime);
    }

    private void HandleCameraRotation()
    {
        // cameraFollowTarget
        cameraLookAngle += (cameraInput.x * cameraLookSpeed * mouseSensitivity * Time.deltaTime);
        cameraPivotAngle -= (cameraInput.y * cameraPivotSpeed * mouseSensitivity * Time.deltaTime);
        cameraPivotAngle = Mathf.Clamp(cameraPivotAngle, minimumPivotAngle, maximumPivotAngle);

        Vector3 rotation = Vector3.zero;
        rotation.y = cameraLookAngle;
        rotation.x = cameraPivotAngle;
        // Convert Look Angle to Quaternion representation
        Quaternion targetRotation = Quaternion.Euler(rotation);
        // Assign it to main camera / camera target transform
        cameraFollowTarget.rotation = targetRotation;
    }

    private void HandleMoveSpeed()
    {
        if (sprintBtn)
        {
            moveDirection *= moveSprintSpeed;
        }
        else
        {
            moveDirection *= moveBaseSpeed;
        }
    }

    private void HandleJump()
    {
        if (jumpBtn)
        {
            // Consume jump signal here
            jumpBtn = false;
            if (canJump)
            {
                // Operate jump behaviour here
                moveDirection.y = jumpPower;
            }
            else if (canDoubleJump && ArmJump.activeSelf)
            {
                moveDirection.y = jumpPower;
                canDoubleJump = false;
            }
        }
        if (_gunController.forceJump && !canJump)
        {
            // Get the forward direction of the camera
            Vector3 forward = -_mainCamera.transform.forward;

            // Set the move direction to the jump direction
            moveDirection = forward.normalized * (jumpPower * 1.5f);
            _gunController.forceJump = false;
        }
        else
        {
            _gunController.forceJump = false;
        }
        if (IsJumpy)
        {
            moveDirection.y = 40;
        }
    }

    private void CheckCanJump()
    {
        Vector3 currentPosition = transform.position;
        // Offset position up a little
        currentPosition.y += checkGroundSpereOffset;
        canJump = Physics.OverlapSphere(currentPosition, checkGroundSpereRadius, groundLayers).Length > 0;
        // Reset Double Jump
        if (canJump)
        {
            canDoubleJump = true;
        }
    }

    private void AnimationStem()
    {
        if (!_syringeController.canUseSyringe && SyringePickUpAlready)
        {
            animWeapon.SetFloat("isInject", 1f);
        }
        else
        {
            animWeapon.SetFloat("isInject", 0f);
        }
    }

    private void AnimationRun()
    {
        //Check if player move or not
        //if not move make no sound play
        if (movementInput == Vector2.zero)
        {
            animRun.SetFloat("Speed", 0f, 0.2f, Time.smoothDeltaTime);
            run.Stop();
            walk.Stop();
        }
        //if yes and not run play walk sound
        else if (movementInput != Vector2.zero && !Input.GetKey(KeyCode.LeftShift))
        {
            animRun.SetFloat("Speed", 0.5f, 0.2f, Time.smoothDeltaTime);
            if (!walk.isPlaying && canJump)
            {
                run.Stop();
                walk.Play();
            }
            //if in the air stop play run and walk
            else if (!canJump)
            {
                run.Stop();
                walk.Stop();
            }
        }
        //if yes and run play run sound
        else if (movementInput != Vector2.zero && Input.GetKey(KeyCode.LeftShift))
        {
            animRun.SetFloat("Speed", 1f, 0.2f, Time.smoothDeltaTime);
            if (!run.isPlaying && canJump)
            {
                run.Play();
                walk.Stop();
            }
            //if in the air stop play run and walk
            else if (!canJump)
            {
                run.Stop();
                walk.Stop();
            }
        }
    }

    private void ChangeHand()
    {
        //Player go to jump mode
        if (ChangeBtn == 1)
        {
            ArmJump.SetActive(true);
            ArmRight.SetActive(false);

            Shotgun.SetActive(false);
            Syringe.SetActive(false);
        }
        //player can shoot shotgun
        else if (ChangeBtn == 2 && ShotgunPickUpAlready == true)
        {
            ArmJump.SetActive(false);
            ArmRight.SetActive(true);

            Shotgun.SetActive(true);
            Syringe.SetActive(false);
            animWeapon.SetFloat("isSwitch", 0f);
        }
        //player can Heal
        else if (ChangeBtn == 3 && SyringePickUpAlready == true)
        {
            ArmJump.SetActive(false);
            ArmRight.SetActive(true);

            Shotgun.SetActive(false);
            Syringe.SetActive(true);
            animWeapon.SetFloat("isSwitch", 1f);
        }
    }

    private void PickUpItem()
    {
        Vector3 currentPosition = transform.position;
        if (pickBtn)
        {
            // Check for items within the pickup radius
            Collider[] nearbyItems = Physics.OverlapSphere(currentPosition, pickupRadius, pickupLayer, QueryTriggerInteraction.Collide);

            // Loop through all nearby items and pick them up
            foreach (Collider item in nearbyItems)
            {
                // Check if the player's camera is facing the item
                Vector3 itemDirection = item.transform.position - _mainCamera.transform.position;
                float angle = Vector3.Angle(_mainCamera.transform.forward, itemDirection);
                if (angle <= 45f)
                {
                    if (item.CompareTag("Shotgun")){
                        ShotgunPickUpAlready = true;
                        Destroy(item.gameObject);
                    }
                    if (item.CompareTag("Syringe"))
                    {
                        SyringePickUpAlready = true;
                        Destroy(item.gameObject);
                    }
                }
            }
        }
    }

    private void GetRef()
    {
        TryGetComponent<CharacterController>(out _characterController);
        TryGetComponent<GunController>(out _gunController);
        TryGetComponent<SyringeController>(out _syringeController);
        animRun = GetComponentInChildren<Animator>();
        _mainCamera = GameObject.FindWithTag("MainCamera");
    }
}