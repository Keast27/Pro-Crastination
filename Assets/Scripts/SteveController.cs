﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerActionMap
{
    Standard,
    UI,
    WaterDispenser
}
public class SteveController : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    private IInteractable interactable;
    private Rigidbody2D rigidBody;
    private Vector2 moveVec;

    public static PlayerControls inputs;

    public static PlayerActionMap ActionMap
    {
        set
        {
            switch (value)
            {
                case PlayerActionMap.Standard:
                    inputs.Standard.Enable();
                    inputs.UI.Disable();
                    inputs.WaterDispenser.Disable();
                    break;
                case PlayerActionMap.UI:
                    inputs.Standard.Disable();
                    inputs.UI.Enable();
                    inputs.WaterDispenser.Disable();
                    break;
                case PlayerActionMap.WaterDispenser:
                    inputs.WaterDispenser.Enable();
                    inputs.Standard.Disable();
                    inputs.UI.Disable();
                    break;
            }
        }
    }

    private void Awake()
    {
        inputs = new PlayerControls();
        rigidBody = GetComponent<Rigidbody2D>();
        ActionMap = PlayerActionMap.Standard;

    }

    private void Start()
    {
        SetUpInputs();

    }

    private void Update()
    {
        Move();
    }

    private void OnEnable()
    {
        inputs.Enable();
    }

    private void OnDisable()
    {
        inputs.Disable();
    }

    void SetUpInputs()
    {
        inputs.Standard.Move.performed += value => moveVec = value.ReadValue<Vector2>();
        inputs.Standard.Move.canceled += _ => moveVec = Vector2.zero;
    }

    private void Move()
    {
        rigidBody.velocity = speed * moveVec;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out interactable))
            inputs.Standard.Interact.performed += interactable.Interact;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IInteractable oldInteractable))
        {
            if (oldInteractable != interactable)
                return;

            inputs.Standard.Interact.performed -= interactable.Interact;
            interactable = null;
        }
    }
}
