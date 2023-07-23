using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PazleManage : MonoBehaviour
{
    [SerializeField] private List<Square> squares;
    private List<int> completedSquares = new List<int>();
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
            Debug.Log("вы выигнрали");
    }
}
