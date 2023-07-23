using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeGuard : EnemyS
{
    [SerializeField] private SpawnBee spawner;
    [SerializeField] private Transform plTransform;
    private float checkTimer;

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
        if (!IsSeeTarget()) 
        {
            SetTarget(plTransform);
        }

        checkTimer += Time.deltaTime;
        if (checkTimer > .5f)
        {
            FindTarget();
            checkTimer = 0;
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
