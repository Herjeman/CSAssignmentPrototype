using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats
{
    private int _hp;
    private int _maxHp;
    
   public Stats(int maxHp, int startingHp = 0)
    {
        _maxHp = maxHp;
        _hp = startingHp;
        if (_hp == 0)
        {
            _hp = _maxHp;
        }
    }


    public void TakeDamage(int damage)
    {
        _hp -= damage;
        Debug.Log($"Took {damage} points of damage, HP is now:{_hp}");
    }

    public int GetHp()
    {
        return _hp;
    }

    public float GetNormalizedHp()
    {
        return _hp * 1.0f / _maxHp * 1.0f;
    }
    
    public void SetHp(int newHp)
    {
        _hp = newHp;
        if (_hp > _maxHp)
        {
            _hp = _maxHp;
        }
    }
}
