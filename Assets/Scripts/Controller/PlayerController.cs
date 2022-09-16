using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Basics")]
    public Character player;
    private CharacterController controller;
    [SerializeField, Tooltip("Main Camera's Transform")] private Transform cameraTransform;
    [SerializeField] private Transform lowestPoint, lastActivePosition;
    [Header("Movement")]
    [SerializeField, ReadOnly] private Vector3 velocity;
    [SerializeField, ReadOnly] private bool jumping = false;
    public Vector3 Velocity { get => velocity; private set => velocity = value; }
    [Header("Services")]
    [SerializeField] private PlayerAttackStateService playerAttackStateService;
    [SerializeField] private PlayerMotionStateService playerMotionStateService;
    [SerializeField] private JumpService jumpService;
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }
    private void Update()
    {
        playerMotionStateService.SetPlayerCurrentSpeed(player);
        if (playerMotionStateService.IsGrounded)
        {
            lastActivePosition.position = transform.position
                + Vector3.up * 2
                - Velocity * .6f;
            lastActivePosition.rotation = transform.rotation;
        }
        else if (transform.position.y < lowestPoint.position.y)
        {
            transform.position = lastActivePosition.position;
            transform.rotation = lastActivePosition.rotation;
        }
        else
        {
            Vector3 movement = CorrespondPlayerMovement();

            controller.Move(movement);
        }
    }

    private Vector3 CorrespondPlayerMovement()
    {
        velocity = cameraTransform.forward * playerMotionStateService.Vertical
            + cameraTransform.right * playerMotionStateService.Horizontal;

        velocity.y = 0;
        velocity = velocity.normalized * playerMotionStateService.Speed;
        if (playerAttackStateService.Charging)
        {
            Vector3 forward = cameraTransform.forward;
            forward.y = 0;
            forward.Normalize();
            transform.forward = forward;
        }
        else
        {
            if (velocity != Vector3.zero)
                transform.forward = velocity;
        }
        if (playerMotionStateService.IsGrounded)
        {
            if (playerMotionStateService.IsJumping)
            {
                jumping = true;
                StartCoroutine(jumpService.Jump(
                    player.jumpStrength,
                    afterJump: () => jumping = false
                ));
            }
            /* else jumping = false; */
        }
        float verticalValue = jumping ? jumpService.VerticalForceMagnitude : GameManagerService.Instance.Gravity;
        Vector3 verticalForce = Vector3.up * verticalValue;
        Vector3 move = verticalForce + velocity;
        return move * Time.deltaTime;
    }
}
