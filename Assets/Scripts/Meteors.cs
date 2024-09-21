using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Meteors : MonoBehaviour
{
    public int speed;

    // Once the meteor reaces the below y coordinate, it will be destroyed.
    private float bottomScreenLimit = -11f;

    public abstract void MeteorMovement();

    public void DestroyThySelf()
    {
        // If the meteor exits the bottom edge of the screen, destroy it to prevent build up of unneeded game objects.
        if (transform.position.y < bottomScreenLimit)
        {
            Destroy(this.gameObject);
        }
    }
}
