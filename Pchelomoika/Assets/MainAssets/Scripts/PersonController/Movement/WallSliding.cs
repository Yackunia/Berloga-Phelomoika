using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSliding : MonoBehaviour
{
    public float maxValueOfVeclocityY;

    [SerializeField] private PlayerMovement move;
    [SerializeField] private PlayerAttackSistem attacker;
    [Header("Input")]
    public bool isJumpInput;

    [Header("Collision")]
    [SerializeField] public bool canWallSliding;

    private bool isWall;
    private bool isWallSliding;
    private bool isStay = true;
    private bool needToLayOnGround;
    private bool onAir;

    [SerializeField] private float wallDistance;

    [SerializeField] private Transform wallCheck;

    [SerializeField] private LayerMask whichWall;

    [SerializeField] private AudioSource fallAudio;
    [SerializeField] private AudioSource jumpAudio;
    [SerializeField] private AudioSource climbAudio;
    [SerializeField] private AudioSource hiddenAudio;


    [Header("Sliding")]
    private bool isJumping;

    [SerializeField] private float slideSpeed;
    [SerializeField] private float wallJumpHeight;
    [SerializeField] private float wallJumpLenght;
    [SerializeField] private float jumpTime;
    private float jumpTimer;

    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private Animator an;

    [Header("Platforms")]
    public bool canTouchPlatform;
    public bool isFalling;

    private bool isPlatform;
    private bool isHanging;
    private bool isClimbing;
    private bool canClimb = true;

    [SerializeField] private float hidSpeed;
    private float progress;

    [SerializeField] private Transform wallUpPoint;

    [SerializeField] private Vector2 startPosition;
    [SerializeField] private Vector2 hidPosition;
    [SerializeField] private Vector2 climbPosition;

    [SerializeField] private LayerMask whichPlatform;

    private void Update()
    {
        CheckIsWall();

        WallSlide();

        Jumping();

        CheckClimb();

        CheckFallGround();
    }
    private void FixedUpdate()
    {
        MoveToHidPoint();
    }
    #region Slide
    private void CheckIsWall()
    {
        isWall = Physics2D.Raycast(wallCheck.position, transform.right, wallDistance * move.plDirection(), whichWall);
        isPlatform = Physics2D.Raycast(wallCheck.position, transform.right, wallDistance * move.plDirection(), whichPlatform);

        if (isPlatform && !isHanging && canTouchPlatform)
        {
            TakePlatform();
        }

        if (isWall && canWallSliding && !attacker.plAttacking() && isStay && !move.plGround())
        {
            isStay = false;
            StartWallSliding();
        }
    }
    private void StartWallSliding()
    {
        isWallSliding = true;
        an.SetBool("isWall", isWallSliding);

        move.StopPlayer();
        attacker.DisableCombat();
        move.StartJumpTrail();

        climbAudio.Play();
    }
    private void WallSlide()
    {
        if (isWallSliding && canWallSliding && !move.plGround() && isWall)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y * slideSpeed);

            InputCheck();
        }
        else if (isWallSliding && canWallSliding && !isJumping && canClimb)
        {
            StopWallSliding();
        }
    }

    private void StopWallSliding()
    {
        isStay = true;
        isWallSliding = false;
        move.UnFreezePlayer();
        attacker.EnableCombat();
        an.SetBool("isWall", isWallSliding);
    }

    private void InputCheck()
    {
        if (isJumpInput)
        {
            Jump();
        }
    }
    private void Jump()
    {
        isJumpInput = false;
        move.Flip();
        isJumping = true;
        an.SetBool("isWall", false);

        jumpAudio.Play();   
    }
    private void Jumping()
    {
        if (isJumping)
        {
            rb.velocity = new Vector2(-move.plFront() * wallJumpLenght, wallJumpHeight);

            jumpTimer += Time.deltaTime;

            if (jumpTime < jumpTimer)
            {
                jumpTimer = 0f;
                isJumping = false;
            }
        }
    }
    #endregion

    #region Climb and Hid
    private void TakePlatform()
    {
        canClimb = false;
        move.StopPlayer();
        attacker.DisableCombat();
        DisableWall();

        isHanging = true;
        an.SetBool("IsHanging", isHanging);

        rb.velocity = new Vector2(0f, 0f);
        rb.isKinematic = true;
        hidPosition = Physics2D.Raycast(wallCheck.position, transform.right, wallDistance * move.plDirection(), whichPlatform).collider.transform.position;
        startPosition = transform.position;

        hiddenAudio.Play(); 
    }
    private void MoveToHidPoint()
    {
        if (!canClimb)
        {
            transform.position = Vector2.Lerp(startPosition, hidPosition, progress);
            progress += hidSpeed;
            if (Vector2.Distance(transform.position, hidPosition) <= 0.1f)
            {
                progress = 0;
                canClimb = true;
            }
        }
        if (isClimbing)
        {
            transform.position = Vector2.Lerp(startPosition, climbPosition, progress);
            progress += hidSpeed; ;
        }
    }

    private void CheckClimb()
    {
        if (isHanging && !isClimbing && canClimb)
        {
            Climb();
        }
    }

    private void Climb()
    {
        isClimbing = true;
        an.SetBool("isClimbing", true);
        progress = 0;
        startPosition = transform.position;
        climbPosition = wallUpPoint.position;

        climbAudio.Play();
    }
    private void EndClimb()
    {
        progress = 0;
        an.SetBool("isClimbing", false);
        an.SetBool("IsHanging", false);
        isClimbing = false;
        isHanging = false;
        move.UnFreezePlayer();
        attacker.EnableCombat();
        EnableWall();

        rb.isKinematic = false;
    }

    #endregion

    #region Other
    public bool plIsWall()
    {
        return isWall;
    }

    public void DisableWall()
    {
        canWallSliding = false;
        isJumping = false;
        jumpTimer = 0;
        isWallSliding = false;
        an.SetBool("isWall", false);
    }
    public void EnableWall()
    {
        canWallSliding = true;
        isWall = false;
        isStay = true;
    }
    public void Fall()
    {
        rb.isKinematic = false;
        isHanging = false;
        progress = 0;
        DisableClimb();
        //DisableWall();
        an.SetBool("isClimbing", false);
        an.SetBool("IsHanging", false);
        an.SetBool("IsAttack", false);
        move.StopPlayer();
        DisableWall();
        attacker.DisableCombat();
        rb.velocity = new Vector2(-move.plDirection() * 4f, 10f);
        isFalling = true;
    }
    private void CheckFallGround()
    {
        CheckStopFall();
        CheckIsOnAir();
    }

    private void CheckIsOnAir()
    {
        if (!move.plGround() && !onAir)
        {
            onAir = true;   
            isJumpInput = false;
        }

        if (move.plGround() && onAir && !isClimbing) 
        {  
            onAir = false;

            fallAudio.Play();
        }
    }

    private void CheckStopFall()
    {
        if (isFalling && move.plGround() && !needToLayOnGround)
        {
            StopFall();
        }
    }

    public void StopFall()
    {
        move.UnFreezePlayer();
        EnableClimb();
        EnableWall();
        attacker.EnableCombat();
        isFalling = false;
    }

    private void GroundDamage()
    {
        if (onAir && rb.velocity.y * -1f > maxValueOfVeclocityY)
        {
            needToLayOnGround = true;
        }

        if (move.plGround() && needToLayOnGround)
        {
            FailOnGround();
        }
    }

    private void FailOnGround()
    {
        StopFall();

        needToLayOnGround = false;

        move.StopPlayer();
        attacker.DisableCombat();
        an.SetBool("isGroundFail", true);
    }

    private void EndLayOnGround()
    {
        an.SetBool("isGroundFail", false);
        move.UnFreezePlayer();

        attacker.EnableCombat();
        EnableClimb();
        EnableWall();

    }

    public void DisableClimb()
    {
        rb.isKinematic = false;
        isHanging = false;
        isClimbing = false;
        progress = 0;
        an.SetBool("isClimbing", false);
        an.SetBool("IsHanging", false);
        canTouchPlatform = false;
    }
    public void EnableClimb()
    {
        canTouchPlatform = true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallDistance * move.plDirection(), wallCheck.position.y, wallCheck.position.z));
    }
    #endregion
}