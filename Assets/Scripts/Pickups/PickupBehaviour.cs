using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBehaviour : MonoBehaviour
{
    private PickupSpawn _mySpawn;

    public void Init(PickupSpawn spawn)
    {
        _mySpawn = spawn;
    }

    private void OnDestroy()
    {
        _mySpawn.EnableSpawning();
    }
}
