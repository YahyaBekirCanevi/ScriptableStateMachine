using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class JumpService
{
    [SerializeField] private AnimationCurve jumpCurve;
    [SerializeField, Tooltip("How long does the jump will take? (in seconds)")] private float jumpTime = 2;
    [SerializeField, ReadOnly] private float verticalForceMagnitude;
    public float VerticalForceMagnitude { get => verticalForceMagnitude; }
    public JumpService() { }

    /// if jump occurs start coroutine 
    /// then use {VerticalForceMagnitude} in relative ways
    public IEnumerator Jump(float jumpStrength, Func<bool> afterJump, float forceMultiplier = 1)
    {
        float time = 0;
        verticalForceMagnitude = 0;
        while (time <= jumpTime && verticalForceMagnitude < jumpStrength)
        {
            verticalForceMagnitude = Mathf.Lerp(
                verticalForceMagnitude,
                jumpStrength * forceMultiplier,
                jumpCurve.Evaluate(time / jumpTime)
            );
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        verticalForceMagnitude = 0;
        afterJump();
    }
}