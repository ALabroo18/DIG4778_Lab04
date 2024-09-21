using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Meteor : MonoBehaviour
{
    // The speed the meteor moves at.
    private float meteorSpeed = 2f;

    // Once the meteor reaches the below y coordinate, it will be destroyed.
    private float bottomScreenLimit = -11f;

    // Reference to impulse source component on object.
    private CinemachineImpulseSource impulseSource;

    // Screen shake profile created for this object.
    [SerializeField] private ScreenShakeProfile profile;

    void Start()
    {
        // Set reference.
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    void Update()
    {
        MeteorMovement();
    }

    private void OnTriggerEnter2D(Collider2D whatIHit)
    {
        // If the meteor collides with the player, do the following.
        if (whatIHit.tag == "Player")
        {
            // Create a screenshake, set the game to over, play the player's death sound, destroy the player, and destroy the meteor.
            CameraShakeManager.instance.ScreenShakeFromProfile(profile, impulseSource, 1f);
            GameObject.Find("GameManager").GetComponent<GameManager>().gameOver = true;
            AudioManager.instance.PlayPlayerDeath(transform.position, 1f);
            Destroy(whatIHit.gameObject);
            Destroy(this.gameObject);
        } 
        // If the meteor collides with the laser, do the following.
        else if (whatIHit.tag == "Laser")
        {
            // Create a screenshake, play the meteor explosion sound, add one to the meteor count variable that determines when a big meteor is spawned,
            // destory the laser, and destroy the meteor.
            CameraShakeManager.instance.ScreenShakeFromProfile(profile, impulseSource, profile.impulseForce);
            AudioManager.instance.PlaySmallMeteorDestroy(transform.position, 0.75f);
            GameObject.Find("GameManager").GetComponent<GameManager>().meteorCount++;
            Destroy(whatIHit.gameObject);
            Destroy(this.gameObject);
        }
    }

    private void MeteorMovement()
    {
        // Move the meteor down based on its move speed.
        transform.Translate(Vector3.down * Time.deltaTime * meteorSpeed);

        // If the meteor goes past the bottom edge of the screen, destroy it to prevent unnecessary object.
        if (transform.position.y < bottomScreenLimit)
        {
            Destroy(this.gameObject);
        }
    }
}
