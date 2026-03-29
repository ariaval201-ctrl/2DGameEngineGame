using UnityEngine;

public class MenuManager : MonoBehaviour
{
[SerializeField] private GameObject pauseMenuUI;
private bool isPaused = false;

void Update()
{
    if (Input.GetKeyDown(KeyCode.Escape))
    {
        if (isPaused)
            Resume();
        else
            Pause();
        }
    }


    void Pause()
    {
        pauseMenuUI.SetActive(true);
        //time.timescale because that controls the speed, not time.deltatime (idk why but sure)
        Time.timeScale = 0f;
        isPaused = true;
    }

    void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
}
