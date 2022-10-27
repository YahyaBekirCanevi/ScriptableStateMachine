using UnityEngine;

[CreateAssetMenu(fileName = "Attack ", menuName = "SO/EnemyBehaviour/Attack")]
public class AttackBehaviour : BaseBehaviour
{
    Skill current;
    Skill NormalAttack;
    Skill Quick;
    Skill Burst;
    public override bool Statement(EnemyController controller)
    {
        return controller.IsAlerted && controller.TargetInSight && controller.TargetDistance <= controller.Weapon.AttackRange;
    }
    public override void OnStart(EnemyController controller)
    {
        base.OnStart(controller);

        NormalAttack = controller.Skills.Find(e => e.BaseSkill is NormalAttackSkill);
        Quick = controller.Skills.Find(e => e.BaseSkill is QuickSkill);
        Burst = controller.Skills.Find(e => e.BaseSkill is BurstSkill);
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        current = null;
        if (Burst != null && Burst.BaseSkill.CanUse(controller)) current = Burst;
        else if (Quick != null && Quick.BaseSkill.CanUse(controller)) current = Quick;
        else if (NormalAttack != null && NormalAttack.BaseSkill.CanUse(controller)) current = NormalAttack;

        if (current != null)
        {
            current.BaseSkill.Use(controller);
            if (current.BaseSkill.BaseAttack != null)
                current.BaseSkill.BaseAttack.Fire(current.PivotPoint, controller.Target);
        }
    }
}