using UnityEngine;

[RequireComponent(typeof(CameraModel))]
public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private PlayerMotionStateService playerMotionStateService;
    [SerializeField] private PlayerAttackStateService playerAttackStateService;
    private CameraModel cameraModel;
    private Camera cam;
    private float MouseX { get; set; }
    private float MouseY { get; set; }
    Vector3 velocity = Vector3.zero;
    private void Awake()
    {
        Cursor.visible = false;
        cameraModel = GetComponent<CameraModel>();
        cam = transform.GetChild(0).GetComponent<Camera>();
    }

    private void Update()
    {
        LockCursor();
        GetInputs();
        Rotate();
    }
    private void LateUpdate()
    {
        FollowPlayer();
    }
    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void GetInputs()
    {
        MouseX += Input.GetAxis("Mouse X") * cameraModel.XRotationSpeed;
        MouseY -= Input.GetAxis("Mouse Y") * cameraModel.YRotationSpeed;
        MouseY = Mathf.Clamp(MouseY, cameraModel.ClampMin, cameraModel.ClampMax);
    }
    float time1 = 1;
    float time2 = 1;
    private void FollowPlayer()
    {
        Vector3 desiredPosition = playerTransform.position + cameraModel.Offset;
        transform.position = desiredPosition;

        time1 += Time.deltaTime * cameraModel.FollowSpeed * (playerMotionStateService.State == CharacterState.idle ? -1 : 1);
        time1 = Mathf.Clamp(time1, 0, 1);

        time2 += Time.deltaTime * 2 * (playerAttackStateService.Charging ? 1 : -1);
        time2 = Mathf.Clamp(time2, 0, 1);

        Vector3 desiredCameraPosition = Vector3.Lerp(cameraModel.CameraPosition, cameraModel.AimedCameraPosition, time2);

        Vector3 movementDistance = (playerAttackStateService.Charging ? (Vector3.forward * .4f) : Vector3.back) * cameraModel.MaxDistanceFromPlayer;
        cam.transform.localPosition = Vector3.Lerp(
            desiredCameraPosition,
            desiredCameraPosition + movementDistance,
            time1
        );
    }
    private void Rotate()
    {
        transform.rotation = Quaternion.Euler(MouseY, MouseX, 0);
    }
}
