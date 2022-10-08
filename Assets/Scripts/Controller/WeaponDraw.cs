using System.Collections;
using UnityEngine;

public class WeaponDraw : MonoBehaviour
{
    PlayerController _playerController;
    [SerializeField] private GameObject handPositionedWeapon;
    [SerializeField] private GameObject backPositionedWeapon;
    [SerializeField, Tooltip("The Duration for weapon to disarm")] private float disarmDelay = 3;
    [SerializeField, Tooltip("The Duration for weapon to disappear")] private float disappearDelay = 6;
    [SerializeField, ReadOnly] private bool onHand = false;
    public bool OnHand { get => onHand; }
    public bool Attacking { get; set; }
    public bool Charging { get => _playerController.Animator.GetBool("charge"); }
    private void Awake()
    {
        _playerController = GetComponentInParent<PlayerController>();
        handPositionedWeapon.SetActive(false);
        backPositionedWeapon.SetActive(false);
    }
    private void Update()
    {
        if (Attacking)
        {
            StopCoroutine("DisappearAfterDelay");
            StopCoroutine("DisarmAfterDelay");
            DrawWeapon();
        }
        if (onHand && !Charging)
        {
            if (_playerController.MoveSpeed > 0 || !_playerController.IsGrounded) DisarmWeapon();
            else StartCoroutine("DisarmAfterDelay");
        }
    }

    private void DrawWeapon()
    {
        handPositionedWeapon.SetActive(true);
        backPositionedWeapon.SetActive(false);
        onHand = true;
    }
    private IEnumerator DisarmAfterDelay()
    {
        yield return new WaitForSeconds(disarmDelay);
        DisarmWeapon();
    }
    private void DisarmWeapon()
    {
        handPositionedWeapon.SetActive(false);
        backPositionedWeapon.SetActive(true);
        onHand = false;
        StartCoroutine("DisappearAfterDelay");
    }
    private IEnumerator DisappearAfterDelay()
    {
        yield return new WaitForSeconds(disappearDelay);
        DisappearWeapon();
    }
    private void DisappearWeapon()
    {
        handPositionedWeapon.SetActive(false);
        backPositionedWeapon.SetActive(false);
    }
}
