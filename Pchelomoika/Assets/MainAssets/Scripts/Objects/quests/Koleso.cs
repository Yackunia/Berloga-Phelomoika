using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Koleso : MonoBehaviour
{
    [SerializeField] private Transform koleso;
    [SerializeField] private GameObject ready;
    private Tasks tasks;
    [SerializeField] private GameObject checkMark;
    private void Start()
    {
        tasks = GameObject.FindObjectOfType<Tasks>();
    }
    private void Update()
    {
        if (Vector2.Distance(transform.position, koleso.position) < 2f)
        {
            ready.SetActive(true);
            tasks.count+=1;
            checkMark.SetActive(true);
            Destroy(koleso.gameObject);
            Destroy(gameObject);

        }
    }
}
