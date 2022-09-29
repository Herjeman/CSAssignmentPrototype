using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    private static PickupManager instance;
    [SerializeField] private GameObject _healthPack;
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
        TurnsManager.OnTurnStart += SpawnHealthPack;
    }

    public static PickupManager GetInstance()
    {
        return instance;
    }

    private void SpawnHealthPack()
    {
        PickupSpawn spawnpoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)];
        spawnpoint.SpawnPickup(_healthPack);
    }

    private void OnDestroy()
    {
        TurnsManager.OnTurnStart -= SpawnHealthPack;
    }
}
