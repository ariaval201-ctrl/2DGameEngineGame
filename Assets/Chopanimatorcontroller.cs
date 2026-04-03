using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterMovement))]
public class ChopAnimatorController : MonoBehaviour
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

        
        animator.SetFloat("moveInput", Mathf.Abs(movement.HorizontalInput));

        
        
    }
}