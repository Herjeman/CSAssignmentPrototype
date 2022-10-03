using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherWeapon : BaseWeapon
{
  //  [SerializeField] private GameObject _muzzleFlash;
    public override GameObject Shoot()
    {
        
        //SpawnMuzzleFlashEffect();
        RaycastHit hit;
        SoundManager.GetInstance().PlayshotgunShotSound();

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
    
    // private void SpawnMuzzleFlashEffect()
    // {
    //     Instantiate(_muzzleFlash, transform.position, Quaternion.identity);
    // }
}
