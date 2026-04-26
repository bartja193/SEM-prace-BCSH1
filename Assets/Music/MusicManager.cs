using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    public AudioClip[] playlist;
    private AudioSource audioSource;
    private int currentTrack = 0;

    [Range(0f, 1f)] public float musicVolume = 1f;

    void Awake()
    {
        if (FindObjectsByType<MusicManager>(FindObjectsSortMode.None).Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        PlayTrack(0);
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            currentTrack = (currentTrack + 1) % playlist.Length;
            PlayTrack(currentTrack);
        }
    }

    void PlayTrack(int index)
    {
        audioSource.clip = playlist[index];
        audioSource.volume = musicVolume;
        audioSource.Play();
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        audioSource.volume = volume;
    }
}