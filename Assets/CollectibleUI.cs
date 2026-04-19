using UnityEngine;
using TMPro; 

public class CollectibleUI : MonoBehaviour
{
    public static CollectibleUI instance;

    [Header("UI")]
    public TextMeshProUGUI counterText;

    private int collectibleCount = 0;

    private void Awake()
    {
        instance = this;
        UpdateUI();
    }

    public void AddCollectible(int amount)
    {
        collectibleCount += amount;
        UpdateUI();
    }

    void UpdateUI()
    {
        counterText.text = "Croc Food: " + collectibleCount;
    }
}