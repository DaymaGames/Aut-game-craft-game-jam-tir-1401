using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
public class HackerController : MonoBehaviour
{
    private CharacterMovement movement;
    private HackerInventory inventory;
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private void Awake()
    {
        movement = GetComponent<CharacterMovement>();
        inventory = GetComponent<HackerInventory>();
    }

    private void Update()
    {
        float h = Input.GetAxisRaw(HORIZONTAL);
        float v = Input.GetAxisRaw(VERTICAL);
        Vector2 input = new Vector2(h, v);
        movement.SetVelocity(input);

        movement.HandleFacingDirection();

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    private void Reload()
    {
        if(inventory.CanReload() == false)
        {
            return;
        }

        inventory.Reload();
    }

    private void Shoot()
    {
        if(inventory.CanShoot() == false)
        {
            return;
        }

        inventory.DecreaseBulletsInMag();

        print("Shoot");
    }
}
