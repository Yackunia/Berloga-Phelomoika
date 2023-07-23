using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    [SerializeField] private GameObject pressButtone;
    [SerializeField] private GameObject pazleQuest;
    [SerializeField] private FixSomething mainObject;
    [SerializeField] private bool pazle;

    private bool canTake = false;

    private void Update()
    {
        if (canTake && Input.GetKeyDown(KeyCode.E) && !pazle)
        {
            mainObject.TakeSomething(gameObject);
            pressButtone.SetActive(false);
        }
            
        if (canTake && pazle && Input.GetKeyDown(KeyCode.E))
        {
            pazleQuest.SetActive(true);
            pressButtone.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canTake = true;
            pressButtone.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canTake = false;
            pressButtone.SetActive(false);
        }
    }
}
