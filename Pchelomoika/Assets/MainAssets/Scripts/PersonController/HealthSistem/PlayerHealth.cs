using UnityEngine;


public class PlayerHealth : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private Animator an;

    public bool isHearting;
    public bool canHurt;

    [SerializeField] private float hurtTimer;
    private float lastHurtTime = Mathf.NegativeInfinity;

    [SerializeField] private LayerMask isDamageable;

    [Header("Skripts")]
    [SerializeField] private PlayerAttackSistem attack;
    [SerializeField] private PlayerMovement move;

    [Header("Phys")]
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float knockspeedX;
    [SerializeField] private float knockSpeedY;

    [Header("Data")]
    private bool isAlive;

    public float healthPoint;
    public float maxHP;

    public GameObject Player;

    [Header("Effects")]
    [SerializeField] private AudioSource[] painAudio;
    [SerializeField] private AudioSource deathAudio;
    [SerializeField] private AudioSource[] armorAudio;
    [SerializeField] private AudioSource lowHpAudio;

    [SerializeField] private GameObject[] spr;
    [SerializeField] private GameObject[] bloodPrefs;

    [SerializeField] private ParticleSystem particle;


    private void Start()
    {
        Time.timeScale = 1f;
    }
    
    public void Damage(float damage)
    {
        if (canHurt && Time.time > lastHurtTime + hurtTimer)
        {      
            lastHurtTime = Time.time;
            Time.timeScale = 0.4f;
            canHurt = false;

            if (damage < 0) knockspeedX = Mathf.Abs(knockspeedX) * -1;
            else knockspeedX = Mathf.Abs(knockspeedX);

            CheckArmorDamage(damage);

            if (healthPoint/maxHP < 0.2f)
            {
                StartLowHPEffetc();
            }
        }
    }

    private void StartLowHPEffetc()
    {
        lowHpAudio.Play();
    }
    private void StopLowHPEffetc()
    {
        lowHpAudio.Stop();
    }

    private void CheckArmorDamage(float damage)
    {
        particle.Play();
        //Instantiate(bloodPrefs[Random.Range(0, bloodPrefs.Length)], new Vector2(transform.position.x, transform.position.y + 0.2f), Quaternion.identity, null);
        if (healthPoint > Mathf.Abs(damage))
        {
            painAudio[0].Play();

            healthPoint -= Mathf.Abs(damage);
            isHearting = true;
            an.SetBool("isPain", isHearting);
            CalmPain();
        }
        else
        {
            healthPoint = 0f;
            KillPLayer();
        }
    }
    private void CalmPain()
    {
        if (isHearting)
        {
            an.SetBool("isGroundFail", false);

            move.StopPlayer();
            attack.DisableCombat();
            move.wall.DisableWall();
            move.wall.DisableClimb();
            rb.velocity = new Vector2(knockspeedX, knockSpeedY);
        }

    }
    private void CheckDeadHitBox()
    {
        Collider2D[] detectedObjs = Physics2D.OverlapCircleAll(transform.position, 15f, isDamageable);
        foreach (Collider2D col in detectedObjs)
        {
            col.transform.SendMessage("StopFight");
        }
    }

    private void KillPLayer()
    {
        rb.velocity = new Vector2(0f, rb.velocity.y);

        move.StopPlayer();
        move.wall.DisableClimb();
        move.wall.DisableWall();
        attack.DisableCombat();

        Destroy(move.wall);
        Destroy(move);
        Destroy(attack);

        isAlive = false;
        an.SetBool("isAlive", isAlive);

        Time.timeScale = 0.4f;

        spr[0].SetActive(false);
        spr[1].SetActive(true);

        Cursor.visible = true;

        painAudio[0].Play();
        gameObject.layer = default;
        CheckDeadHitBox();
    }

    private void PlayerHill(float hpPlus)
    {
        bool x = (healthPoint / maxHP < 0.2f);
        if (hpPlus + healthPoint < maxHP) healthPoint += hpPlus;
        else healthPoint = maxHP;

        if (healthPoint / maxHP > 0.2f && x)
        {
            StopLowHPEffetc();
        }
    }
}
