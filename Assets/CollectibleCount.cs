using UnityEngine;

public class CollectibleCount : MonoBehaviour
{
    public static CollectibleCount instance;

    [Header("Collectibles")]
    public int totalCollected = 0;

    private void Awake()
    {
        // Simple singleton setup
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddCollectible(int amount)
    {
        totalCollected += amount;
        Debug.Log("Collected: " + totalCollected);
    }

    public void ResetCount()
    {
        totalCollected = 0;
    }
}