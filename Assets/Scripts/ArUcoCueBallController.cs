using UnityEngine;

public class ArUcoCueBallController : MonoBehaviour
{
    public ArUcoPoseReceiver arUcoPoseReceiver;
    public GameObject cueBall;
    public float positionMultiplier = 1.0f;
    public float positionResponseSpeed = 15f;
    [Range(0, 1)] public float positionSmoothing = 0.3f;
    private Vector3 currentPosVelocity;

    void Update()
    {
        if (arUcoPoseReceiver != null && cueBall != null)
        {
            // Get the position from ArUcoPoseReceiver
            Vector3 targetPosition = arUcoPoseReceiver.transform.position * positionMultiplier;

            // Keep the Y position of the cue ball fixed (assuming it's on the table)
            targetPosition.y = cueBall.transform.position.y;

            // Smoothly move the cue ball to the target position
            cueBall.transform.position = Vector3.SmoothDamp(
                cueBall.transform.position,
                targetPosition,
                ref currentPosVelocity,
                positionSmoothing,
                positionResponseSpeed
            );
        }
    }
}