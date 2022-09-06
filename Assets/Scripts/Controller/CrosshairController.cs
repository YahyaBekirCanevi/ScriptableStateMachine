using UnityEngine;

public class CrosshairController : MonoBehaviour
{
    [SerializeField] private PlayerAttackStateService playerAttackStateService;
    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        Charge();
    }
    private void Charge()
    {
        anim.SetBool("charge", playerAttackStateService.Charging);
    }
}
