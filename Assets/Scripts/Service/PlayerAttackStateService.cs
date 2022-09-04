using UnityEngine;

public class PlayerAttackStateService : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField, ReadOnly] private bool isAttacking = false;
    [SerializeField, ReadOnly] private bool charging = false;
    [SerializeField, ReadOnly] private bool disarmed = false;
    [SerializeField, ReadOnly] private bool disappeared = false;
    public bool IsAttacking { get => isAttacking; }
    public bool Charging { get => charging; }
    public bool Disarmed { get => disarmed; set => disarmed = value; }
    public bool Disappeared { get => disappeared; set => disappeared = value; }

    private void Update()
    {
        isAttacking = Input.GetKeyDown(KeyCode.Mouse0);
        charging = Input.GetKey(KeyCode.Mouse0);
    }
}