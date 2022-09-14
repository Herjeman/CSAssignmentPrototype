using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    void Update()
    {       
        RaycastHit result;
        bool thereWasHit = Physics.Raycast(transform.position, transform.forward, out result, Mathf.Infinity);

        if (thereWasHit)
        {
            Debug.Log($"There was hit at {result.point}, ray hit { result.transform.name} game object.");
        }

        Debug.DrawRay(transform.position, transform.forward * 50f, Color.red);
    }
}
