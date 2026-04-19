using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollectibleSystem : MonoBehaviour
{
    [Header("Collectible Settings")]
    public int value = 1;

    [Header("Effects")]
    public AudioClip pickupSound;
    public ParticleSystem pickupParticles;

    [Header("Floating Animation")]
    public float floatAmplitude = 0.25f;  
    public float floatSpeed = 2f;        

    private Vector3 startPos;
    private AudioSource audioSource;

    private void Start()
    {
        startPos = transform.position;
        audioSource = FindObjectOfType<AudioSource>();
    }

    private void Update()
    {
        // Bob up and down using sine wave
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CollectibleUI.instance.AddCollectible(value);

            if (pickupSound != null)
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);

            if (pickupParticles != null)
                Instantiate(pickupParticles, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}