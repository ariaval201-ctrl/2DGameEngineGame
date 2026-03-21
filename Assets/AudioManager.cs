using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header("---AudioSources---")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("---AudioClips---")]
    public AudioClip background;

    public float startVolumeDB = -20f;
    private static AudioManager instance;
    [SerializeField] AudioMixer musicMixer;

    private void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    private void Start()
    {
        musicSource.clip = background;
        musicMixer.SetFloat("MasterVolume", -20f);
        musicSource.Play();
    }

    private void Update()
    {
        if (musicSource.isPlaying)
        {
            Debug.Log("Music is playing");
        }
        else
        {
            Debug.Log("Music is NOT playing");
        }
    }
}