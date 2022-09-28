using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats
{
    private int _hp;
    private int _maxHp;
    
   

    public void TakeDamage(int damage)
    {
        _hp -= damage;
    }

    public int GetHp()
    {
        return _hp;
    }

    public float GetNormalizedHp()
    {
        return _hp / _maxHp;
    }
    
    public void SetHp(int newHp)
    {
        _hp = newHp;
    }

}
