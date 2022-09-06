using UnityEngine;

[CreateAssetMenu(fileName = "Char", menuName = "Character/New")]
public class Character : ScriptableObject
{
    public string characterName;
    public float hitPoint;
}