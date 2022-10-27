using UnityEngine;

public abstract class BaseSkill : ScriptableObject
{
    [SerializeField] private float _damage;
    public BaseAttack BaseAttack;
    public abstract bool CanUse(EnemyController controller);
    public abstract void Use(EnemyController controller);
}