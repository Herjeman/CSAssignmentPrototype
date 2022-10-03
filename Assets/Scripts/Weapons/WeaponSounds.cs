using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Legacy
public class WeaponSounds : MonoBehaviour
{
   [SerializeField] private AudioClip shoot;
   [SerializeField] private AudioClip impact;
   [SerializeField]  private float volume1 = 1;
   [SerializeField] private float volume2 = 1;


    void Awake()
    {
        PlayShootEffect();
    }


    private void OnCollisionEnter(Collision collider)
    {
        PlayExplosion();
    }

    void PlayShootEffect()
    {
        AudioSource.PlayClipAtPoint(shoot, transform.position, volume1);
    }


    void PlayExplosion()
    {
        AudioSource.PlayClipAtPoint(impact, transform.position, volume2);
    }



}
