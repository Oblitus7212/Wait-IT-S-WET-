using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    protected Vector2 movementInput;
    protected Vector2 cameraInput;
    protected bool jumpBtn;
    protected bool sprintBtn;
    protected bool pickBtn;
    protected float ChangeBtn;
    protected bool AcceptBtn = false;
    protected bool RefuseBtn = false;

    protected void OnMovement(InputValue inputValue)
    {
        movementInput = inputValue.Get<Vector2>();
    }
    protected void OnCamera(InputValue inputValue)
    {
        cameraInput = inputValue.Get<Vector2>();
    }
    protected void OnJump(InputValue inputValue)
    {
        jumpBtn = inputValue.isPressed;
    }
    protected void OnSprint(InputValue inputValue)
    {
        sprintBtn = inputValue.isPressed;
    }
    protected void OnChangeWeapon(InputValue inputValue)
    {
        ChangeBtn = inputValue.Get<float>();
    }

    protected void OnPickUpItem(InputValue inputValue)
    {
        pickBtn = inputValue.isPressed;
    }
    protected void OnAccept(InputValue inputValue)
    {
        AcceptBtn = inputValue.isPressed;
    }
    protected void OnRefuse(InputValue inputValue)
    {
        RefuseBtn = inputValue.isPressed;
    }
}