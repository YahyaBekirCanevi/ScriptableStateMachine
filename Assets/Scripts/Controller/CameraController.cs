using UnityEngine;

[RequireComponent(typeof(CameraModel))]
public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private PlayerMotionStateService playerMotionStateService;
    private CameraModel cameraModel;
    private Camera cam;
    private float MouseX { get; set; }
    private float MouseY { get; set; }
    Vector3 velocity = Vector3.zero;
    private void Awake()
    {
        cameraModel = GetComponent<CameraModel>();
        cam = transform.GetChild(0).GetComponent<Camera>();
    }
    private void Update()
    {
        GetInputs();
        Rotate();
    }
    private void LateUpdate()
    {
        FollowPlayer();
    }

    private void GetInputs()
    {
        MouseX += Input.GetAxis("Mouse X") * cameraModel.XRotationSpeed;
        MouseY -= Input.GetAxis("Mouse Y") * cameraModel.YRotationSpeed;
        MouseY = Mathf.Clamp(MouseY, cameraModel.ClampMin, cameraModel.ClampMax);
    }
    float time = 1;
    private void FollowPlayer()
    {
        Vector3 desiredPosition = playerTransform.position + cameraModel.Offset;
        transform.position = desiredPosition;

        float distanceToPlayer = cameraModel.MaxDistanceFromPlayer;

        time += Time.deltaTime * cameraModel.FollowSpeed * (playerMotionStateService.State == CharacterState.idle ? -1 : 1);
        time = Mathf.Clamp(time, 0, 1);
        Vector3 movementDistance = Vector3.back * distanceToPlayer;
        cam.transform.localPosition = Vector3.Lerp(
            cameraModel.CameraPosition,
            cameraModel.CameraPosition + movementDistance,
            time
        );
    }
    private void Rotate()
    {
        transform.rotation = Quaternion.Euler(MouseY, MouseX, 0);
    }
}
