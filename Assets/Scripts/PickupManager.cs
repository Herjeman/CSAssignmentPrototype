using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    private static PickupManager instance;
    [SerializeField] GameObject pickupPrefab;

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

    public static PickupManager GetInstance()
    {
        return instance;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) // move to inputManager
        {
            GameObject newPickup = Instantiate(pickupPrefab);
            newPickup.transform.position = Vector3.zero;
        }
    }
}
