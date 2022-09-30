using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    public abstract void StartCharge();

    public abstract GameObject Shoot();
}
