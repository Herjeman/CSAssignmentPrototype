using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void PlayJumpAnimation()
    {
            _animator.SetTrigger("JumpTrigger");
    }   
}


