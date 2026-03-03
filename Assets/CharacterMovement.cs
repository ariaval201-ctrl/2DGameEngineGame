using UnityEngine;
using Unity.Cinemachine;
using System.Collections;

public class CharacterMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 8f;
    public float jumpForce = 12f;
    public bool canMove = true;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckDistance = 0.3f;
    public Vector2 groundCheckOffset = new Vector2(0f, -0.5f);
    public LayerMask groundLayer;

    [Header("Camera")]
    public CameraManager cameraManager;
    public CinemachineCamera myCamera; // assign chopCamera or crocCamera


 
   

    private Rigidbody2D rb;
    private float horizInput;
    private bool isGrounded;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Only read input if this character's camera is active
        if (cameraManager.currentCam == myCamera)
        {
            horizInput = Input.GetAxisRaw("Horizontal");

            
            if (Input.GetButtonDown("Jump") && IsGrounded())
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            }
        }
        else
        {
            horizInput = 0;
        }

        // Flip sprite
        if (spriteRenderer != null)
        {
            if (horizInput > 0.1f) spriteRenderer.flipX = false;
            else if (horizInput < -0.1f) spriteRenderer.flipX = true;
        }

        // Update animator
        if (animator != null)
        {
            animator.SetFloat("moveInput", Mathf.Abs(horizInput));
            animator.SetBool("isGrounded", IsGrounded());
        }
        
       
    }

   void FixedUpdate()
{
    // Stop horizontal movement if canMove is false
    if (!canMove)
    {
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        return;
    }

    // Normal movement
    rb.linearVelocity = new Vector2(horizInput * speed, rb.linearVelocity.y);
    
}

    bool IsGrounded()
    {
        Vector2 rayOrigin = groundCheck != null ? (Vector2)groundCheck.position : (Vector2)transform.position + groundCheckOffset;
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, groundCheckDistance, groundLayer);
        return hit.collider != null;
    }


}