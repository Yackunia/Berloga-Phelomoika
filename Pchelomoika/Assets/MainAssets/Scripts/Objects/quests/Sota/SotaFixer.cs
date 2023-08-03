using UnityEngine.UI;
using UnityEngine;

public class SotaFixer : MonoBehaviour
{
    private Tasks tasks;
    private SotaDetails details;

    [SerializeField] private GameObject podskaska;
    [SerializeField] private Button endButtonObj;
    [SerializeField] private GameObject endObj;
    [SerializeField] private GameObject checkMark;

    [SerializeField] private Transform bear;
    private void Start()
    {
        tasks = GameObject.FindObjectOfType<Tasks>();
        details = GameObject.FindObjectOfType<SotaDetails>();
    }

    private void Update()
    {
        PodskaskaCheck();
        DetailsCheck();
    }

    private void DetailsCheck()
    {
        if (details.GetCount() > 0 && !endButtonObj.interactable)
        {
            endButtonObj.interactable = true;
        }
        if (details.GetCount() == 0 && endButtonObj.interactable)
        {
            endButtonObj.interactable = false;
        }
    }

    private void PodskaskaCheck()
    {
        if (Vector2.Distance(transform.position, bear.position) < 8f && details.GetCount() > 0 && !podskaska.activeSelf)
        {
            podskaska.SetActive(true);
        }
        if (Vector2.Distance(transform.position, bear.position) > 8f && podskaska.activeSelf)
        {
            podskaska.SetActive(false);
        }
    }

    public void EndQuest()
    {
        endObj.SetActive(true);
        checkMark.SetActive(true);
        tasks.count++;
        Destroy(gameObject);
        details.SetCount(-1);
    }

}
