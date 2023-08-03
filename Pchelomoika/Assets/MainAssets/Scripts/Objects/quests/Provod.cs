using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Provod : MonoBehaviour
{
    public bool isProvod;

    [SerializeField] private Transform st;
    [SerializeField] private Transform end;

    [SerializeField] private GameObject[] tuskly;
    [SerializeField] private GameObject[] yarky;
    [SerializeField] private GameObject pushButton;
    [SerializeField] private GameObject checkMark;

    [SerializeField] private FixedJoint2D joint;

    private Tasks tasks;
    private ProvodPhisic provod;
    private void Start()
    {
        tasks = GameObject.FindObjectOfType<Tasks>();
        provod = GameObject.FindObjectOfType<ProvodPhisic>();

        for (int i = 0; i < yarky.Length; i++)
        {
            yarky[i].SetActive(false);
        }

    }

    private void Update()
    {
        CheckCanPush();
    }

    private void CheckCanPush()
    {
        if (Vector2.Distance(st.position, end.position) < 3f && !isProvod && pushButton.activeSelf == false) 
        {
            pushButton.SetActive(true);
        }
        if (Vector2.Distance(st.position, end.position) >= 3f && !isProvod && pushButton.activeSelf == true)
        {
            pushButton.SetActive(false);
        }
    }

    public void Push()
    {
        if (!isProvod)
        {
            isProvod = true;
            for (int i = 0; i < tuskly.Length; i++)
            {
                tuskly[i].SetActive(false);
            }
            for (int i = 0; i < yarky.Length; i++)
            {
                yarky[i].SetActive(true);
            }

            tasks.count++;
            checkMark.SetActive(true);

            joint.enabled = true;
            provod.LoseProvod();
        }
    }

    private void Damage()
    {
        if (isProvod)
        {
            isProvod = false;

            for (int i = 0; i < tuskly.Length; i++)
            {
                tuskly[i].SetActive(true);
            }
            for (int i = 0; i < yarky.Length; i++)
            {
                yarky[i].SetActive(false);
            }

            tasks.count--;
            checkMark.SetActive(false);

            joint.enabled = false;
        }
    }
}
