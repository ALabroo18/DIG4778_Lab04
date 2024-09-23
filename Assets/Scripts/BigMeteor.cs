using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMeteor : Meteor
{
    // The number of times the meteor has been hit.
    private int hitCount = 0;

    // The speed at which the big meteor moves.
    private float bigMeteorSpeed = 0.5f;

    protected override void OnTriggerEnter2D(Collider2D whatIHit)
    {
        base.OnTriggerEnter2D (whatIHit);

        // If the laser has been hit, do the following.
        if (whatIHit.tag == "Laser")
        {
            // Add one to the hit count variable that determines when the big meteor is destroyed. Also, destroy the laser.
            hitCount++;
            Destroy(whatIHit.gameObject);

            // If the hit count is 5 or above, play the big meteor explosion sound, create a screen shake,
            // reset the camera's FOV (zoom back in), and destroy the big meteor.
            if (hitCount >= 5)
            {
                AudioManager.instance.PlayBigMeteorDestroy(transform.position, 1f);
                CameraShakeManager.instance.ScreenShakeFromProfile(profile, impulseSource, profile.impulseForce);
                CinemachineManager.instance.ResetFOV();
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

    protected override void MeteorMovement()
    {
        // Move the big meteor down at its move speed.
        transform.Translate(Vector3.down * Time.deltaTime * bigMeteorSpeed);

        // The base version of this function checks to see if the meteor is located at the point where they will be destroyed.
        base.MeteorMovement();
    }
}
