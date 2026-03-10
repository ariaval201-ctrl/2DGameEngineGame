using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player2"))
        {
            RespawnManager.UpdateSharedCheckpoint(transform.position);
            Debug.Log("Checkpoint reached by Player2 at: " + transform.position);
        }
    }
}