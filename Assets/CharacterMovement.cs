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
    public Vector2 groundCheckSize = new Vector2(1f, 0.2f);

    [Header("Camera")]
    public CameraManager cameraManager;
    public CinemachineCamera myCamera; 

    public bool FacingRight => !spriteRenderer.flipX;
    private Rigidbody2D rb;
    private float horizInput;
    private bool isGrounded;
    public SpriteRenderer spriteRenderer;
    private Animator animator;
    public bool canFlip = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
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

        if (spriteRenderer != null && canFlip)
{
            if (horizInput > 0.1f) spriteRenderer.flipX = false;
            else if (horizInput < -0.1f) spriteRenderer.flipX = true;
}

        if (animator != null)
        {
            animator.SetFloat("moveInput", Mathf.Abs(horizInput));
            animator.SetBool("isGrounded", IsGrounded());
        }
        
       
    }

   void FixedUpdate()
{
    if (!canMove)
    {
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        return;
    }

    rb.linearVelocity = new Vector2(horizInput * speed, rb.linearVelocity.y);
    
}

    bool IsGrounded()
{
    Vector2 checkPos = groundCheck != null 
        ? (Vector2)groundCheck.position + groundCheckOffset 
        : (Vector2)transform.position + groundCheckOffset;

    Collider2D hit = Physics2D.OverlapBox(
        checkPos,
        groundCheckSize,
        0f,
        groundLayer
    );

    return hit != null;
}
void OnDrawGizmos()
{
    Gizmos.color = Color.red;

    Vector2 checkPos = groundCheck != null 
        ? (Vector2)groundCheck.position + groundCheckOffset 
        : (Vector2)transform.position + groundCheckOffset;

    Gizmos.DrawWireCube(checkPos, groundCheckSize);
}


}