using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Die(collision.gameObject);
        }
    }

    void Die(GameObject player)
    {
        Destroy(player); // destroys the player GameObject
    }
}