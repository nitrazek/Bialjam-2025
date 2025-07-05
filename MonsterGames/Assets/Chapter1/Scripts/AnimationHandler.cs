using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationHandler : MonoBehaviour
{
    public Animator animator;
    void Start()
    {
        
    }

    void Update()
    {
        if (animator.GetBool("Happy") == false)
            animator.SetBool("Happy", true);
    }
}
