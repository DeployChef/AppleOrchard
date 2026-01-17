using UnityEngine;

public class CarIController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void SwitchToIdle()
    {
        animator.SetBool("IsDrive", false);
    }

    public void SwitchToDrive()
    {
        animator.SetBool("IsDrive", true);
    }
}
