using UnityEngine;

public class CameraModel : MonoBehaviour
{
    [SerializeField] private float followSpeed = 12;
    [SerializeField] private float xRotationSpeed = 8;
    [SerializeField] private float yRotationSpeed = 5;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 cameraPosition, aimedCameraPosition;
    [SerializeField, Range(-90, 0)] private float clampMin = -50;
    [SerializeField, Range(0, 90)] private float clampMax = 50;
    [SerializeField, Range(0, 20)] private float maxDistanceFromPlayer = 2;

    public float FollowSpeed { get => followSpeed; }
    public float XRotationSpeed { get => xRotationSpeed; }
    public float YRotationSpeed { get => yRotationSpeed; }
    public Vector3 Offset { get => offset; }
    public Vector3 CameraPosition { get => cameraPosition; }
    public Vector3 AimedCameraPosition { get => aimedCameraPosition; }
    public float ClampMin { get => clampMin; }
    public float ClampMax { get => clampMax; }
    public float MaxDistanceFromPlayer { get => maxDistanceFromPlayer; }
}