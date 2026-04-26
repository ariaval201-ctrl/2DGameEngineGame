using UnityEngine;
using System.Collections;

public class LedgeClimb2D : MonoBehaviour
{
    [Header("Detection")]
    public Transform wallCheck;
    public Transform ledgeCheck;
    public float wallCheckDistance = 0.2f;
    public float ledgeCheckDistance = 0.2f;
    public LayerMask groundLayer;

    [Header("Climb Settings")]
    public Vector2 climbOffset = new Vector2(0.6f, 1.2f);
    public float climbDuration = 0.25f;


    public SpriteRenderer spriteRenderer;

    private Rigidbody2D rb;

    private bool isTouchingWall;
    private bool canClimb;
    private bool isClimbing;

    private int facingDirection = 1;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
       facingDirection = spriteRenderer.flipX ? -1 : 1;

        CheckSurroundings();

        float horiz = Input.GetAxisRaw("Horizontal");


    if (isTouchingWall && canClimb && !isClimbing && horiz == facingDirection)
    {
    ClimbLedge();
    }
    }

    void CheckSurroundings()
    {
        // code for gizmo to check if there are walls 
        Vector2 wallDir = Vector2.right * facingDirection;

        isTouchingWall = Physics2D.Raycast(
            wallCheck.position,
            wallDir,
            wallCheckDistance,
            groundLayer
        );
            //to check if there is a wall above the wall so you can jump up
        bool isBlockedAbove = Physics2D.Raycast(
            ledgeCheck.position,
            wallDir,
            ledgeCheckDistance,
            groundLayer
        );

        canClimb = !isBlockedAbove;
    }

    void ClimbLedge()
    {
        StartCoroutine(SmoothClimb());
    }

    IEnumerator SmoothClimb()
    {
        //code to move slowly instead of straight teleport
        isClimbing = true;

        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = 0f;

        Vector3 startPos = transform.position;
        Vector3 targetPos = transform.position +
                            new Vector3(climbOffset.x * facingDirection,
                                        climbOffset.y,
                                        0);

        float time = 0f;

        while (time < climbDuration)
        {
            float t = time / climbDuration;
            t = t * t * (3f - 2f * t);

            transform.position = Vector3.Lerp(startPos, targetPos, t);

            time += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;

        rb.gravityScale = 1f;
        isClimbing = false;
    }

    private void OnDrawGizmos()
    {

        //drawing the gizmos in unity
        if (wallCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(wallCheck.position,
                wallCheck.position + Vector3.right * transform.localScale.x * wallCheckDistance);
        }

        if (ledgeCheck != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(ledgeCheck.position,
                ledgeCheck.position + Vector3.right * transform.localScale.x * ledgeCheckDistance);
        }
    }
}