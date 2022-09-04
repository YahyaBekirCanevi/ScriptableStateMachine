using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerMotionStateService))]
[RequireComponent(typeof(JumpService))]
public class PlayerController : MonoBehaviour
{
    private Player player;
    private PlayerMotionStateService playerMotionStateService;
    [SerializeField] private PlayerAttackStateService playerAttackStateService;
    private JumpService jumpService;
    [SerializeField, Tooltip("Main Camera's Transform")] private Transform cam;
    private CharacterController controller;
    [SerializeField, ReadOnly] private Vector3 movementDirection;
    [SerializeField, ReadOnly] private bool jumping = false;
    [SerializeField] private WeaponDraw weaponDraw;
    [SerializeField] private Transform lowestPoint, lastActivePosition;
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        player = GetComponent<Player>();
        playerMotionStateService = GetComponent<PlayerMotionStateService>();
        jumpService = GetComponent<JumpService>();
    }
    private void Update()
    {
        ReplaceFallenPlayer();
    }

    private void ReplaceFallenPlayer()
    {
        if (playerMotionStateService.IsGrounded)
        {
            lastActivePosition.position = transform.position + Vector3.up * 2 - movementDirection * .6f;
            lastActivePosition.rotation = transform.rotation;
        }
        if (transform.position.y < lowestPoint.position.y)
        {
            transform.position = lastActivePosition.position;
            transform.rotation = lastActivePosition.rotation;
        }
        else
        {
            FaceForward();
            CorrespondPlayerMovement();
        }
    }

    private void FaceForward()
    {
        if (movementDirection == Vector3.zero) return;
        transform.forward = movementDirection;
    }
    private void CorrespondPlayerMovement()
    {
        movementDirection =
            cam.forward * playerMotionStateService.Vertical
            + cam.right * playerMotionStateService.Horizontal;

        movementDirection.y = 0;
        movementDirection = movementDirection.normalized * playerMotionStateService.Speed;
        if (playerMotionStateService.IsGrounded)
        {
            if (playerMotionStateService.IsJumping)
            {
                jumping = true;
                StartCoroutine(jumpService.Jump(
                    player.JumpStrength,
                    afterJump: () => jumping = false
                ));
            }
            /* else jumping = false; */
        }
        float verticalValue = jumping ? jumpService.VerticalForceMagnitude : GameManagerService.Instance.Gravity;
        Vector3 verticalForce = Vector3.up * verticalValue;
        Vector3 move = verticalForce + movementDirection;
        controller.Move(move * Time.deltaTime);
    }
}
