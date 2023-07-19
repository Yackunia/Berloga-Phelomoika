using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{

    [Header("Skripts")]
    [SerializeField] private PlayerMovement move;
    [SerializeField] private PlayerAttackSistem attacker;
    [SerializeField] private WallSliding wall;

    [Header("Other")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator an;

    [SerializeField] private GameObject player;

    [SerializeField] private AudioSource dashSound;

    [Header("Dash Data")]
    public bool canDoDash;

    public bool isDashing;
    [SerializeField] private bool canDash = true;

    private float dashTimer;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashSpeed;



    private void Update()
    {
        InputCheck();
        Dashing();

        Invoke("CheckEndLayer", 0.5f);
    }

    private void CheckEndLayer()
    {
        if (!isDashing && player.layer != 6)
        {
            player.layer = 6;
        }
    }

    private void Dashing()
    {
        if (canDoDash && isDashing)
        {
            rb.velocity = new Vector2(dashSpeed * -move.plFront(), 0);
            dashTimer += Time.deltaTime;

            if (dashTimer > dashTime)
            {
                isDashing = false;
                dashTimer = 0;
                an.SetBool("isDashing", false);
                rb.velocity = new Vector2 (0, 0);
            }
            if (wall.plIsWall())
            {
                StopDash();
                wall.Fall();
            }
            if (!move.plGround()) StopDash();
        }

    }

    public void StopDash()
    {
        an.SetBool("isDashing", false);
        isDashing = false;
        dashTimer = 0;

        canDash = true;

        move.UnFreezePlayer();
        attacker.DisableCombat();
        attacker.EnableCombat();

        player.layer = 6;
    }

    private void InputCheck()
    {
        if (Input.GetKeyDown(KeyCode.E) && canDash && canDoDash && move.plGround() && !wall.plIsWall())
        {
            StartDash();
        }
    }

    private void StartDash()
    {
        an.SetBool("isDashing", true);
        move.StopPlayer();
        attacker.DisableCombat();
        canDash = false;
        isDashing = true;
        player.layer = 10;
        dashSound.Play();
    }

    public void DisableDash()
    {
        canDoDash = false;
        isDashing = false;
        canDash = false;
        player.layer = 6;
        dashTimer = 0;
        an.SetBool("isDashing", false);
    }
    public void EnableDash()
    {
        canDoDash = true;
        canDash = true;
    }
}
