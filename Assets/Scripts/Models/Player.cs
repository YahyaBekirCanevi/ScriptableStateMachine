using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 20;
    [SerializeField] private float runSpeed = 40;
    [SerializeField] private float jumpStrength = 60;

    public float WalkSpeed { get => walkSpeed; }
    public float RunSpeed { get => runSpeed; }
    public float JumpStrength { get => jumpStrength; }
}