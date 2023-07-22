using UnityEngine;
using UnityEngine.UI;

public class SpawnBee : MonoBehaviour
{
    private int beeCount;

    [SerializeField] private int maxCountOfBee;
    
    [SerializeField] private GameObject spawnBee;

    [SerializeField] private Text countOutput;

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

    public void AddBeePoint()
    {
        beeCount++;
        countOutput.text = beeCount.ToString();
    }

}
