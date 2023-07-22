using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BEE : EnemyS
{
    [SerializeField] private SpawnBee spawner;

    protected override void SetEnemyModificators()
    {
        base.SetEnemyModificators();

        spawner = GameObject.FindObjectOfType<SpawnBee>();
    }
    protected override void Update()
    {
        base.Update();
        MinusHP();
    }

    protected override void Death()
    {
        base.Death();
        spawner.AddBeePoint();
    }

    private void MinusHP()
    {
        SetHealth(GetHP() - Time.deltaTime * 5f);

        if (GetHP() < 1) {
            Death();
        }
    }
}
