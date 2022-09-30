using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    public abstract GameObject Shoot();

    public abstract void Equip();

    public abstract void UnEquip();
}
