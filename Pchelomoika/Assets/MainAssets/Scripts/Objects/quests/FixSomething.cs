using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FixSomething : MonoBehaviour
{
    [SerializeField] private GameObject sprites;
    [SerializeField] private GameObject pressButtone;
    [SerializeField] private string objectName;

    private bool hasTakeThing = false;
    private bool canCheckQuestResult = false;

    private void Update()
    {
        if (canCheckQuestResult)
        {
            if (Input.GetKeyDown(KeyCode.E) && hasTakeThing)
            {
                sprites.SetActive(true);
                gameObject.SetActive(false);
            }
               
            else if (Input.GetKeyDown(KeyCode.E))
                Debug.Log("Task isn't completed");
        }
    }

    public void TakeSomething(GameObject nameOfObject)
    {
        if (nameOfObject.name == objectName)
        {
            hasTakeThing = true;
            Destroy(nameOfObject);
        }
 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canCheckQuestResult = true;
            pressButtone.SetActive(true);
        }    
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canCheckQuestResult = false;
            pressButtone.SetActive(false);
        }
    }
}
