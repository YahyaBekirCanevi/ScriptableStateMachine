using UnityEngine;

public class CharacterSOController : MonoBehaviour
{
    float currentHitPoint;
    [SerializeField] Character character;
    public Character Character { get => character; set => character = value; }
    private void Awake()
    {
        currentHitPoint = character.hitPoint;
    }
    public void Heal(float amount)
    {
        currentHitPoint += amount;
        currentHitPoint = currentHitPoint > character.hitPoint ? character.hitPoint : currentHitPoint;
    }
    public void TakeDamage(float amount)
    {
        currentHitPoint -= amount;
        if (currentHitPoint <= 0)
        {
            Destroy(gameObject);
        }
    }
}