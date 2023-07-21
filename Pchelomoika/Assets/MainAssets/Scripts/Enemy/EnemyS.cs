using UnityEngine;

public class EnemyS : MonoBehaviour
{
    public bool isGismos;

    [Header("Partool / Chase")]
    //variables needed to implement the Movement of the enemy

    private bool canSeeAnotherEnemy;
    private bool seeTarget;
    private bool canSeeTarget;
    private bool canRun = true;
    private bool canFlip = true;
    private bool idleArchive;


    [SerializeField] private int currentDirection = 1;

    private float spawnPoint;

    private Transform target;

    private Rigidbody2D rb;

    [SerializeField] private bool isIdle;
    [SerializeField] private bool isRight;
    [SerializeField] private bool isWallMove;

    [SerializeField] private float chaseSpeed;
    [SerializeField] private float patroolSpeed;

    [SerializeField] private Transform drawObject;

    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private LayerMask anotherEnemys;

    [Header("Raycusts")]
    //variables required for raycasts to work

    [SerializeField] private float targetSeeDistance;
    [SerializeField] private float targetSeeDistanceBack;
    [SerializeField] private float patroolDistance;

    [SerializeField] private Transform targetCheck;
    [SerializeField] private Transform enemysCheck;

    [Header("Animations")]
    //wow, this is realy Animator!!!!!
    private Animator an;

    [Header("Health System")]
    //parameters for Damage/Hill System

    private bool isHearting;
    private bool isAlive = true;

    private float healthMax;

    [SerializeField] private float health;
    [SerializeField] private float knockSpeedX;
    [SerializeField] private float knockSpeedY;

    [SerializeField] private GameObject canv;
    [SerializeField] private HitZone hitZone;

    [SerializeField] private Transform healthFieldDrawable;

    [SerializeField] private SpriteRenderer[] sprites;

    [Header("Effects and Audio")]
    //Audio, Blood, Drop and other effects

    [SerializeField] private GameObject money;

    [SerializeField] private GameObject[] bloodPrefs;

    [SerializeField] private ParticleSystem particle;

    [SerializeField] private AudioSource painAud;
    [SerializeField] private AudioSource deathAud;


    protected virtual void Start()
    {
        SetAllStartParameters();
    }

    protected virtual void Update()
    {
        CheckLayerStats();
        CheckWhatToDo();
        UpdAnim();
        UpdHealh();
    }

    #region Set Default Variables Or Parameters

    private void SetAllStartParameters()
    {
        SetEnemyModificators();
        SetEnemySimpleParameters();
    }


    private void SetEnemyModificators()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        an = gameObject.GetComponent<Animator>();
        hitZone = gameObject.GetComponent<HitZone>();
    }

    private void SetEnemySimpleParameters()
    {
        spawnPoint = transform.position.x; //set center point for patrool mechanic
        healthMax = health;
        idleArchive = isIdle;
    }

    #endregion
    private void CheckWhatToDo()
    {
        CheckIsSeePlayer();
        if (!seeTarget) Patrol();
        else ChasePlayer();

        if (!isAlive) rb.velocity = new Vector2(0f, rb.velocity.y);
    }

    private void CheckIsSeePlayer()
    {
        if (!seeTarget && canSeeTarget)
        {
            StartFight();
        }
    }
    private void StartFight()
    {
        FindTarget();
        seeTarget = true;
        isIdle = false;

    }

    private void FindTarget()
    {
        RaycastHit2D[] raycastHit2Ds = Physics2D.RaycastAll(targetCheck.position, transform.right, (targetSeeDistance + 1) * currentDirection, targetLayer);

        foreach (RaycastHit2D col in raycastHit2Ds)
        {
            if (col.transform != transform)
            {
                target = col.transform;
 
                break;
            }
        }

        raycastHit2Ds = Physics2D.RaycastAll(targetCheck.position, transform.right, -targetSeeDistance * currentDirection,  targetLayer);

        foreach (RaycastHit2D col in raycastHit2Ds)
        {
            if (col.transform != transform)
            {
                target = col.transform;

                break;
            }
        }

        Debug.Log(name + "заагрился на " + target.name);
    }

    protected virtual void UpdAnim()
    {
        an.SetBool("isAlive", isAlive);
        an.SetBool("isPain", isHearting);
        an.SetBool("isIdle", isIdle);
    }
    private void CheckLayerStats()
    {
        canSeeAnotherEnemy = Physics2D.Raycast(enemysCheck.position, transform.right, targetSeeDistance * currentDirection * 0.1f, anotherEnemys);
        canSeeTarget = Physics2D.Raycast(targetCheck.position, transform.right, targetSeeDistance * currentDirection, targetLayer) && isAlive || Physics2D.Raycast(targetCheck.position, transform.right, -targetSeeDistanceBack * currentDirection, targetLayer) && isAlive;
    }

    #region Movement
    //патрулирование
    private void Patrol()
    {
        if (!isIdle)
        {
            Move(patroolSpeed);
            PatroolFlip();
        }
    }
    private void PatroolFlip()
    {
        var x = transform.position.x;
        if ((x < spawnPoint - patroolDistance && currentDirection == -1) || (x > spawnPoint + patroolDistance && currentDirection == 1))
        {
            Flip();
        }
    }
    //преследование
    private void ChasePlayer()
    {
        Move(chaseSpeed);
        ChaseFlip();
    }
    private void ChaseFlip()
    {
        var enemyX = transform.position.x;
        var targetX = target.position.x;
        if ((enemyX < targetX - 1f && currentDirection == -1) || (enemyX > targetX + 1f && currentDirection == 1))
        {
            Flip();
        }
    }
    private void Flip()
    {
        if (canFlip)
        {
            currentDirection *= -1;
            isRight = !isRight;

            drawObject.Rotate(0.0f, 180.0f, 0.0f);
        }
    }

    private void Move(float speed)
    {
        if (canRun && !canSeeAnotherEnemy) rb.velocity = new Vector2(speed * currentDirection, rb.velocity.y);
        else if (canRun) rb.velocity = new Vector2(0, rb.velocity.y);
    }
    #endregion

    private void UpdHealh()
    {
        healthFieldDrawable.localScale = new Vector3(health / healthMax, 1, 1);
    }
    protected virtual void Damage(float damage)
    {
        StartFight();

        StopEnemy();

        DamageCalculation(damage);

        KnockEffect(damage);
        //BloodEffect();
        DamageSoundEffect();
    }

    private void DamageCalculation(float damage)
    {
        damage = Mathf.Abs(damage);

        if (health > damage)
        {
            health -= damage;
            isHearting = true;
        }
        else
        {
            health = 0f;
            Death();
        }
    }

    protected virtual void Death()
    {
        Destroy(hitZone);

        isAlive = false;
        an.SetBool("isAlive", isAlive);

        canv.SetActive(false);

        //rb.velocity = new Vector2(0f, rb.velocity.y);
        //gameObject.layer = 10;

        Destroy(this);

        CheckDeadHitBox();

        for(int i = 0; i < sprites.Length; i++)
        {
            sprites[i].sortingLayerName = "HUD";
        }

        gameObject.layer = 10;
    }
    protected void CheckDeadHitBox()
    {
        Collider2D[] detectedObjs = Physics2D.OverlapCircleAll(transform.position, 15f, targetLayer);
        foreach (Collider2D col in detectedObjs)
        {
            col.transform.SendMessage("StopFight");
        }
    }
    #region Effects
    private void BloodEffect()
    {
        particle.Play();
        Instantiate(bloodPrefs[Random.Range(0, bloodPrefs.Length)], targetCheck.position, Quaternion.identity, null);
    }

    private void KnockEffect(float damage)
    {
        if (damage > 0) knockSpeedX = Mathf.Abs(knockSpeedX);
        else knockSpeedX = -1 * Mathf.Abs(knockSpeedX);

        if (isAlive)
            rb.velocity = new Vector2(knockSpeedX, knockSpeedY);
        else
            rb.velocity = new Vector2(knockSpeedX*1.2f, knockSpeedY*1.8f);
    }

    private void DamageSoundEffect()
    {
        if (isAlive) painAud.Play();
        else deathAud.Play();
    }
    #endregion

    #region For Set Status of Behavior (PUBLIC)
    public void EndPain()
    {
        isHearting = false;

        UnFreezeEnemy();
    }
    public void DamageWithoutDamage()
    {
        StopEnemy();
        DamageCalculation(0);
        KnockEffect(-currentDirection);
    }
    #endregion

    #region For Set Status of Behavior (PRIVATE)

    private void StopFight()
    {
        target = null;

        StopEnemy();
        UnFreezeEnemy();
        seeTarget = false;
        isIdle = idleArchive;
        rb.velocity = Vector2.zero;

        Debug.Log("Stop" + name);
    }
    #endregion

    #region For Set Status of Behavior (PROTECTED)
    protected void StopEnemy()
    {
        canRun = false;
        canFlip = false;

        rb.velocity = new Vector2(0f, 0f);
    }

    protected void UnFreezeEnemy()
    {
        canRun = true;
        canFlip = true;

        rb.velocity = new Vector2(0f, rb.velocity.y);
    }


    #endregion

    #region Public Variable's methods
    public float GetHP()
    {
        return health;
    }

    public int enDirection()
    {
        return currentDirection;
    }
    #endregion

    #region Protected Variable's methods
    protected Rigidbody2D rbEnemy()
    {
        return rb;
    }

    protected Animator anEnemy()
    {
        return an;
    }

    protected bool IsSeePlayer()
    {
        return seeTarget;
    }

    protected bool enHearting()
    {
        return isHearting;
    }
    #endregion

    protected virtual void OnDrawGizmos()
    {
        if (isGismos)
        {
            Gizmos.color = Color.white;
            if (!isRight)
            {
                Gizmos.DrawLine(targetCheck.position, new Vector3(targetCheck.position.x + targetSeeDistance, targetCheck.position.y, targetCheck.position.z));
                Gizmos.color = Color.black;
                Gizmos.DrawLine(targetCheck.position, new Vector3(targetCheck.position.x + -targetSeeDistanceBack, targetCheck.position.y, targetCheck.position.z));
                Gizmos.color = Color.red;
                Gizmos.DrawLine(enemysCheck.position, new Vector3(enemysCheck.position.x + targetSeeDistance * 0.1f, enemysCheck.position.y, targetCheck.position.z));
            }
            else
            {
                Gizmos.DrawLine(targetCheck.position, new Vector3(targetCheck.position.x - targetSeeDistance, targetCheck.position.y, targetCheck.position.z));
                Gizmos.color = Color.black;
                Gizmos.DrawLine(targetCheck.position, new Vector3(targetCheck.position.x + targetSeeDistanceBack, targetCheck.position.y, targetCheck.position.z));
                Gizmos.color = Color.red;
                Gizmos.DrawLine(enemysCheck.position, new Vector3(enemysCheck.position.x - targetSeeDistance * 0.1f, enemysCheck.position.y, targetCheck.position.z));

            }

            Gizmos.color = Color.yellow;
            if (!isIdle)
            {
                Gizmos.DrawWireSphere(new Vector2(transform.position.x - patroolDistance, transform.position.y), 1f);
                Gizmos.DrawWireSphere(new Vector2(transform.position.x + patroolDistance, transform.position.y), 1f);
            }
        }
    }     
}
