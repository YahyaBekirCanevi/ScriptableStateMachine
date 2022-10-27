using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileController : MonoBehaviour
{
    [HideInInspector] public MissileAttack Missile;
    Rigidbody _rb;
    [SerializeField] float _readyTimer = 1;
    float _time = 0;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _time = Time.time;
    }
    private void FixedUpdate()
    {
        if (Time.time - _time >= _readyTimer)
            HomeToTarget();

        _rb.velocity = transform.forward * Missile.Force;
    }
    private void HomeToTarget()
    {
        if (Missile.Target == null) return;
        Vector3 direction = Missile.Target.position - transform.position;
        Vector3 rotationAmount = Vector3.Cross(transform.forward, direction);
        _rb.angularVelocity = rotationAmount * Missile.Rotation;
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.TryGetComponent(out CharacterSOController health))
        {
            health.TakeDamage(Missile.Damage);
            Destroy(other.collider.gameObject);
            Destroy(gameObject);
        }
    }
}