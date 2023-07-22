using UnityEngine;


public class PlayerAttackSistem : MonoBehaviour
{
    [SerializeField] private PlayerMovement move;
    [SerializeField] private PlayerHealth plHealth;

    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private AudioSource kickAudio;
    [SerializeField] private AudioSource block;

    private bool isInput;
    private bool canInput = true;

    [Header("Combat")]
    public bool isCombat;

    private bool isAttacking;

    [SerializeField] private Transform atHitBoxPosition; 

    [SerializeField] private float attackRad;
    [SerializeField] private float attackDamage;

    [SerializeField] private LayerMask isDamageable;

    [Header("Animation")]

    [SerializeField] private Animator an;

    [SerializeField] private CameraShake cam;

    [SerializeField] private ParticleSystem particle;

    private void Update()
    {
        CheckAttackInput();
    }

    private void CheckAttackInput()
    {
        if (isInput && isCombat && !isAttacking)
        {
            StartAttack();
        }
    }
    private void PlayAttackSound()
    {
        kickAudio.Play();
    }

    private void StartAttack()
    {
        isAttacking = true;
        isInput = false;
        move.wall.DisableWall();
        move.wall.DisableClimb();
        //move.dash.DisableDash();
        move.SlowPlayer();
        an.SetBool("IsAttack", isAttacking);
    }

    public void StartInput()
    {
        if (canInput) 
        { 
            isInput = true; 
            canInput = false;
        }
    }


    private void CheckAttackHitBox()
    {
        Collider2D[] detectedObjs = Physics2D.OverlapCircleAll(atHitBoxPosition.position, attackRad, isDamageable);

        foreach (Collider2D col in detectedObjs)
        {
            col.transform.SendMessage("Damage", attackDamage * -move.plFront());
            cam.StartShake();
        }
    }

    private void EndAttack()
    {
        isAttacking = false;

        an.SetBool("IsAttack", isAttacking);
        an.SetBool("IsCombo", false);
        an.SetBool("next", true);

        move.FastPlayer();
        move.wall.EnableClimb();
        move.wall.EnableWall();

        Invoke("CanInput", .2f);
    }

    private void CanInput()
    {
        canInput = true; 
    }

    private void EndPain()
    {
        isAttacking = false;

        an.SetBool("IsCombo", false);
        an.SetBool("next", true);

        an.SetBool("isPain", false);

        Time.timeScale = 1f;

        move.FastPlayer();

        plHealth.isHearting = false;
        move.wall.isFalling = true;

        plHealth.canHurt = true;

        Invoke("CanInput", .1f);
    }

    public void DisableCombat()
    {
        isAttacking = false;
        an.SetBool("IsCombo", false);
        an.SetBool("next", true);


        isCombat = false;
        an.SetBool("IsAttack", isAttacking);
        an.SetBool("IsSekAttack", isAttacking);
        an.SetBool("Dropping_Plates", false);
    }
    public void EnableCombat()
    {
        isCombat = true;
        move.FastPlayer();
    }

    public bool plAttacking()
    {
        return isAttacking;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(atHitBoxPosition.position, attackRad);
    }
}
