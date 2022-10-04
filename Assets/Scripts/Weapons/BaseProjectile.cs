using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    private Vector3 _rotation;
    private Rigidbody _rb;
    [HideInInspector] public float blastRadius;
    [HideInInspector] public int baseDamage;
    [HideInInspector] public int fixedDamage;

    [HideInInspector] public GameObject shootingPlayer;
    [HideInInspector] public SoundManager soundManager;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        soundManager = SoundManager.GetInstance();
        soundManager.PlayLaunchSound();
    }


    void Update()
    {
        _rotation = _rb.velocity;
        _rotation.Normalize();

        Vector3 newDirection = Vector3.RotateTowards(transform.position, _rotation, 10F, 10F);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    public void Init(GameObject player, float blastRadius, int baseDamage, int fixedDamage)
    {
        shootingPlayer = player;
        this.blastRadius = blastRadius;
        this.baseDamage = baseDamage;
        this.fixedDamage = fixedDamage;
        TurnsManager.OnTurnEnd += RemoveThis;
    }
    private void RemoveThis()
    {
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        TurnsManager.OnTurnEnd -= RemoveThis;
    }
}
