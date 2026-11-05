using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic Instance { get; private set; }
    private AudioClip menuMusic;
    private AudioClip gameMusic;
    private AudioSource audioSource;
    private float volume = 1.0f; // Default volume

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            audioSource.volume = volume;
            LoadAudioClips();
        }
    }

    void Start()
    {
        PlayMenuMusic();
    }

    void PlayMenuMusic()
    {
        if(audioSource.isPlaying && audioSource.clip == menuMusic)
        {
            return; // Already playing menu music
        }
        audioSource.clip = menuMusic;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlayGameMusic()
    {
        if(audioSource.isPlaying && audioSource.clip == gameMusic)
        {
            return; // Already playing game music
        }
        audioSource.clip = gameMusic;
        audioSource.loop = true;
        audioSource.Play();
    }


    public void SetVolume(float newVolume)
    {
        volume = Mathf.Clamp01(newVolume); // Ensure volume is between 0 and 1
        audioSource.volume = volume;
    }


    private void LoadAudioClips()
    {
        menuMusic = Resources.Load<AudioClip>("Sound/MenuMusic");
        gameMusic = Resources.Load<AudioClip>("Sound/GameMusic");
    }
}
