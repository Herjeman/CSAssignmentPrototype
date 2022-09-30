using UnityEngine;

public class PickupSpawn : MonoBehaviour
{
    private bool _isNotSpawning;

    public void SpawnPickup(GameObject pickup)
    {
        if (!_isNotSpawning)
        {
            GameObject newPickup = Instantiate(pickup, transform.position, Quaternion.identity);
            newPickup.GetComponentInChildren<PickupBehaviour>().Init(this);
            _isNotSpawning = true;
        }
    }

    public void EnableSpawning()
    {
        _isNotSpawning = false;
    }
}
