using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{

    private GameObject[] _equipSlots;
    private int _equipIndex;

    public Inventory(GameObject bazooka, GameObject otherWeapon)
    {
        _equipSlots = new GameObject[2];
        _equipSlots[0] = bazooka;
        _equipSlots[1] = otherWeapon;

        otherWeapon.SetActive(false);
    }

    public int EquipNextWeapon()
    {
        _equipSlots[_equipIndex].SetActive(false);
        _equipIndex++;
        if (_equipIndex >= _equipSlots.Length)
        {
            _equipIndex = 0;
        }
        _equipSlots[_equipIndex].SetActive(true);
        return _equipIndex;
    }
}
