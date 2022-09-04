using System.Collections;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private float speed = 0;
    [SerializeField] private PlayerMotionStateService playerMotionStateService;
    [SerializeField] private PlayerAttackStateService playerAttackStateService;
    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (playerMotionStateService.State == CharacterState.idle && anim.GetFloat("random") == 0)
            StartCoroutine("RandomEmote");
        else
            StopCoroutine("RandomEmote");

    }

    private IEnumerator RandomEmote()
    {
        yield return new WaitForSeconds(Random.Range(10, 30));
        float random = Random.Range(1, 4);
        random = Mathf.Floor(random) % 2;
        random++;
        anim.SetFloat("random", random);
    }

    private void LateUpdate()
    {
        anim.SetBool("ground", playerMotionStateService.IsGrounded);

        bool longFall = playerMotionStateService.FallHeight >= playerMotionStateService.LandHeight
            && playerMotionStateService.HeightFromGround <= playerMotionStateService.LandHeight;
        anim.SetBool("longFall", longFall);

        float newSpeed = playerMotionStateService.State == CharacterState.walk ?
            1 : playerMotionStateService.State == CharacterState.run ?
                2 : 0;
        speed = Mathf.Lerp(speed, newSpeed, Time.deltaTime * 12f);

        anim.SetFloat("speed", speed);
    }
}
