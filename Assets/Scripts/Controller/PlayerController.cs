using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    float _vertical;
    float _horizontal;
    float _gravity;
    Vector3 _currentMovement;
    float time = 0;
    float _animatorMoveSpeed = 0;

    CharacterController _controller;
    public CameraController CameraController;
    public Character Character;
    public Animator Animator;

    [Header("Values")]
    [SerializeField] protected AnimationCurve jumpCurve;

    /*     [Header("Advanced")]
        [SerializeField] Transform _lowestPoint;
        [SerializeField] Transform _lastActivePosition; */
    // getters and setters
    private static PlayerInputActions playerInput;
    public static PlayerInputActions PlayerInput
    {
        get => playerInput == null ? (playerInput = new PlayerInputActions()) : playerInput;
    }
    public float MoveSpeed { get; protected set; }
    public bool IsGrounded
    {
        get => Physics.SphereCast(
                transform.position + _controller.center,
                _controller.radius,
                Vector3.down,
                out RaycastHit hit,
                (_controller.height * .5f) + _controller.skinWidth
            );
    }
    public bool IsRunning { get; protected set; }
    public bool IsJumping { get; protected set; }
    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        PlayerInput.CharacterControls.Move.started += PlayerInputActions_OnMove;
        PlayerInput.CharacterControls.Move.performed += PlayerInputActions_OnMove;
        PlayerInput.CharacterControls.Move.canceled += PlayerInputActions_OnMove;

        PlayerInput.CharacterControls.Run.started += PlayerInputActions_OnRun;
        PlayerInput.CharacterControls.Run.canceled += PlayerInputActions_OnRun;

        PlayerInput.CharacterControls.Jump.started += PlayerInputActions_OnJump;
    }
    private void PlayerInputActions_OnMove(InputAction.CallbackContext context)
    {
        Vector2 currentMovementInput = context.ReadValue<Vector2>();
        _vertical = currentMovementInput.y;
        _horizontal = currentMovementInput.x;

        MoveSpeed = Mathf.Abs(_vertical) + Mathf.Abs(_horizontal);
        MoveSpeed = Mathf.Clamp(MoveSpeed, 0, 1);
    }
    private void PlayerInputActions_OnRun(InputAction.CallbackContext context)
    {
        IsRunning = !CameraController.Aim && context.ReadValueAsButton();
    }
    private void PlayerInputActions_OnJump(InputAction.CallbackContext context)
    {
        if (IsGrounded) StartCoroutine(Jumping());
    }
    private IEnumerator Jumping()
    {
        float duration = jumpCurve[jumpCurve.length - 1].time;
        do
        {
            time += Time.deltaTime;
            _gravity = jumpCurve.Evaluate(time) * Character.jumpStrength;
            yield return new WaitForEndOfFrame();
            if (IsGrounded && time > duration * .4f) break;
        } while (time < duration);
        time = 0;
    }
    private void Update()
    {
        HandleAnimator();
        HandlePlayerMovement();
        HandlePlayerRotation();
        if (time == 0)
            _gravity = IsGrounded ? -1 : Math.Clamp(_gravity - .8f, -16, -10);

        _controller.Move((_currentMovement + (Vector3.up * _gravity)) * Time.deltaTime);
    }
    private void HandleAnimator()
    {
        float newSpeed = MoveSpeed * (IsRunning ? 2 : 1);
        _animatorMoveSpeed = Mathf.Lerp(_animatorMoveSpeed, newSpeed, Time.deltaTime * 4f);
        Animator.SetFloat("speed", _animatorMoveSpeed);
        Animator.SetFloat("vertical", _vertical);
        Animator.SetFloat("horizontal", _horizontal);
        Animator.SetBool("ground", IsGrounded);
    }
    private void HandlePlayerMovement()
    {
        Vector3 move = CameraController.Cam.forward * _vertical +
            CameraController.Cam.right * _horizontal;
        move.y = 0;

        float movementSpeed = IsRunning ? Character.runSpeed : Character.walkSpeed;
        _currentMovement = Vector3.Lerp(_currentMovement, move.normalized * movementSpeed, Time.deltaTime * 8f);
    }
    private void HandlePlayerRotation()
    {
        Vector3 forward = _currentMovement;
        if (CameraController.Aim)
        {
            forward = CameraController.transform.forward;
            forward.y = 0;
            transform.forward = forward;
        }
        if (_currentMovement != Vector3.zero)
            transform.forward = Vector3.Lerp(transform.forward, forward, Time.deltaTime * 20f);
    }
    private void OnEnable()
    {
        PlayerInput.CharacterControls.Enable();
    }
    private void OnDisable()
    {
        PlayerInput.CharacterControls.Disable();
    }
}
