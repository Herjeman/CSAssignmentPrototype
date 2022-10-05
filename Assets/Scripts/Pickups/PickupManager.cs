using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    private static PickupManager instance;
    [SerializeField] private List<GameObject> _pickups;
    [SerializeField] private List<PickupSpawn> _spawnPoints;
    [SerializeField] private TurnsManager _turnsManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        TurnsManager.OnTurnStart += SpawnRandomPickup;
    }

    public static PickupManager GetInstance()
    {
        return instance;
    }

    private void SpawnRandomPickup()
    {
        if (_turnsManager.GetTurnNumber() > _turnsManager.GetNumberOfPlayers())
        {
            PickupSpawn spawnpoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)];
            spawnpoint.SpawnPickup(_pickups[Random.Range(0, _pickups.Count)]);
        }
    }

    private void OnDestroy()
    {
        TurnsManager.OnTurnStart -= SpawnRandomPickup;
    }
}
