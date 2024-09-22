using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Laser weapon the player spawns when pressing the attacking/shooting button.
    [SerializeField] private GameObject laserPrefab;

    // Speed the player moves at.
    private float speed = 6f;

    // The edge bounds of the screen that determine when the player is sent to the other side.
    private float horizontalScreenLimit = 10f;
    private float verticalScreenLimit = 6f;

    // Variables related to when the player is able to shoot again.
    private bool canShoot = true;
    private float cooldown = 1f;

    // Movement variables.
    private float horizontalMovement;
    private float verticalMovement;
    
    // Player's audio source.
    private AudioSource playerAS;

    private void Start()
    {
        playerAS = GetComponent<AudioSource>();
    }

    void Update()
    {
        Movement();
        Shooting();
        ScreenLimitChecks();
    }

    void Movement()
    {
        // Set movement variables.
        horizontalMovement = InputManager.instance.moveInput.x;
        verticalMovement = InputManager.instance.moveInput.y;

        // Translate the player based on the x and y values produced by their moveInput. Additionally, move them based on their speed variable.
        transform.Translate(new Vector3(horizontalMovement, verticalMovement, 0) * Time.deltaTime * speed);
        
        // Player move sound that plays when they are moving. This is done in the player script because it works much better
        // than doing it in the audio manager script.
        if ((horizontalMovement != 0 || verticalMovement != 0) && !GameManager.instance.gameOver && !playerAS.isPlaying)
        {
            playerAS.Play();
        }
        else if ((horizontalMovement == 0 && verticalMovement == 0) || GameManager.instance.gameOver)
        {
            playerAS.Stop();
        }
    }

    void ScreenLimitChecks()
    {
        // If the player reaches the horizontal or vertical screen limits, put them at the opposite end.
        // For example, if player hits the left screen limit, they will be put at the right side.
        if (transform.position.x > horizontalScreenLimit || transform.position.x <= -horizontalScreenLimit)
        {
            transform.position = new Vector3(transform.position.x * -1f, transform.position.y, 0);
        }
        if (transform.position.y > verticalScreenLimit || transform.position.y <= -verticalScreenLimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y * -1, 0);
        }
    }

    void Shooting()
    {
        // Check to see if the player pressed the attack button and is able to shoot.
        if (InputManager.instance.attackInput && canShoot)
        {
            // If they are, play a firing sound, spawn the laser, turn off their ability to fire, and begin the cooldown that will let them fire again.
            AudioManager.instance.PlayPlayerShoot(transform.position, 1f);
            Instantiate(laserPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            canShoot = false;
            StartCoroutine("Cooldown");
        }
    }

    // Coroutine that allows the player to fire again after a set amount of time.
    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldown);
        canShoot = true;
    }
}
