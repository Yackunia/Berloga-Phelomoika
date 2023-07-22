using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BEE : EnemyS
{
    protected override void Update()
    {
        base.Update();
        MinusHP();
    }

    private void MinusHP()
    {
        SetHealth(GetHP() - Time.deltaTime * 2f);
    }
}
