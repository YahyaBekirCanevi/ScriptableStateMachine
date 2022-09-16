using UnityEngine;

[CreateAssetMenu(fileName = "Char", menuName = "Character/New")]
public class Character : ScriptableObject
{
    public string characterName;
    public float hitPoint;
    public float walkSpeed = 4;
    public float runSpeed = 8;
    public float jumpStrength = 6;
}