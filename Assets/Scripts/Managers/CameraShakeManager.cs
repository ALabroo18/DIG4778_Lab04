using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShakeManager : MonoBehaviour
{
    // Singleton reference for easy referencing in other scripts.
    public static CameraShakeManager instance;

    // The force of the impulse shake.
    private float cameraShakeForce = 1f;

    // Reference to impulse listener to change its settings.
    [SerializeField] private CinemachineImpulseListener impulseListener;

    // Allows for the accessing and subsequent adjusting of impulse settings.
    private CinemachineImpulseDefinition impulseDefinition;

    private void Awake()
    {
        // Assign singleton reference.
        if (instance == null)
        {
            instance = this;
        }
    }

    // Shake the camera.
    public void CameraShake(CinemachineImpulseSource impulseSource, float shakeForce)
    {
        cameraShakeForce = shakeForce;
        impulseSource.GenerateImpulseWithForce(cameraShakeForce);
    }

    // Shake the camera based on the profile made in the editor.
    public void ScreenShakeFromProfile(ScreenShakeProfile profile, CinemachineImpulseSource impulseSource, float impulseForce)
    {
        // Apply settings of the profile to the impulse source and impulse listener.
        SetupScreenShakeSettings(profile, impulseSource);

        // Shake the screen.
        impulseSource.GenerateImpulseWithForce(impulseForce);
    }

    private void SetupScreenShakeSettings(ScreenShakeProfile profile, CinemachineImpulseSource impulseSource)
    {
        // Set impulse definition reference.
        impulseDefinition = impulseSource.m_ImpulseDefinition;

        // Set the impulse source settings.
        impulseDefinition.m_ImpulseDuration = profile.impulseTime;
        impulseSource.m_DefaultVelocity = profile.defaultVelocity;
        impulseDefinition.m_CustomImpulseShape = profile.impulseCurve;
        impulseDefinition.m_ImpulseShape = (CinemachineImpulseDefinition.ImpulseShapes)profile.impulseShape;

        // Set the impulse listener settings.
        impulseListener.m_ReactionSettings.m_AmplitudeGain = profile.listenerAmplitude;
        impulseListener.m_ReactionSettings.m_FrequencyGain = profile.listenerFrequency;
        impulseListener.m_ReactionSettings.m_Duration = profile.listenerDuration;
    }
}
