using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherWeapon : BaseWeapon
{
    public override GameObject Shoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {
            if (hit.transform.tag == "Player")
            {
                hit.transform.gameObject.GetComponent<WormController>().TakeDamage(75);
            }
        }
        return new GameObject();
    }

    public override void StartCharge()
    {
        Debug.Log("Shotgun goes, clink-clonk");
    }
}
