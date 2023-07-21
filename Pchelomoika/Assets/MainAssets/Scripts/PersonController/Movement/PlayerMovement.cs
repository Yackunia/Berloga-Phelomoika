using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Other Scripts")]
    public Dash dash;
    public WallSliding wall;

    [Header("Input")]
    private bool isLeftInput;
    private bool isRightInput;
    private bool isJumpInput;

    [Header("Movement")]
    public bool canRun = true;
    public bool canFlip = true;
    public bool canJump = true;

    public float velocityY;

    private int frontOfDirection = 1;

    [SerializeField] private float movementSpeed;
    private float attackMovementSpeed;
    private float normalMovementSpeed;
    [SerializeField] private float jumpVelocity;
    [SerializeField] private float heightOfJumpMultiplier;
    private float movementDirection;

    [SerializeField] private Rigidbody2D rb;// rb игрока

    [Header("Effects")]
    [SerializeField] private ParticleSystem jumpTrail;

    [SerializeField] private AudioSource stepAud;
    [SerializeField] private AudioSource jumpAud;
    [SerializeField] private AudioSource dashAud;

    [Space]

    [Header("Player Animator")]
    public bool isRight;
    private bool isWalking;

    [SerializeField] private Animator an;

    [SerializeField] private Transform drawablePart; //отрисовываемые части игрока

    [Space]

    [Header("Collision")]
    private bool isGround;

    [SerializeField] private float grRad;//радиус groundCheck

    [SerializeField] private Transform groundCheck;

    [SerializeField] private LayerMask whichGround;
    private void Start()
    {
        attackMovementSpeed = movementSpeed / 3;
        normalMovementSpeed = movementSpeed;
    }
    private void Update()
    {
        InputCheck();

        MovementDirectionCheck();

        AnimatorUpdate();
    }

    private void FixedUpdate()
    {
        Movement();

        CheckCollision();
    }

    private void InputCheck()
    {
        MovementInput();

        JumpInput();
    }

    //методы для реализации движения
    #region Movement
    private void MovementInput()
    {
        if (canRun)
        {
            if (isRightInput && isLeftInput || !isRightInput && !isLeftInput) movementDirection = 0;
            else if (isRightInput) movementDirection = 1;
            else movementDirection = -1;
        }
    }

    public void CheckLeft()
    {
        isLeftInput = true;
    }
    public void CheckRight()
    {
        isRightInput = true;
    }
    public void StopLeft()
    {
        isLeftInput = false;
    }
    public void StopRight()
    {
        isRightInput = false;
    }

    private void MovementDirectionCheck()
    {
        if (canFlip)
        {
            if (isRight && movementDirection < 0 && canRun)
                Flip();
            else if (!isRight && movementDirection > 0 && canRun)
                Flip();
        }

        if ((rb.velocity.x > 0.05f || rb.velocity.x < -0.05f) && !isWalking)
        {
            isWalking = true;
            stepAud.Play();
        }
        else if ((rb.velocity.x < 0.05f && rb.velocity.x > -0.05f) && isWalking)
        {
            isWalking = false;
            stepAud.Stop();
        }

        if (!isGround)
        {
            stepAud.Stop();
            isWalking = false;
        }
    }

    private void Movement()
    {
        velocityY = rb.velocity.y;

        if (canRun)
        {
            rb.velocity = new Vector2(movementSpeed * movementDirection, rb.velocity.y);
        }
        //if (Input.GetButtonUp("Jump")) rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * heightOfJumpMultiplier);
    }
    #endregion

    //методы для реализации анимаций и отображения персонажа
    #region Animation
    private void AnimatorUpdate()
    {
        an.SetBool("IsWalking", isWalking);
        an.SetBool("isGround", isGround);
    }

    public void Flip()
    {
        isRight = !isRight;
        frontOfDirection = -frontOfDirection;
        drawablePart.Rotate(0.0f, 180.0f, 0.0f);
    }
    #endregion

    //методы для реализации всех видов прыжков 
    #region Jump
    private void CheckCollision()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, grRad, whichGround);
        if (!isGround) isJumpInput = false;
    }

    private void JumpInput()
    {
        if (isJumpInput && isGround && canRun && canJump) 
        {
            jumpAud.Play();

            isJumpInput = false;

            rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);

            StartJumpTrail();
        }
    }

    public void StartInputJump()
    {
        isJumpInput = true;
        wall.isJumpInput = true;
    }
    public void StopInputJump()
    {
        isJumpInput = false;
        wall.isJumpInput = false;
    }
    #endregion

    //отрисовка интерфейса для дебага
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, grRad);
    }

    //геттеры и сеттеры
    #region Getters/Setter or Enabled/Disabled Functions
    public float plDirection()
    {
        if (movementDirection == 0)
        {
            return frontOfDirection;
        }

        return movementDirection;
    }
    public int plFront()
    {
        return frontOfDirection;
    }
    public bool plGround()
    {
        return isGround;
    }
    public void StopPlayer()
    {
        rb.velocity = new Vector2(0f, rb.velocity.y);
        canRun = false;
        canJump = false;
        canFlip = false;
        isWalking = false;
        stepAud.gameObject.SetActive(false);
        an.SetBool("IsWalking", isWalking);
    }
    public void SlowPlayer()
    {
        canJump = false;
        canFlip = false;
        movementSpeed = attackMovementSpeed;
    }
    public void FastPlayer()
    {
        canJump = true;
        canFlip = true;
        movementSpeed = normalMovementSpeed;
    }
    public void UnFreezePlayer()
    {
        rb.velocity = new Vector2(0f, rb.velocity.y);
        canRun = true;
        canJump = true;
        canFlip = true;
    }
    public void Sitting(bool flag)
    {
        canFlip = false;
        canRun = false;
        rb.velocity = new Vector2(0f, 0f);
        an.SetBool("isSitting", flag);
    }
    #endregion

    #region Effects
    public void StartJumpTrail()
    {
        jumpTrail.Play();
    }

    public void StopJumpTrail()
    {
        //jumpTrail.enabled = false;
    }
    #endregion
}
