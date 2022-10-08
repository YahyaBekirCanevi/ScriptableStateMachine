using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "SO/WeaponModel")]
public class WeaponModel : ScriptableObject
{
    [SerializeField] float damage = 20;
    [SerializeField] bool hasCapacity = false;
    [SerializeField] float capacity = -1;
    private void OnValidate()
    {
        capacity = hasCapacity ? capacity : -1;
    }

    public float Damage { get => damage; }
    public float Capacity { get => capacity; }
}