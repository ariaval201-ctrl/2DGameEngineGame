using UnityEngine;

public class MenuManager : MonoBehaviour
{
[SerializeField] private GameObject pauseMenuUI;
[SerializeField] private GameObject OptionsPageUI;
[SerializeField] private GameObject SettingsPageUI;
[SerializeField] private GameObject ControlsPageUI;
[SerializeField] private GameObject SpecialPageUI;
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

    public void OpenSettings()
    {
        OptionsPageUI.SetActive(false);
        SettingsPageUI.SetActive(true);
    }
    public void OpenControls()
    {
        OptionsPageUI.SetActive(false);
        ControlsPageUI.SetActive(true);
    }
    public void OpenSpecial()
    {
        OptionsPageUI.SetActive(false);
        SpecialPageUI.SetActive(true);
    }
    public void QuitGameButton()
    {
        Debug.Log("Quit Button Pressed");
        Application.Quit();
    }

    public void BackButton()
    {
    if(SettingsPageUI.activeSelf)
    {
        SettingsPageUI.SetActive(false);
        OptionsPageUI.SetActive(true);
    }
    else if(ControlsPageUI.activeSelf)
        {
            ControlsPageUI.SetActive(false);
            OptionsPageUI.SetActive(true);
        }
    else if(SpecialPageUI.activeSelf)
        {
            SpecialPageUI.SetActive(false);
            OptionsPageUI.SetActive(true);
        }
    else if(OptionsPageUI.activeSelf)
        {
            Resume();
        }
    }
}
