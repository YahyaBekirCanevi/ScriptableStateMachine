using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerAttackStateService))]
public class WeaponDraw : MonoBehaviour
{
    private bool idleWaiting = false;
    private bool drawWeaponStatement;
    private PlayerAttackStateService playerAttackStateService;
    [SerializeField] private PlayerMotionStateService playerMotionStateService;
    [SerializeField] private GameObject handPositionedWeapon;
    [SerializeField] private GameObject backPositionedWeapon;
    [SerializeField, Tooltip("The Duration for weapon to disarm")] private float disarmDelay = 3;
    [SerializeField, Tooltip("The Duration for weapon to disappear")] private float disappearDelay = 6;
    [SerializeField, ReadOnly] private bool onHand = false;
    public bool OnHand { get => onHand; }

    private void Awake()
    {
        playerAttackStateService = GetComponent<PlayerAttackStateService>();
        handPositionedWeapon.SetActive(false);
        backPositionedWeapon.SetActive(false);
    }
    private void Update()
    {
        if (playerAttackStateService.IsAttacking)
        {
            StopCoroutine("DisappearAfterDelay");
            StopCoroutine("DisarmAfterDelay");
            DrawWeapon();
        }
        if (!playerAttackStateService.Disarmed && !playerAttackStateService.Charging &&
            (playerMotionStateService.MoveSpeed > 0 || playerMotionStateService.IsGrounded))
        {
            DisarmWeapon();
        }
        if (onHand && !playerAttackStateService.Charging && !idleWaiting)
        {
            StartCoroutine("DisarmAfterDelay");
        }
    }

    private void DrawWeapon()
    {
        handPositionedWeapon.SetActive(true);
        backPositionedWeapon.SetActive(false);
        onHand = true;
        idleWaiting = false;
        playerAttackStateService.Disarmed = false;
        playerAttackStateService.Disappeared = false;
    }

    private void DisarmWeapon()
    {
        handPositionedWeapon.SetActive(false);
        backPositionedWeapon.SetActive(true);
        onHand = false;
        idleWaiting = true;
        playerAttackStateService.Disarmed = true;
        playerAttackStateService.Disappeared = false;
        StartCoroutine("DisappearAfterDelay");
    }
    private void DisappearWeapon()
    {
        handPositionedWeapon.SetActive(false);
        backPositionedWeapon.SetActive(false);
        playerAttackStateService.Disarmed = true;
        playerAttackStateService.Disappeared = true;
    }
    private IEnumerator DisarmAfterDelay()
    {
        yield return new WaitForSeconds(disarmDelay);
        DisarmWeapon();
    }
    private IEnumerator DisappearAfterDelay()
    {
        yield return new WaitForSeconds(disappearDelay);
        DisappearWeapon();
    }
}
