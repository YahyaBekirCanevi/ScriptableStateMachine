using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerMotionStateService : MonoBehaviour
{
    private Player player;
    private float vertical;
    private float horizontal;
    [SerializeField] private float landHeight = 3;
    [SerializeField] private GroundCheckController groundCheck;
    [SerializeField, ReadOnly] private bool isJumping = false;
    [SerializeField, ReadOnly] private bool isRunning = false;
    [SerializeField, ReadOnly] private bool isGrounded = true;
    [SerializeField, ReadOnly] private float fallHeight = 0;
    [SerializeField, ReadOnly] private float heightFromGround = 0;
    [SerializeField, ReadOnly] private CharacterState state;
    [SerializeField, ReadOnly] private Vector3 aerialMaxPoint = Vector3.zero;

    public float Vertical { get => vertical; }
    public float Horizontal { get => horizontal; }
    public float Speed { get; set; }
    public bool IsJumping { get => isJumping; }
    public bool IsRunning { get => isRunning; }
    public bool IsGrounded { get => isGrounded; }
    public float LandHeight { get => landHeight; }
    public float FallHeight { get => fallHeight; }
    public float HeightFromGround { get => heightFromGround; }
    public CharacterState State { get => state; set => state = value; }

    private void Awake()
    {
        player = GetComponent<Player>();
    }
    private void Update()
    {
        GetInputs();
        CheckGrounded();
        State = SetState();
        Speed = GetPlayerCurrentSpeed();
    }
    private void CheckGrounded()
    {
        aerialMaxPoint = isGrounded ? transform.position : aerialMaxPoint.y < transform.position.y ? transform.position : aerialMaxPoint;
        heightFromGround = transform.position.y - groundCheck.Hit.point.y;
        isGrounded = groundCheck.IsGrounded;
        float _height = aerialMaxPoint.y - groundCheck.Hit.point.y;
        fallHeight = isGrounded ? 0 : (fallHeight < _height ? _height : fallHeight);
    }
    private void GetInputs()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        isJumping = Input.GetKeyDown(KeyCode.Space);
        isRunning = Input.GetKey(KeyCode.LeftShift);
    }

    private float GetPlayerCurrentSpeed() => State == CharacterState.run ? player.RunSpeed : player.WalkSpeed;

    private CharacterState SetState()
    {
        float moveSpeed = Mathf.Abs(horizontal) + Mathf.Abs(vertical);

        if (moveSpeed < .1f)
        {
            return CharacterState.idle;
        }
        else if (IsRunning)
        {
            return CharacterState.run;
        }
        return CharacterState.walk;
    }

}