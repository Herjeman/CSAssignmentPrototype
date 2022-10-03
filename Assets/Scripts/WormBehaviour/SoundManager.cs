using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;


    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _jumpSound;
    [SerializeField] private AudioClip _landingSound;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static SoundManager GetInstance()
    {
        return instance;
    }

    public void PlayJumpSound()
    {
        _audioSource.PlayOneShot(_jumpSound);
    }

    public void PlayLandingSound()
    {
        _audioSource.PlayOneShot(_landingSound);
    }
}
