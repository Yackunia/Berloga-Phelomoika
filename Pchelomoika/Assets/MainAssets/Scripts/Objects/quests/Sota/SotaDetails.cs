using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SotaDetails : MonoBehaviour
{
    private int countofDetails;

    [SerializeField] private GameObject[] SotaObj;

    private void Start()
    {
        SetSotaObj();
    }

    public void SetCount(int value)
    {
        countofDetails += value;

        SetSotaObj();
    }

    private void SetSotaObj()
    {
        for (int i = 0;i < SotaObj.Length; i++) SotaObj[i].SetActive(false);
        for (int i = 0; i < countofDetails; i++) SotaObj[i].SetActive(true);
    }

    public int GetCount()
    {
        return countofDetails;
    }
}
