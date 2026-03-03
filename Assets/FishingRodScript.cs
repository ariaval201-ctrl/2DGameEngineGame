using UnityEngine;

public class FishingRod : MonoBehaviour
{
    [Header("Hook Settings")]
    public GameObject hookPrefab;
    public float hookSpeed = 10f;
    public float pullSpeed = 8f;
    public float snapDistance = 0.2f;

    private GameObject currentHook;
    private Rigidbody2D hookedPlayerRb;
    private CharacterMovement hookedPlayerMovement; // your player movement script

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && currentHook == null)
        {
            SpawnHook();
        }
    }

    void FixedUpdate()
    {
        if (currentHook != null && hookedPlayerRb != null)
        {
            PullPlayerUp();
        }
    }

    void SpawnHook()
    {
        Vector3 spawnPos = transform.position + Vector3.down * 0.5f;
        currentHook = Instantiate(hookPrefab, spawnPos, Quaternion.identity);
        HookMovement hookScript = currentHook.GetComponent<HookMovement>();
        hookScript.Initialize(this, hookSpeed);
    }

    // Called by the hook when it collides with the other player
    public void OnPlayerHooked(Rigidbody2D playerRb, CharacterMovement movement)
    {
        hookedPlayerRb = playerRb;
        hookedPlayerMovement = movement;

        // Temporarily disable player input and physics for smooth pull
        hookedPlayerMovement.enabled = false;
        hookedPlayerRb.isKinematic = true;
    }

    void PullPlayerUp()
    {
        Vector2 direction = ((Vector2)transform.position - hookedPlayerRb.position).normalized;
        Vector2 newPos = hookedPlayerRb.position + direction * pullSpeed * Time.fixedDeltaTime;
        hookedPlayerRb.MovePosition(newPos);

        if (Vector2.Distance(hookedPlayerRb.position, transform.position) < snapDistance)
        {
            // Snap to player and restore control
            hookedPlayerRb.position = transform.position;
            hookedPlayerRb.isKinematic = false;
            hookedPlayerMovement.enabled = true;

            Destroy(currentHook);
            hookedPlayerRb = null;
            hookedPlayerMovement = null;
        }
    }
}