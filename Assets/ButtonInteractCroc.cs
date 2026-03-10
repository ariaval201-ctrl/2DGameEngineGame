using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ButtonInteractCroc : MonoBehaviour
{
    public GameObject door;
    public TMP_Text interactText;
    public TMP_Text wrongCharactercroc;

    // Static list to track all doors
    public static List<GameObject> AllDoors = new List<GameObject>();

    private void Awake()
    {
        // Add this door to the static list if not already added
        if (door != null && !AllDoors.Contains(door))
            AllDoors.Add(door);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player2") && door != null && door.activeSelf)
        {
            interactText.gameObject.SetActive(true);
        }
        else if (other.CompareTag("Player") && door != null && door.activeSelf)
        {
            wrongCharactercroc.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player2"))
        {
            interactText.gameObject.SetActive(false);
        }
        else if (other.CompareTag("Player"))
        {
            wrongCharactercroc.gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player2") && Input.GetKeyDown(KeyCode.F))
        {
            DestroyDoor();
            interactText.gameObject.SetActive(false);
        }
    }

    void DestroyDoor()
    {
        if (door != null)
        {
            door.SetActive(false);
        }
    }
}