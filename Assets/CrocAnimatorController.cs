using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterMovement))]
public class CrocAnimatorController : MonoBehaviour
{
    private Animator animator;
    public CharacterMovement movement;

    void Awake()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<CharacterMovement>();
    }

    void Update()
    {
        if (animator == null || movement == null) return;

        // Set walking/running speed
        animator.SetFloat("moveInput", Mathf.Abs(movement.HorizontalInput));

        // Set grounded state
        animator.SetBool("isGrounded", movement.IsGrounded());
    }
}