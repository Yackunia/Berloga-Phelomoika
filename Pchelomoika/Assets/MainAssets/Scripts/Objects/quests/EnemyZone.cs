using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZone : MonoBehaviour
{
    [SerializeField] private List<GameObject> spawnPoints;
    [SerializeField] private GameObject enemy;

    private bool canSpawn = true;
    private int i;
    private void Update()
    {
        while (true)
        {
            if (canSpawn)
            {
                canSpawn = false;
                Invoke("SpawnEnemy", 10);
                if (i >= spawnPoints.Count)
                    i = 0;
                else
                    i++;
            }
        }
    }

    public void SpawnEnemy()
    {
        canSpawn = true;

        Instantiate(enemy, spawnPoints[i].transform.position, Quaternion.identity);
    }
}
