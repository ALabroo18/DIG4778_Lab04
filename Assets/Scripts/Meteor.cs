using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Meteor : MonoBehaviour
{
    // Reference to impulse source component on object.
    private CinemachineImpulseSource impulseSource;

    // Screen shake profile created for this object.
    [SerializeField] private ScreenShakeProfile profile;

    void Start()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * 2f);

        if (transform.position.y < -11f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D whatIHit)
    {
        if (whatIHit.tag == "Player")
        {
            CameraShakeManager.instance.ScreenShakeFromProfile(profile, impulseSource, 1f);
            GameObject.Find("GameManager").GetComponent<GameManager>().gameOver = true;
            AudioManager.instance.PlayPlayerDeath(transform.position, 1f);
            Destroy(whatIHit.gameObject);
            Destroy(this.gameObject);
        } else if (whatIHit.tag == "Laser")
        {
            CameraShakeManager.instance.ScreenShakeFromProfile(profile, impulseSource, profile.impulseForce);
            AudioManager.instance.PlaySmallMeteorDestroy(transform.position, 0.75f);
            GameObject.Find("GameManager").GetComponent<GameManager>().meteorCount++;
            Destroy(whatIHit.gameObject);
            Destroy(this.gameObject);
        }
    }
}
