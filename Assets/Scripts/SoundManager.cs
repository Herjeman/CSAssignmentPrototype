using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;


    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _jumpSound;
    [SerializeField] private AudioClip _landingSound;
    [SerializeField] private AudioClip _launchRocket;
    [SerializeField] private AudioClip _explosion;

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
        Debug.Log("PlayJumpSound");
        _audioSource.PlayOneShot(_jumpSound);
    }

    public void PlayLandingSound()
    {
        _audioSource.PlayOneShot(_landingSound);
    }

    public void PlayLaunchSound()
    {
        _audioSource.PlayOneShot(_launchRocket);
    }

    public void PlayExplosionSound()
    {
        _audioSource.PlayOneShot(_explosion);
    }
}
