using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Singleton reference for easy referencing in other scripts.
    public static AudioManager instance;

    [SerializeField] private AudioClip playerShoot;
    [SerializeField] private AudioClip smallMeteorDestroy;
    [SerializeField] private AudioClip bigMeteorDestroy;
    [SerializeField] private AudioClip bigMeteorHit;
    [SerializeField] private AudioClip playerDeath;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void PlayAudioClip(AudioClip clip, Vector3 position, float volume)
    {
        AudioSource.PlayClipAtPoint(clip, position, volume);
    }

    public void PlayPlayerShoot(Vector3 position, float volume)
    {
        PlayAudioClip(playerShoot, position, volume);
    }

    public void PlaySmallMeteorDestroy(Vector3 position, float volume)
    {
        PlayAudioClip(smallMeteorDestroy, position, volume);
    }

    public void PlayBigMeteorDestroy(Vector3 position, float volume)
    {
        PlayAudioClip(bigMeteorDestroy, position, volume);
    }

    public void PlayBigMeteorHit(Vector3 position, float volume)
    {
        PlayAudioClip(bigMeteorHit, position, volume);
    }

    public void PlayPlayerDeath(Vector3 position, float volume)
    {
        PlayAudioClip(playerDeath, position, volume);
        GetComponent<AudioSource>().Play();
    }
}
