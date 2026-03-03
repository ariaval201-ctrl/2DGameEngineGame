using UnityEngine;

public class HookController : MonoBehaviour
{
    private Transform player1;
    private float speed;
    private bool goingDown = true;
    private Transform caughtPlayer;

    public float maxDistance = 5f;
    private Vector3 startPos;

    public void Initialize(Transform p1, float hookSpeed)
    {
        player1 = p1;
        speed = hookSpeed;
        startPos = transform.position;
    }

    void Update()
    {
        if (goingDown)
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);

            if (Vector2.Distance(startPos, transform.position) >= maxDistance)
            {
                goingDown = false;
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                player1.position,
                speed * Time.deltaTime
            );

            if (caughtPlayer != null)
            {
                caughtPlayer.position = transform.position;
            }

            if (Vector2.Distance(transform.position, player1.position) < 0.1f)
            {
                if (caughtPlayer != null)
                {
                    caughtPlayer.SetParent(null);
                }

                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player2"))
        {
            goingDown = false;
            caughtPlayer = collision.transform;
            caughtPlayer.SetParent(transform);
        }
    }
}