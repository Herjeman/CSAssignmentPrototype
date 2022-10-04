using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : BaseWeapon
{
    [SerializeField] private GameObject _muzzleFlash;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _screenShakeIntensity;
    [SerializeField] private float _screenShakeDuration;

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

        CameraOrbit.GetInstance().ApplyScreenShake(_screenShakeIntensity, _screenShakeDuration);
        return new GameObject();
        
    }

    public override void StartCharge()
    {
        SoundManager.GetInstance().PlayShotgunReloadSound();
    }
    
    private void SpawnMuzzleFlashEffect()
    {
        if (!_isCreated)
        {
             GameObject currentShot  =  Instantiate(_muzzleFlash, _attackPoint.position, Quaternion.identity);
            _isCreated = true;
        }
    }
}
