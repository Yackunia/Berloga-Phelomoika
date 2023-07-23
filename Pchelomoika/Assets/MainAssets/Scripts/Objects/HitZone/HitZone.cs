using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitZone : MonoBehaviour
{
    [SerializeField] private bool needToRotatePlayer = true;
    [SerializeField] private bool canHurtEnemy;
    public bool canHurtPlayer;

    [SerializeField] private float damageValue;

    [SerializeField] private EnemyS enemyS;

    private PlayerMovement move;

    private void Start()
    {
        move = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        HitTrig(collision);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        HitCol(collision);
    }

    protected virtual void HitTrig(Collider2D collision)
    {
        if (collision.tag == "Player" && canHurtPlayer)
        {
            if (needToRotatePlayer) collision.transform.SendMessage("Damage", damageValue * -move.plDirection());
            else collision.transform.SendMessage("Damage", damageValue * enemyS.enDirection());
        }
        if (collision.transform.tag == "Enemy" && canHurtEnemy && collision.gameObject.layer != gameObject.layer)
        {
            if (needToRotatePlayer) collision.transform.SendMessage("Damage", damageValue * -move.plDirection());
            else collision.transform.SendMessage("Damage", damageValue * enemyS.enDirection());
        }
    }
    protected virtual void HitCol(Collision2D collision)
    {
        if (collision.transform.tag == "Player" && canHurtPlayer)
        {
            if (needToRotatePlayer) collision.transform.SendMessage("Damage", damageValue * -move.plDirection());
            else collision.transform.SendMessage("Damage", damageValue * enemyS.enDirection());
        }
        if (collision.transform.tag == "Enemy" && canHurtEnemy && collision.gameObject.layer != gameObject.layer)
        {
            if (needToRotatePlayer) collision.transform.SendMessage("Damage", damageValue * -move.plDirection());
            else collision.transform.SendMessage("Damage", damageValue * enemyS.enDirection());
        }
    }
}
