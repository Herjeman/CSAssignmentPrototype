using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : BaseWeapon
{
    [SerializeField] private GameObject _muzzleFlash;
    [SerializeField] private Transform _attackPoint;
    private bool _isCreated;
    public override GameObject Shoot()
    {
        _isCreated = false;
        RaycastHit hit;
        SoundManager.GetInstance().PlayShotgunShotSound();
        SpawnMuzzleFlashEffect();

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
        SoundManager.GetInstance().PlayShotgunReloadSound();
    }
    
    public void SpawnMuzzleFlashEffect()
    {
        if (!_isCreated)
        {
             GameObject currentShot  =  Instantiate(_muzzleFlash, _attackPoint.position, Quaternion.identity);
            _isCreated = true;
        }
    }
}
