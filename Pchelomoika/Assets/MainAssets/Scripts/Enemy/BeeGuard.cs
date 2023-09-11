using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeGuard : EnemyS
{
    [SerializeField] private SpawnBee spawner;
    [SerializeField] private Transform plTransform;
    private float checkTimer;
    private float timerPain;

    protected override void SetEnemyModificators()
    {
        base.SetEnemyModificators();

        spawner = GameObject.FindObjectOfType<SpawnBee>();

        plTransform = GameObject.FindObjectOfType<PlayerHealth>().GetComponent<Transform>();
    }

    protected override void Update()
    {
        base.Update();
        FollowPlayer();
        MinusHP();
    }

    private void FollowPlayer()
    {
        if (!IsChasing()) 
        {
            SetTarget(plTransform);
            SetNeedToLeanBack(true);
            Debug.Log("BeeFreeze");

        }

        

        checkTimer += Time.deltaTime;
        if (isHearting) timerPain += Time.deltaTime;

        if (timerPain > 3f)
        {
            if (isHearting)
            {
                EndPain();
            }
            timerPain = 0f;
        }

        if (checkTimer > .5f)
        {
            if (isHearting)
            {
                EndPain();
            }

            checkTimer = 0;
            if (IsSeeTarget())
            {
                FindTarget();
                SetNeedToLeanBack(false);
                Debug.Log("BeeAttack");
            }
        }
    }
    protected override void Death()
    {
        base.Death();
        spawner.GuardRestoring();
    }
    private void MinusHP()
    {
        SetHealth(GetHP() - Time.deltaTime * 5f);

        if (GetHP() < 1)
        {
            Damage(-10);
        }
        
    }
}
