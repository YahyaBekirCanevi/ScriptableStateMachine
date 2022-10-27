using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAimController : MonoBehaviour
{
    WeaponDraw _weaponDraw;
    CameraController _cameraController;
    PlayerController _playerController;
    [SerializeField] BaseAttack weaponModel;
    [SerializeField] LayerMask targetLayer;
    [SerializeField, ReadOnly] CharacterSOController target;

    [Tooltip("If Raycast hits the target this turns to true")]
    [SerializeField, ReadOnly] bool rayHitTarget;
    float pressTime = 0;
    private void Awake()
    {
        _cameraController = Camera.main.GetComponentInParent<CameraController>();
        _playerController = GetComponentInParent<PlayerController>();
        _weaponDraw = GetComponentInParent<WeaponDraw>();

        PlayerController.PlayerInput.CharacterControls.Fire.started += PlayerInputActions_PressDown;
        PlayerController.PlayerInput.CharacterControls.Fire.canceled += PlayerInputActions_PressUp;
    }
    private void PlayerInputActions_PressDown(InputAction.CallbackContext context)
    {
        _playerController.Animator.SetBool("chargeButtonUp", false);
        _playerController.Animator.SetBool("charge", true);
        pressTime = Time.time;
    }
    private void PlayerInputActions_PressUp(InputAction.CallbackContext context)
    {
        _playerController.Animator.SetBool("chargeButtonUp", true);
        CastRay();
        if (target != null)
        {
            target.TakeDamage(weaponModel.Damage);
        }
        pressTime = 0;
    }
    private void CastRay()
    {
        rayHitTarget = _cameraController.RaycastToMiddleOfScreen(targetLayer, out target);
    }
    private void Update()
    {
        _weaponDraw.Attacking = pressTime != 0;
    }
}
