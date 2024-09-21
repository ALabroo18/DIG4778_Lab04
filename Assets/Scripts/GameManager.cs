using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Prefabs that are spawned during runtime.
    public GameObject playerPrefab;
    public GameObject meteorPrefab;
    public GameObject bigMeteorPrefab;

    public bool gameOver = false;

    // meteorCount is used to determines when the big meteor will spawn.
    public int meteorCount = 0;

    void Start()
    {
        // Spawn the player and begin repeatedly spawning meteors.
        Instantiate(playerPrefab, transform.position, Quaternion.identity);
        InvokeRepeating("SpawnMeteor", 1f, 2f);
    }

    void Update()
    {
        // If the game is over, cancel the invokes and stop playing the background music.
        if (gameOver)
        {
            CancelInvoke();
            GetComponent<AudioSource>().Stop();
        }

        // If the player presses the restart button once the game is over, the game will reload. 
        if (InputManager.instance.restartInput && gameOver)
        {
            SceneManager.LoadScene("Week5Lab");
        }

        // If the meteor count is equal to 5, it is time to spawn a big ole meteor.
        if (meteorCount == 5)
        {
            BigMeteor();
        }
    }

    // Spawn a meteor at the top of the screen, but at a random spot horizontally.
    void SpawnMeteor()
    {
        Instantiate(meteorPrefab, new Vector3(Random.Range(-8, 8), 7.5f, 0), Quaternion.identity);
    }

    // Spawn a big meteor and reset the meteor count so another big meteor does not spawn until 5 meteors have been hit.
    void BigMeteor()
    {
        meteorCount = 0;
        Instantiate(bigMeteorPrefab, new Vector3(Random.Range(-8, 8), 7.5f, 0), Quaternion.identity);
    }
}
