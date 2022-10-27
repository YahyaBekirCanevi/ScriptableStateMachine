using UnityEngine;

[CreateAssetMenu(fileName = "MissileAttack", menuName = "SO/Attack/MissileAttack")]
public class MissileAttack : BaseAttack
{
    public float Force;
    public float Rotation;
    public GameObject MisslePrefab;
    [HideInInspector] public Transform Target;
    public override void Fire(Transform pivot, Transform target)
    {
        if (MisslePrefab.GetComponent<ProjectileController>() == null) return;
        Instantiate(MisslePrefab, pivot.position, pivot.rotation)
            .GetComponent<ProjectileController>().Missile = this;
    }
}