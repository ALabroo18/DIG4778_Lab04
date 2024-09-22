using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RegularMeteor : Meteor
{
    // The speed the meteor moves at.
    private float meteorSpeed = 2f;

    protected override void OnTriggerEnter2D(Collider2D whatIHit)
    {
        base.OnTriggerEnter2D (whatIHit);
        // If the meteor collides with the laser, do the following.
        if (whatIHit.tag == "Laser")
        {
            // Create a screenshake, play the meteor explosion sound, add one to the meteor count variable that determines when a big meteor is spawned,
            // destory the laser, and destroy the meteor.
            CameraShakeManager.instance.ScreenShakeFromProfile(profile, impulseSource, profile.impulseForce);
            AudioManager.instance.PlaySmallMeteorDestroy(transform.position, 0.75f);
            GameManager.instance.meteorCount++;
            Destroy(whatIHit.gameObject);
            Destroy(this.gameObject);
        }
    }

    protected override void MeteorMovement()
    {
        // Move the meteor down based on its move speed.
        transform.Translate(Vector3.down * Time.deltaTime * meteorSpeed);

        // The base version of this function checks to see if the meteor is located at the point where they will be destroyed.
        base.MeteorMovement();
    }
}
