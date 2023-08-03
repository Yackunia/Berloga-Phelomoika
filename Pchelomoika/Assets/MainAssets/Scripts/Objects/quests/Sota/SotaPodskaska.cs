using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SotaPodskaska : MonoBehaviour
{
    [SerializeField] private GameObject podskaska;

    [SerializeField] private Transform bear;

    private void Update()
    {
        CheckPodskaska();
    }

    private void CheckPodskaska()
    {
        if (Vector2.Distance(transform.position, bear.position) < 5f && !podskaska.activeSelf)
        {
            podskaska.SetActive(true);
        }
        if (Vector2.Distance(transform.position, bear.position) > 5f && podskaska.activeSelf)
        {
            podskaska.SetActive(false);
        }
    }
}
