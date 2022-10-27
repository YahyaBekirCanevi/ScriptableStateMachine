using UnityEngine;

[CreateAssetMenu(fileName = "Search ", menuName = "SO/EnemyBehaviour/Search")]
public class SearchBehaviour : BaseBehaviour
{
    [SerializeField] float dealertDuration = 3;
    float _dealertTime = 0;
    public override bool Statement(EnemyController controller)
    {
        Search(controller);
        return controller.IsAlerted && !controller.TargetInSight;
    }
    public override void OnUpdate()
    {
        Search(controller);
        base.OnUpdate();
    }
    protected void Search(EnemyController controller)
    {
        float range = controller.Weapon.AttackRange + controller.TargetEscapeDistance;
        if (controller.TargetDistance > range || !InSight(controller))
        {
            _dealertTime += Time.deltaTime;
            if (_dealertTime > dealertDuration)
                Dealert(controller);
        }
        else _dealertTime = 0;
    }

    protected void Dealert(EnemyController controller)
    {
        _dealertTime = 0;
        controller.IsAlerted = false;
    }

    protected bool InSight(EnemyController controller)
    {
        return controller.TargetInSight =
            SeeTarget(out RaycastHit hit) ?
                hit.distance < (controller.Weapon.AttackRange + controller.TargetEscapeDistance) &&
                hit.collider.transform == controller.Target &&
                hit.collider != null
            : false;
    }

    private bool SeeTarget(out RaycastHit hit)
    {
        return Physics.Linecast(controller.transform.position + Vector3.up,
                controller.TargetPosition + Vector3.up, out hit);
    }
}