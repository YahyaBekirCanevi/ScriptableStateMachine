using UnityEngine;

[CreateAssetMenu(fileName = "Idle ", menuName = "SO/EnemyBehaviour/Idle")]
public class IdleBehaviour : BaseBehaviour
{
    public override bool Statement(EnemyController controller)
    {
        return controller.MovementSpeed == 0 && !controller.IsAlerted;
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (controller.TargetDistance > 0 &&
                controller.TargetDistance <= controller.AlertDistance &&
                controller.TargetInSight)
            controller.IsAlerted = true;
    }
}