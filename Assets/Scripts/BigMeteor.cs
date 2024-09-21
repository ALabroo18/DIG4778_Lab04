using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMeteor : MonoBehaviour
{
    // The number of times the meteor has been hit.
    private int hitCount = 0;

    // The speed at which the big meteor moves.
    private float bigMeteorSpeed = 0.5f;

    // Once the meteor reaces the below y coordinate, it will be destroyed.
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
        BigMeteorMovement();
    }

    private void OnTriggerEnter2D(Collider2D whatIHit)
    {
        // If the player is hit, do the following.
        if (whatIHit.tag == "Player")
        {
            // Create a screen shake, set the game to over, play the player death sound, and destroy the player.
            CameraShakeManager.instance.ScreenShakeFromProfile(profile, impulseSource, 1f);
            GameObject.Find("GameManager").GetComponent<GameManager>().gameOver = true;
            AudioManager.instance.PlayPlayerDeath(transform.position, 1f);
            Destroy(whatIHit.gameObject);
        }

        // If the laser has been hit, do the following.
        else if (whatIHit.tag == "Laser")
        {
            // Add one to the hit count variable that determines when the big meteor is destroyed. Also, destroy the laser.
            hitCount++;
            Destroy(whatIHit.gameObject);

            // If the hit count is 5 or above, play the big meteor explosion sound, create a screen shake, and destroy the big meteor.
            if (hitCount >= 5)
            {
                AudioManager.instance.PlayBigMeteorDestroy(transform.position, 1f);
                CameraShakeManager.instance.ScreenShakeFromProfile(profile, impulseSource, profile.impulseForce);
                Destroy(this.gameObject);
            }
            // If the hit count is NOT above or equal to 5 (it's 4 or less), play the big meteor hit sound and create a screenshake.
            else
            {
                AudioManager.instance.PlayBigMeteorHit(transform.position, 1f);
                CameraShakeManager.instance.ScreenShakeFromProfile(profile, impulseSource, 0.25f);
            }
        }
    }

    private void BigMeteorMovement()
    {
        // Move the big meteor down at its move speed.
        transform.Translate(Vector3.down * Time.deltaTime * bigMeteorSpeed);

        // If the big meteor exits the bottom edge of the screen, destroy it to prevent build up of unneeded game objects.
        if (transform.position.y < bottomScreenLimit)
        {
            Destroy(this.gameObject);
        }
    }
}
