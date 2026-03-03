using UnityEngine;

public class HookMovement : MonoBehaviour
{
    private FishingRod rod;
    private float speed;
    private bool isHooked = false;

    public void Initialize(FishingRod fishingRod, float hookSpeed)
    {
        rod = fishingRod;
        speed = hookSpeed;
    }

    void Update()
    {
        if (!isHooked)
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isHooked && collision.CompareTag("Player") && collision.transform != rod.transform)
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            CharacterMovement movement = collision.GetComponent<CharacterMovement>();

            if (rb != null && movement != null)
            {
                isHooked = true;
                rod.OnPlayerHooked(rb, movement);
            }
        }
    }
}