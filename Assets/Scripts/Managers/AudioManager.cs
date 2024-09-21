using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Singleton reference for easy referencing in other scripts.
    public static AudioManager instance;

    // Audio clips that will be played.
    [SerializeField] private AudioClip playerShoot;
    [SerializeField] private AudioClip smallMeteorDestroy;
    [SerializeField] private AudioClip bigMeteorDestroy;
    [SerializeField] private AudioClip bigMeteorHit;
    [SerializeField] private AudioClip playerDeath;

    private void Awake()
    {
        // Set singleton reference if it hasn't been already.
        if (instance == null)
        {
            instance = this;
        }
    }

    // Play the specified audio clip at the given position and volume when the function is called.
    private void PlayAudioClip(AudioClip clip, Vector3 position, float volume)
    {
        AudioSource.PlayClipAtPoint(clip, position, volume);
    }

    
 /*  Below are functions that are called from other scripts to play an audio clip at the object's position.
     To keep all audio clips within the audio manager, these functions are called rather than the above one being called using
     an audio clip that is set in the object that calls one of the below functions.
     For example, rather than adding an audio clip to the player script, then assigning it in the inspector, and then inputting it when calling
     the PlayAudioClip() function, the player script only needs to call the PlayPlayerShoot() function and input the player's location and 
     volume to play the clip at.
     Doing this allows all the audio clips to be stored in the audio manager script, which is attached to the audio manager object in the scene.
 */

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

    // This function differs as it also plays the game over audio source that is attached to the object this script is on.
    public void PlayPlayerDeath(Vector3 position, float volume)
    {
        PlayAudioClip(playerDeath, position, volume);
        GetComponent<AudioSource>().Play();
    }
}
