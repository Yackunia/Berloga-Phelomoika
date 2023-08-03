using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tasks : MonoBehaviour
{
    public int count = 0;
    public int endCount = 4;
    public GameObject end;

    private void Update()
    {
        if (count == endCount)
        {
            end.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
