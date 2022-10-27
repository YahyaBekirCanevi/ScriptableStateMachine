using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Chase ", menuName = "SO/EnemyBehaviour/Chase")]
public class ChaseBehaviour : BaseBehaviour
{
    Vector3 _direction;
    public override bool Statement(EnemyController controller)
    {
        _direction = controller.TargetPosition - controller.transform.position;
        float magnitude = Math.Abs(_direction.magnitude);
        return controller.IsAlerted && controller.TargetInSight && magnitude > controller.Weapon.AttackRange;
    }
    public override void OnUpdate()
    {
        if (Statement(controller))
        {
            Chase();
        }
        else base.OnUpdate();
    }
    protected void Chase()
    {
        controller.transform.Translate(_direction.normalized * controller.Character.walkSpeed * Time.deltaTime);
        controller.transform.forward = -_direction.normalized;
    }
}