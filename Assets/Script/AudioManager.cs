using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource musicSource;
    public AudioSource sfxSource;

    public AudioClip backgroundMusic;
    public AudioClip correctSFX;
    public AudioClip wrongSFX;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // keep across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        PlayMusic();
    }

    public void PlayMusic()
    {
        if (musicSource && backgroundMusic)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void PlayCorrect()
    {
        sfxSource.PlayOneShot(correctSFX);
    }

    public void PlayWrong()
    {
        sfxSource.PlayOneShot(wrongSFX);
    }

    public void ToggleMusic(bool isOn)
    {
        musicSource.mute = !isOn;
    }

    public void ToggleSFX(bool isOn)
    {
        sfxSource.mute = !isOn;
    }
}
