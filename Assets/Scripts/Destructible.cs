using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] int _startingHp;
    [SerializeField] GameObject DestructionParticleEffect;
    public Stats stats;

    private void Awake()
    {
        stats = new Stats(_startingHp);
    }

    public void TakeDamage(int damage)
    {
        stats.TakeDamage(damage);
        if (stats.GetHp() <= 0)
        {
            //ParticleEffect();
            Destroy(this.gameObject);
        }
    }

    //private void ParticleEffect()
    //{

    //    Instantiate(DestructionParticleEffect, transform.position, Quaternion.identity); 

    //}


}

