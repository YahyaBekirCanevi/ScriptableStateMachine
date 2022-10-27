using UnityEngine;

[CreateAssetMenu(fileName = "NormalAttack", menuName = "SO/Skill/NormalAttack")]
public class NormalAttackSkill : BaseSkill
{
    float _time = -1;
    public override bool CanUse(EnemyController controller)
    {
        if (_time == -1) _time = Time.time - controller.Weapon.AttackSpeed;
        return Time.time - _time >= controller.Weapon.AttackSpeed;
    }
    public override void Use(EnemyController controller)
    {
        _time = Time.time;
    }
}