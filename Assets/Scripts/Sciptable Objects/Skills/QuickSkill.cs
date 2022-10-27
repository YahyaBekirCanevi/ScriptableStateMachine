using UnityEngine;

[CreateAssetMenu(fileName = "QuickSkill", menuName = "SO/Skill/Quick")]
public class QuickSkill : BaseSkill
{
    [SerializeField] private float _cooldown;
    private float _activeCD = 0;
    public float Cooldown { get => _cooldown; }
    public float ActiveCD { get => _activeCD; }
    public override bool CanUse(EnemyController controller) => _activeCD == 0;
    public override void Use(EnemyController controller)
    {
        _activeCD = _cooldown;
    }
}