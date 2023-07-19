using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIndicators : MonoBehaviour
{
    [SerializeField] private Transform healthFieldDrawable;

    private float hp;
    private float maxHP;
    private float stamina;
    private float staminaMax;

    public PlayerHealth playerHealth;
    public PlayerMovement playerMove;


    private void Start()
    {
        maxHP = playerHealth.maxHP;
    }

    private void Update()
    {
        hp = playerHealth.healthPoint;
        DrawIndicators();
    }

    private void DrawIndicators()
    {
        healthFieldDrawable.localScale = new Vector2(hp/maxHP, 1);
    }
}
