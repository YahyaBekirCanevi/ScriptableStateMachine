using UnityEngine;

public abstract class BaseAttack : ScriptableObject
{
    public float Damage = 10;
    public float AttackRange = 1;
    [Tooltip("Per second")] public float AttackSpeed = 1;
    public virtual void Fire(Transform pivot, Transform target) { }
}