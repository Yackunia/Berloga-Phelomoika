using UnityEngine;
using UnityEngine.UI;

public class SpawnBee : MonoBehaviour
{
    private bool canSpawnGuard = true;

    [SerializeField] private GameObject spawnBeeGuard;

    [SerializeField] private Button GuardButton;


    public void SpawnGuard()
    {
        if (canSpawnGuard) 
        {
            Instantiate(spawnBeeGuard, transform.position, spawnBeeGuard.transform.rotation);
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

}
