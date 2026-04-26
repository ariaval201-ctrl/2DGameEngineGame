using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public static Vector2 SharedCheckpoint { get; private set; }

    private void Awake()
    {
        // Initialize shared checkpoint to starting positions of first player
        if (SharedCheckpoint == Vector2.zero)
        {
            SharedCheckpoint = transform.position;
        }
    }

    public static void UpdateSharedCheckpoint(Vector2 newCheckpoint)
    {
        SharedCheckpoint = newCheckpoint;
        Debug.Log("Shared checkpoint updated to: " + SharedCheckpoint);
    }

    public void Die()
    {
        if (UIManager.Instance != null)
        {
            UIManager.Instance.FadeToWhite(() =>
            {
                RespawnAllPlayers();
                UIManager.Instance.FadeFromWhite();
            });
        }
        else
        {
            RespawnAllPlayers();
        }
    }

    private void RespawnAllPlayers()
{

    RespawnManager[] allPlayers = FindObjectsOfType<RespawnManager>();
    foreach (RespawnManager player in allPlayers)
    {
        player.transform.position = SharedCheckpoint;
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb != null) rb.linearVelocity = Vector2.zero;
    }

    foreach (GameObject door in ButtonInteractChop.AllDoors)
    {
        if (door != null)
            door.SetActive(true);
    }

    foreach (GameObject door in ButtonInteractCroc.AllDoors)
    {
        if (door != null)
            door.SetActive(true);
    }
}
}