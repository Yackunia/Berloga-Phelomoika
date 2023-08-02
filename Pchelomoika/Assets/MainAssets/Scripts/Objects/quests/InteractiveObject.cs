using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    [SerializeField] private GameObject pazleQuest;
    [SerializeField] private FixSomething mainObject;
    [SerializeField] private bool pazle;
    [SerializeField] private GameObject checkMark;


    public void Take()
    {
        if (!pazle)
        {
            mainObject.TakeSomething(gameObject);
            checkMark.SetActive(true);
        }

        if (pazle)
        {
            pazleQuest.SetActive(true);
            Time.timeScale = 0;
        }
        Destroy(gameObject);
    }
}
