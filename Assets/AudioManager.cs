using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("---AudioSources---")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("---AudioClips---")]
    public AudioClip background;

    
    private static AudioManager instance;

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