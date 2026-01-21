using System;
using UnityEngine;

public class ActorController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource audioS;
    [SerializeField] private AudioClip clip;

    public void Awake()
    {
        audioS.clip = clip;
    }

    public void Stop()
    {
        audioS.Stop();
        animator.SetBool("IsRun", false);
    }

    public void Run()
    {
        audioS.Play();
        animator.SetBool("IsRun", true);
    }

    /// <param name="direction">
    /// 1  Ч смотреть вправо  
    /// -1 Ч смотреть влево
    /// </param>
    public void Turn(int direction)
    {
        if (direction == 0) return;

        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * direction;
        transform.localScale = scale;
    }
}
