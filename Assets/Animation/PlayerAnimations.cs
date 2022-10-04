using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _deathParticles;

    private WormController _myWorm;

    public void Init(WormController wormController)
    {
        _myWorm = wormController;
    }

    public void PlayJumpAnimation()
    {
            _animator.SetTrigger("JumpTrigger");
    }
    
    public void PlayDamageAnimation()
    {
        _animator.SetTrigger("DamageTrigger");
    }

    public void PlayDeathAnimation()
    {
        _animator.SetTrigger("DeathTrigger");
    }

    public void SpawnDeathParticles()
    {
        _deathParticles.SetActive(true);
    }

    public void Die()
    {
        _myWorm.Die();
    }

    public void PlaySquishAnimation()
    {
        //_animator.SetTrigger("SquishTrigger");
        _animator.Play("JumpSquish");
    }

  
}


