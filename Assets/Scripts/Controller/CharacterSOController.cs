using UnityEngine;

public class CharacterSOController : MonoBehaviour
{
    private float currentHitPoint;
    [SerializeField] private Character character;
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