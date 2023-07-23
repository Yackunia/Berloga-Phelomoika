using UnityEngine;
using UnityEngine.UI;

public class SpawnBee : MonoBehaviour
{
    private int beeCount;
    private bool canSpawnGuard = true;

    [SerializeField] private int maxCountOfBee;
    
    [SerializeField] private GameObject spawnBee;
    [SerializeField] private GameObject spawnBeeGuard;


    [SerializeField] private Text countOutput;

    [SerializeField] private Button GuardButton;

    private void Start()
    {
        beeCount = maxCountOfBee;
        countOutput.text = beeCount.ToString();
    }

    public void SpawnNewBee()
    {
        if (beeCount > 0)
        {
            beeCount--;
            Instantiate(spawnBee, transform.position, spawnBee.transform.rotation);
        }

        countOutput.text = beeCount.ToString();
    }

    public void SpawnGuard()
    {
        if (canSpawnGuard) 
        {
            Instantiate(spawnBeeGuard, transform.position, spawnBee.transform.rotation);
            canSpawnGuard = false;
            GuardButton.interactable = false;
        }
    }

    public void GuardRestoring()
    {
        Invoke("RestoreGuard", 10f);
    }

    private void RestoreGuard()
    {
        canSpawnGuard = true;
        GuardButton.interactable = true;
    }

    public void AddBeePoint()
    {
        beeCount++;
        countOutput.text = beeCount.ToString();
    }

}
