using UnityEngine;

public class CrosshairController : MonoBehaviour
{
    Animator anim;
    [SerializeField] CameraController CameraController;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        anim.SetBool("charge", CameraController.Aim);
    }
}
