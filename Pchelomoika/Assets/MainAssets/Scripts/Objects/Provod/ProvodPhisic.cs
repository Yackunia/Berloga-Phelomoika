using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProvodPhisic : MonoBehaviour
{
    public bool ToughtProvod;

    [SerializeField] private Transform podskaska;

    [SerializeField] private Transform vilkaPoint;
    [SerializeField] private Transform oldPoint;
    [SerializeField] private Transform bear;
    [SerializeField] private Transform vilka;
    [SerializeField] private Transform vilkaTarget;

    [SerializeField] private Animator plAnim;

    [SerializeField] private HingeJoint2D hinge;

    [SerializeField] private PlayerAttackSistem attack;


    private void Update()
    {
        PodskaskaCheck();
    }

    private void PodskaskaCheck()
    {
        podskaska.rotation = Quaternion.identity;

        if (Vector2.Distance(vilkaTarget.position, bear.position) < 5f && !podskaska.gameObject.activeSelf && !ToughtProvod)
        {
            podskaska.gameObject.SetActive(true);
        }

        if (Vector2.Distance(vilkaTarget.position, bear.position) > 5f && podskaska.gameObject.activeSelf && !ToughtProvod)
        {
            podskaska.gameObject.SetActive(false);
        }
    }

    public void TakeProvod()
    {
        Debug.Log("1");
        if (!ToughtProvod)
        {
            bear.position = vilkaTarget.position;

            vilka.position = vilkaPoint.position;
            vilka.rotation = vilkaPoint.rotation;
            vilka.parent = vilkaPoint;

            hinge.enabled = true;

            //attack.DisableCombat();
            ToughtProvod = true;

            podskaska.gameObject.SetActive(false);
        }
        Debug.Log("2");
    }

    public void LoseProvod()
    {
        Debug.Log("3");
        if (ToughtProvod)
        {
            vilka.position = oldPoint.position;
            vilka.rotation = oldPoint.rotation;
            vilka.parent = oldPoint;

            hinge.enabled = false;

            //attack.EnableCombat();
            ToughtProvod = false;
        }
        Debug.Log("5");
    }
}
