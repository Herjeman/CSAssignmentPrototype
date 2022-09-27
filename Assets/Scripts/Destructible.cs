using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] int _startingHp;
    public Stats stats;

    private void Awake()
    {
        stats = new Stats();
        stats.SetHp(_startingHp);
    }

    public void TakeDamage(int damage)
    {
        stats.TakeDamage(damage);
        if (stats.GetHp() < 0)
        {
            Destroy(this.gameObject);
        }
    }
}
