using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Animator _animator;

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

    public void Die()
    {
        _myWorm.Die();
    }
}


