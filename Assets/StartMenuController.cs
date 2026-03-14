using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class StartMenuController : MonoBehaviour
{

    public AudioMixer audioMixer;

    public GameObject MainMenu;
    public GameObject SettingsMenu;
    
    public void OnStartClick()
    {
        SceneManager.LoadScene("Level1");
    }

    public void OnQuitClick()
    {
        #if Unity_Editor
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
            Application.Quit();
    } 
       public void OpenSettings()
    {
        MainMenu.SetActive(false);
        
        SettingsMenu.SetActive(true);
    }

    public void CloseSettings()
    {
        SettingsMenu.SetActive(false);
        
        MainMenu.SetActive(true);
    }
    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);
    }

    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

}

