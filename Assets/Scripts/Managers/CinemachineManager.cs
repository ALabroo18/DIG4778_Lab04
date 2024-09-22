using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineManager : MonoBehaviour
{
    // Singleton for easy referencing.
    public static CinemachineManager instance;

    private CinemachineVirtualCamera cVS;

    // Variables related to the changing of the cinemachine's FOV.
    private float zoomSpeed = 0.35f;
    private float originalFOV = 60f;
    private float newFOV = 60f; // Default of 60 to prevent FOV change when game begins.

    private void Awake()
    {
        // Set singleton reference.
        if (instance == null)
        {
            instance = this;
        }        
    }

    private void Start()
    {
        // Set ref.
        cVS = GetComponent<CinemachineVirtualCamera>();

        // Set the following target of the virtual camera to the player that was spawned by game manager.
        cVS.Follow = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        // Constantly lerp between the current FOV and the newFOV.
        cVS.m_Lens.FieldOfView = Mathf.Lerp(cVS.m_Lens.FieldOfView, newFOV, Time.deltaTime * zoomSpeed);
    }

    // Called in GameManager to increase the cam's FOV when a big meteor spawns.
    public void ChangeFOV(float brandNewFOV)
    {
        newFOV = brandNewFOV;
    }
    
    // Called in the BigMeteor script when it is destroyed to set the cam's FOV back to the original.
    public void ResetFOV()
    {
        newFOV = originalFOV; 
    }
}
