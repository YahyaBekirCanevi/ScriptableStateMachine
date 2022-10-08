using UnityEngine;

[CreateAssetMenu(fileName = "Char", menuName = "SO/Character")]
public class Character : ScriptableObject
{
    public string characterName;
    public float hitPoint = 10;
    public float walkSpeed = 4;
    public float runSpeed = 8;
    public float jumpStrength = 6;
}