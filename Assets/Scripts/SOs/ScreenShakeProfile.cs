using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

// Enum that contains the possible impulse shapes.
public enum ImpulseShape
{
    Recoil,
    Bump,
    Explosion,
    Rumble
}

// Adds an item to the create asset menu that allows for users in the editor to create new ScreenShake profiles.
[CreateAssetMenu(menuName ="ScreenShake/New Profile")]
public class ScreenShakeProfile : ScriptableObject
{
    // The impulse source settings that are adjustable.
    [Header("Impulse Source Settings")]
    public float impulseTime = 0.5f;
    public float impulseForce = 1f;
    public Vector3 defaultVelocity = new Vector3(0f, -1f, 0f);
    public AnimationCurve impulseCurve;
    public ImpulseShape impulseShape;

    // The impulse listener settings that are adjustable.
    [Header("Impulse Listener Settings")]
    public float listenerAmplitude = 1f;
    public float listenerFrequency = 1f;
    public float listenerDuration = 1f;
}
