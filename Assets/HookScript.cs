using UnityEngine;

public class HookScript : MonoBehaviour
{
    [Header("Players")]
    public GameObject player1;   // the one casting the hook
    public GameObject player2;   // the one to be pulled

    [Header("Hook Settings")]
    public float liftSpeed = 5f;        // speed to pull player2 up
    public float liftHeight = 1f;       // how high above player1
    public float player1BackwardOffset = 0.5f; // player1 moves back slightly
    public float pullDistance = 0f;     // horizontal offset beside player1

    [Header("Drop Limit")]
    public float maxDropDistance = 5f;

    private Vector3 startPosition;
    private bool dropStopped = false;

    private bool caught = false;
    private Rigidbody2D rb;

    void Start()
{
    rb = GetComponent<Rigidbody2D>();
    startPosition = transform.position;

    // Ignore collision with Player1
    Collider2D hookCollider = GetComponent<Collider2D>();
    Collider2D player1Collider = player1.GetComponent<Collider2D>();

    if (hookCollider != null && player1Collider != null)
    {
        Physics2D.IgnoreCollision(hookCollider, player1Collider);
    }
}

    void Update()
    {
        if (!dropStopped)
{
    float distanceDropped = Vector3.Distance(startPosition, transform.position);

    if (distanceDropped >= maxDropDistance)
    {
        StopFalling();
    }
}
        if (caught && player2 != null)
        {
            // Target position: beside player1, slightly lifted
            Vector3 targetPos = new Vector3(
                player1.transform.position.x + (player1.transform.localScale.x > 0 ? pullDistance : -pullDistance),
                player1.transform.position.y + liftHeight,
                player2.transform.position.z
            );

            // Smoothly move player2 toward target
            player2.transform.position = Vector3.Lerp(player2.transform.position, targetPos, Time.deltaTime * liftSpeed);

            // Move player1 slightly backward to simulate pulling
            Vector3 player1Target = player1.transform.position - new Vector3(
                player1.transform.localScale.x > 0 ? player1BackwardOffset : -player1BackwardOffset, 
                0, 0
            );
            player1.transform.position = Vector3.Lerp(player1.transform.position, player1Target, Time.deltaTime * (liftSpeed / 2));
        }

    }
    private void StopFalling()
{
    dropStopped = true;

    if (rb != null)
    {
        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = 0f;
    }
}
}