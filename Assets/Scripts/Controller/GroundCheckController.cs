using UnityEngine;

public class GroundCheckController : MonoBehaviour
{
    [SerializeField] private float maxDistance = 100;
    [SerializeField] private float radius = .5f;
    [SerializeField] private LayerMask layer;
    [SerializeField] private string objectTag;
    [SerializeField] private RaycastHit hit;
    public RaycastHit Hit { get => hit; }
    [SerializeField, ReadOnly] private bool isGrounded;
    public bool IsGrounded { get => isGrounded; }
    private void Update()
    {
        CalculateHitDistance();
    }
    private void CalculateHitDistance()
    {
        bool hitToGround = Physics.SphereCast(transform.position + Vector3.up, radius, Vector3.down, out hit, maxDistance, layer);
        //bool checkSphere = Physics.CheckSphere(transform.position, radius, layer);
        hit.distance = hitToGround ? hit.distance : maxDistance;
        isGrounded = hitToGround && hit.distance < 1.6f;//checkSphere;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
