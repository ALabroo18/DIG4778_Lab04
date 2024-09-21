using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMeteor : MonoBehaviour
{
    private int hitCount = 0;

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
        transform.Translate(Vector3.down * Time.deltaTime * 0.5f);

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
        }
        else if (whatIHit.tag == "Laser")
        {
            hitCount++;
            Destroy(whatIHit.gameObject);

            if (hitCount >= 5)
            {
                AudioManager.instance.PlayBigMeteorDestroy(transform.position, 1f);
                CameraShakeManager.instance.ScreenShakeFromProfile(profile, impulseSource, profile.impulseForce);
                Destroy(this.gameObject);
            }
            else
            {
                AudioManager.instance.PlayBigMeteorHit(transform.position, 1f);
                CameraShakeManager.instance.ScreenShakeFromProfile(profile, impulseSource, 0.25f);
            }
        }
    }
}
