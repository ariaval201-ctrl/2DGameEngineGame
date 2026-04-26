using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public RespawnManager respawnManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Player2"))
        {
            respawnManager.Die();
        }
    }

   
}