using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip[] playlist;
    private AudioSource audioSource;
    private int currentTrack = 0;

    void Awake()
    {
        if (FindObjectsByType<MusicManager>(FindObjectsSortMode.None).Length > 1)
        {
            Destroy(gameObject);
            return;
        }
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
        audioSource.Play();
    }
}