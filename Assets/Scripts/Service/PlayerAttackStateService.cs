using UnityEngine;

[RequireComponent(typeof(PlayerAimController))]
public class PlayerAttackStateService : MonoBehaviour
{
    private PlayerAimController playerAimController;
    private PlayerMotionStateService playerMotionStateService;
    [SerializeField] private Player player;
    [SerializeField, ReadOnly] private bool isAttacking = false;
    [SerializeField, ReadOnly] private bool charging = false;
    [SerializeField, ReadOnly] private bool disarmed = false;
    [SerializeField, ReadOnly] private bool disappeared = false;
    public bool IsAttacking { get => isAttacking; }
    public bool Charging { get => charging; set => charging = value; }
    public bool Disarmed { get => disarmed; set => disarmed = value; }
    public bool Disappeared { get => disappeared; set => disappeared = value; }
    public bool ButtonUp { get; set; }

    private CharacterSOController hitTargetController;
    private void Awake()
    {
        playerAimController = GetComponent<PlayerAimController>();
        playerMotionStateService = player.GetComponent<PlayerMotionStateService>();
    }
    private void Update()
    {
        isAttacking = Input.GetKeyDown(KeyCode.Mouse0);
        ButtonUp = Input.GetKeyUp(KeyCode.Mouse0);
        if (!playerMotionStateService.IsGrounded)
        {
            charging = false;
        }
        else
        {
            if (isAttacking) charging = true;
            if (charging) hitTargetController = playerAimController.HitTarget;
        }
    }
    public void Attack()
    {
        if (hitTargetController == null) return;
        hitTargetController.TakeDamage(amount: 20);
    }
}