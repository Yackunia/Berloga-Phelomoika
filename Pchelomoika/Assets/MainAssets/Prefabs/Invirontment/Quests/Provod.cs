using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Provod : MonoBehaviour
{
    private bool isProvod;
    [SerializeField] private Transform st;
    [SerializeField] private Transform end;
    [SerializeField] private FixedJoint2D plJoint;
    [SerializeField] private GameObject[] tuskly;
    [SerializeField] private GameObject[] yarky;
    private Tasks tasks;
    [SerializeField] private GameObject checkMark;
    private void Start()
    {
        tasks = GameObject.FindObjectOfType<Tasks>();
    }
    public void StartProvod()
    {
        isProvod = true;
        plJoint.enabled = true;
        plJoint.transform.position = st.position;
    }

    private void Update()
    {
        if (Vector2.Distance(st.position, end.position) < 2f && isProvod)
        {
            for (int i = 0;i < yarky.Length; i++) 
            {
                yarky[i].SetActive(true);
            }
            Destroy(plJoint);
            tasks.count += 1;
            checkMark.SetActive(true);
            isProvod = false;
            for (int i = 0; i < tuskly.Length; i++)
            {
                Destroy(tuskly[i]);
            }
        }
    }
}
