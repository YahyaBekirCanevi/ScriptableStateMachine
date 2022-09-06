using System;
using UnityEngine;

[RequireComponent(typeof(PlayerAttackStateService))]
public class PlayerAimController : MonoBehaviour
{
    private PlayerAttackStateService playerAttackStateService;
    [SerializeField] private String targetTag;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private Transform barrel;

    [Tooltip("If Raycast hits the target this turns to true")]
    [SerializeField, ReadOnly] private bool rayHitTarget;
    public CharacterSOController HitTarget { get => RaycastToMiddleOfScreen(); }
    private void Awake()
    {
        playerAttackStateService = GetComponent<PlayerAttackStateService>();
    }
    private CharacterSOController RaycastToMiddleOfScreen()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        /* barrel.position */
        rayHitTarget = Physics.Raycast(ray, out hit, 200, targetLayer);
        CharacterSOController targetController = null;
        rayHitTarget = rayHitTarget && hit.collider.gameObject.TryGetComponent(out targetController);
        return targetController;
    }
}
