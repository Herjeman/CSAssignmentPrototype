using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;


    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private AudioClip _jumpSound;
    [SerializeField] private float _jumpSoundVolume = 1;

    [SerializeField] private AudioClip _landingSound;
    [SerializeField] private float _landingSoundVolume = 1;

    [SerializeField] private AudioClip _launchRocket;
    [SerializeField] private float _launchRocketVolume = 1;

    [SerializeField] private AudioClip _explosion;
    [SerializeField] private float _explosionVolume = 1;
    
    [SerializeField] private AudioClip _shotgunShot;
    [SerializeField] private float _shotgunShotVolume = 1;

    [SerializeField] private AudioClip _shotgunReload;
    [SerializeField] private float _shotgunReloadVolume = 1;
    
    [SerializeField] private AudioClip _birdsSound;
    [SerializeField] private float _birdsSoundVolume = 1;

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

    private void Start()
    {
        PlayBirdSounds();
    }

    public static SoundManager GetInstance()
    {
        return instance;
    }

    public void PlayJumpSound()
    {
        _audioSource.PlayOneShot(_jumpSound, _jumpSoundVolume);
    }

    public void PlayLandingSound()
    {
        _audioSource.PlayOneShot(_landingSound, _landingSoundVolume);
    }

    public void PlayLaunchSound()
    {
        _audioSource.PlayOneShot(_launchRocket, _launchRocketVolume);
    }

    public void PlayExplosionSound()
    {
        _audioSource.PlayOneShot(_explosion, _explosionVolume);
    }

    public void PlayShotgunShotSound()
    {
        _audioSource.PlayOneShot(_shotgunShot, _shotgunShotVolume);
    }
    
    public void PlayShotgunReloadSound()
    {
        _audioSource.PlayOneShot(_shotgunReload, _shotgunReloadVolume);
    }
    
    public void PlayBirdSounds()
    {
        _audioSource.PlayOneShot(_birdsSound, _birdsSoundVolume);
    }
}
