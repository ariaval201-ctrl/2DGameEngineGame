using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPlatform : MonoBehaviour
{
    private bool player1On = false;
    private bool player2On = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            player1On = true;
        }

        if(other.CompareTag("Player2"))
        {
            player2On = true;
        }

        CheckWin();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            player1On = false;
        }

        if(other.CompareTag("Player2"))
        {
            player2On = false;
        }
    }

    void CheckWin()
    {
        if (player1On && player2On && CollectibleCount.instance.totalCollected >= 14)
        {
            SceneManager.LoadScene("WinScreen");
        }
    }
}