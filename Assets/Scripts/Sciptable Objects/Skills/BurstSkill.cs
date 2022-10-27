using UnityEngine;

[CreateAssetMenu(fileName = "BurstSkill", menuName = "SO/Skill/Burst")]
public class BurstSkill : BaseSkill
{
    [SerializeField] private float _energyRequired;
    private float _activeEnergy;
    public float EnergyRequired { get => _energyRequired; }
    public float ActiveEnergy { get => _activeEnergy; }
    public override bool CanUse(EnemyController controller) => _activeEnergy == _energyRequired;
    public override void Use(EnemyController controller)
    {
        _activeEnergy = 0;
    }
}