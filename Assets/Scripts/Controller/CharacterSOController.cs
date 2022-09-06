using UnityEngine;

public class CharacterSOController : MonoBehaviour
{
    private float maxHitPoint;
    private float currentHitPoint;
    [SerializeField] private Character character;
    private void Awake()
    {
        currentHitPoint = maxHitPoint = character.hitPoint;
    }
    public void Heal(float amount)
    {
        currentHitPoint += amount;
        currentHitPoint = currentHitPoint > maxHitPoint ? maxHitPoint : currentHitPoint;
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