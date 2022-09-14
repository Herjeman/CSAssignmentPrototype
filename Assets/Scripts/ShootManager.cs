using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ShootManager
{
    public static void Shoot(Vector3 origin , Vector3 direction,int damage)
    {
        RaycastHit output;
        bool hitSomething = Physics.Raycast(origin, direction, out output, Mathf.Infinity);

        Debug.Log(output);
        if (hitSomething && output.transform.gameObject.tag == "Player")
        {
            Debug.Log($"Hit {output.transform.name} at position {output.point}");
            output.transform.gameObject.GetComponent<PlayerController>().stats.takeDamage(damage);
        }
        else if (hitSomething)
        {
            Debug.Log($"Hit {output.transform.gameObject.tag} at {output.point}, this is when destructible environments would be cool...");
        }
        else
        {
            Debug.Log("Hit nothing");
        }
    }
}
