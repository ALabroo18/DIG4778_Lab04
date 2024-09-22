using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public abstract class Meteor : MonoBehaviour
{
    // Once the meteor reaces the below y coordinate, it will be destroyed.
    private float bottomScreenLimit = -11f;

    // Reference to impulse source component on object.
    protected CinemachineImpulseSource impulseSource;

    // Screen shake profile created for this object.
    [SerializeField] protected ScreenShakeProfile profile;

    void Start()
    {
        // Set reference.
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    private void Update()
    {
        MeteorMovement();
    }

    // Movement speed is different for each meteor, so allow the function to be overridden.
    protected virtual void MeteorMovement()
    {
        DestroyThySelf();
    }

    private void DestroyThySelf()
    {
        // If the meteor exits the bottom edge of the screen, destroy it to prevent build up of unneeded game objects.
        if (transform.position.y < bottomScreenLimit)
        {
            Destroy(this.gameObject);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D whatIHit)
    {
        // If the meteor collides with the player, do the following.
        if (whatIHit.tag == "Player")
        {
            // Create a screenshake, set the game to over, play the player's death sound, destroy the player, and destroy the meteor.
            CameraShakeManager.instance.ScreenShakeFromProfile(profile, impulseSource, 1f);
            GameManager.instance.gameOver = true;
            AudioManager.instance.PlayPlayerDeath(transform.position, 1f);
            Destroy(whatIHit.gameObject);
            Destroy(this.gameObject);
        }
    }
}
