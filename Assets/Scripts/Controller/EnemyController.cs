using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    CharacterSOController health;
    [HideInInspector] public Character Character;
    public BaseAttack Weapon;
    public BaseBehaviour Current;

    [Space]
    public List<Skill> Skills;

    [Space]

    [Tooltip("Player")]
    public Transform Target;

    [Space]

    [Header("Values")]
    [SerializeField] float _alertDistance = 8;
    [SerializeField] float _targetEscapeDistance = 10;
    [SerializeField, ReadOnly] float _targetDistance;
    [SerializeField, ReadOnly] bool _isAlerted = false;
    [SerializeField, ReadOnly] bool _targetInSight = false;
    public float MovementSpeed { get; private set; }
    public float AlertDistance { get => _alertDistance; }
    public float TargetEscapeDistance { get => _targetEscapeDistance; }
    public float TargetDistance { get => _targetDistance; }
    public bool IsAlerted { get => _isAlerted; set => _isAlerted = value; }
    public bool TargetInSight { get => _targetInSight; set => _targetInSight = value; }
    public Vector3 TargetPosition
    {
        get => Target.position;
    }
    private void Start()
    {
        Character = (health = GetComponent<CharacterSOController>()).Character;
        Current.OnStart(this);
    }
    private void Update()
    {
        Current.OnUpdate();
        _targetDistance = Vector3.Distance(TargetPosition, transform.position);
    }
}
