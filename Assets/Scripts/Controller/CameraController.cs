using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public CameraModel Camera;
    public PlayerController Target;
    public Transform Cam;
    private Camera _camera;
    float _distanceTime = 0;
    Vector2 rotation = Vector2.zero;
    public bool Aim { get => Target != null ? Target.Animator.GetBool("charge") : false; }
    private void Awake()
    {
        PlayerController.PlayerInput.CharacterControls.Look.started += PlayerInputActions_OnLook;
        PlayerController.PlayerInput.CharacterControls.Look.performed += PlayerInputActions_OnLook;
        PlayerController.PlayerInput.CharacterControls.Look.canceled += PlayerInputActions_OnLook;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _camera = Cam.GetComponent<Camera>();
    }
    private void PlayerInputActions_OnLook(InputAction.CallbackContext context)
    {
        Vector2 delta = context.ReadValue<Vector2>() * .04f;
        delta.y *= -1;
        rotation += delta * Camera.Sensitivity;
        rotation.y = Math.Clamp(rotation.y, Camera.ClampMin, Camera.ClampMax);
    }
    private void Update()
    {
        transform.rotation = Quaternion.Euler(rotation.y, rotation.x, 0);
    }
    private void LateUpdate()
    {
        transform.position = Target.transform.position + Camera.Offset;
        Vector3 localPosition = Cam.localPosition;
        if (Aim)
        {
            localPosition = Camera.AimedCameraPosition;
        }
        else
        {
            float direction = Target.MoveSpeed > .1f ? 1 : -1;
            _distanceTime += Time.deltaTime * direction * Camera.FollowSpeed;
            _distanceTime = Math.Clamp(_distanceTime, 0, 1);
            localPosition = Vector3.Lerp(
                Camera.CameraPosition,
                Camera.CameraPosition - Vector3.forward * Camera.MaxDistanceFromPlayer * (Target.IsRunning ? 2 : 1),
                _distanceTime
            );
        }
        Cam.localPosition = Vector3.Lerp(Cam.localPosition, localPosition, Time.deltaTime * Camera.FollowSpeed);

    }
    public bool RaycastToMiddleOfScreen(LayerMask targetLayer, out CharacterSOController targetController)
    {
        RaycastHit hit;
        targetController = null;
        Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        /* barrel.position */
        bool rayHitTarget = Physics.Raycast(ray, out hit, 200, targetLayer);
        if (rayHitTarget)
            if (hit.collider.gameObject.TryGetComponent(out CharacterSOController c))
                targetController = c;
        return rayHitTarget;
    }
}
