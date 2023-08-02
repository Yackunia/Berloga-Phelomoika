using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FixSomething : MonoBehaviour
{
    [SerializeField] private GameObject sprites;
    [SerializeField] private string objectName;

    [SerializeField] private bool hasTakeThing = false;
    [SerializeField] private bool canCheckQuestResult = false;
    private Tasks tasks;
    [SerializeField] private GameObject checkMark;
    private void Start()
    {
        tasks = GameObject.FindObjectOfType<Tasks>();
    }

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

    public void Fix()
    {
        if (hasTakeThing)
        {
            sprites.SetActive(true);
            gameObject.SetActive(false);
            Destroy(this);
            tasks.count += 1;
            checkMark.SetActive(true);
        }
    }
}
