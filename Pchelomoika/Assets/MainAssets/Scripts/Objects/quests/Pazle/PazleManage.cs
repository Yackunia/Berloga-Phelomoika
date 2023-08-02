using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PazleManage : MonoBehaviour
{
    [SerializeField] private List<Square> squares;
    private List<int> completedSquares = new List<int>();
    private Tasks tasks;
    [SerializeField] private GameObject checkMark;
    private void Start()
    {
        tasks = GameObject.FindObjectOfType<Tasks>();
    }

    [SerializeField] private GameObject pazleEnd;
    public void CheckCompletion(int index)
    {
        var countOfCompletedSquares = 0;

        for (var i = 0; i < squares.Count; i++)
        {
            if (squares[i].GetWireSides())
            {
                completedSquares.Add(index);
                countOfCompletedSquares++;
            }
            else if (completedSquares.Contains(index))
            {
                completedSquares.Remove(index);
                countOfCompletedSquares--;
            }
        }

        if (countOfCompletedSquares == squares.Count)
        {
            EndPazle();
        }
    }

    private void EndPazle()
    {
        Destroy(gameObject);
        pazleEnd.SetActive(true);
        Time.timeScale = 1;
        tasks.count += 1;
        checkMark.SetActive(true);
    }
}
