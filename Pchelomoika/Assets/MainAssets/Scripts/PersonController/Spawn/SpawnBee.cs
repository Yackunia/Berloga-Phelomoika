using UnityEngine;
public class SpawnBee : MonoBehaviour
{
    private bool canSpawnGuard = true;

    [SerializeField] private GameObject spawnBeeGuard;

    [SerializeField] private Animator an;

    private BeeGuard guard;


    public void SpawnGuard()
    {
        if (canSpawnGuard) 
        {
            guard =  Instantiate(spawnBeeGuard, transform.position, spawnBeeGuard.transform.rotation).GetComponent<BeeGuard>();

            canSpawnGuard = false;

            an.Play("Guard");
        }
    }

    public void GuardRestoring()
    {
        Invoke("RestoreGuard", 16f);

        an.Play("Death");
    }

    private void RestoreGuard()
    {
        canSpawnGuard = true;

        an.Play("Reborn");
    }

    public void ReplaceGuard()
    {
        guard.transform.position = transform.position;
        guard.SendMessage("SetTarget", transform);
        guard.SendMessage("SetNeedToLeanBack", true);

        an.Play("Replace");
    }

}
