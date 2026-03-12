using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetLevel : MonoBehaviour
{
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ReloadLevel();
        }
    }

    public void ReloadLevel()
    {
        
        string currentScene = SceneManager.GetActiveScene().name;
        
        SceneManager.LoadScene(currentScene);
    }
}