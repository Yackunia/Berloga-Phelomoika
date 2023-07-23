using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    [SerializeField] private int direction;
    [SerializeField] private int index;
    [SerializeField] private PazleManage pazleManager;

    private int count;

    public void TurnSquare()
    {
        transform.rotation = Quaternion.Euler(0, 0, ((count + 1) % 4) * -90);

        pazleManager.CheckCompletion(index);
        count++;
    }

    public bool GetWireSides()
    {
        if (direction == count % 4 || direction == count % 4 + 2 ||
            direction == count % 4 - 2)
            return true;

        return false;
    } 
        
}
