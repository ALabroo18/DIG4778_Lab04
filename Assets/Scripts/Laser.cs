using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    // Speed that the laser travels at.
    private float laserSpeed = 8f;

    void Update()
    {
        LaserMovement();
    }

    // Control the movement of the laser.
    private void LaserMovement()
    {
        // Translate the laser upwards at a speed set by the laserSpeed variable.
        transform.Translate(Vector3.up * Time.deltaTime * laserSpeed);

        // If the laser goes outside the bounds of the screen, destroy it.
        // Destroying the laser helps prevent a mass number of game objects existing that are not needed.
        if (transform.position.y > 11f)
        {
            Destroy(this.gameObject);
        }
    }
}
