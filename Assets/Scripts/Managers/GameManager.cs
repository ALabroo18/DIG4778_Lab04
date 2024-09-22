using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton reference for easy referencing in other scripts.
    public static GameManager instance;

    // Prefabs that are spawned during runtime.
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject meteorPrefab;
    [SerializeField] private GameObject bigMeteorPrefab;

    private bool _gameOver;
    public bool gameOver
    {
        get { return _gameOver; }
        set { _gameOver = value; }
    }

    // meteorCount is used to determine when the big meteor will spawn.
    private int _meteorCount = 0;
    public int meteorCount
    {
        get { return _meteorCount; }
        set { _meteorCount = value; }
    }

    private float zoomOutFOV = 80f;

    private void Awake()
    {
        // Set singleton ref.
        if (instance == null)
        {
            instance = this;
        }

        // Spawn the player and begin repeatedly spawning meteors.
        Instantiate(playerPrefab, transform.position, Quaternion.identity);
        InvokeRepeating("SpawnMeteor", 1f, 2f);
    }

    void Start()
    {
        
    }

    void Update()
    {
        // If the game is over, cancel the invokes and stop playing the background music.
        if (_gameOver)
        {
            CancelInvoke();
            GetComponent<AudioSource>().Stop();
        }

        // If the player presses the restart button once the game is over, the game will reload. 
        if (InputManager.instance.restartInput && _gameOver)
        {
            SceneManager.LoadScene("Week5Lab");
        }

        // If the meteor count is equal to 5, it is time to spawn a big ole meteor.
        if (_meteorCount == 5)
        {
            BigMeteor();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            _meteorCount = 5;
        }
    }

    // Spawn a meteor at the top of the screen, but at a random spot horizontally.
    void SpawnMeteor()
    {
        Instantiate(meteorPrefab, new Vector3(Random.Range(-8, 8), 7.5f, 0), Quaternion.identity);
    }

    // Spawn a big meteor and reset the meteor count so another big meteor does not spawn until 5 more meteors have been hit.
    // Also, begin zooming out the camera.
    void BigMeteor()
    {
        _meteorCount = 0;
        Instantiate(bigMeteorPrefab, new Vector3(Random.Range(-8, 8), 7.5f, 0), Quaternion.identity);
        CinemachineManager.instance.ChangeFOV(zoomOutFOV);
    }
}
