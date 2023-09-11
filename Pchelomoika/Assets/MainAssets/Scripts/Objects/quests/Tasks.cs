using UnityEngine;

public class Tasks : MonoBehaviour
{
    public int idOfScene;
    public int count = 0;
    public int endCount = 4;
    public Animator end;
    public GameObject endPannel;
    public Transform endTarget;
    public Transform bear;
    public Loader loader;
    private bool isEnd;
    private bool isOpen;

    private void Update()
    {
        if (count == endCount)
        {
            if (!isEnd) endPannel.SetActive(true);
            isEnd = true;
            if (Vector2.Distance(bear.position, endTarget.position) < 5) loader.LoadScene(idOfScene);
            if (Vector2.Distance(bear.position, endTarget.position) < 25 && !isOpen)
            {
                end.Play("End");
                isOpen = true;
            }
        }
    }
}
