using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool IsPaused = false;

    [Header("UI")]
    public GameObject pauseMenuUI;
    public Slider musicSlider;
    public Slider sfxSlider;

    void Start()
    {
        musicSlider.value = MusicManager.Instance != null ? MusicManager.Instance.musicVolume : 1f;
        sfxSlider.value = SoundManager.Instance != null ? SoundManager.Instance.sfxVolume : 1f;

        musicSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        sfxSlider.onValueChanged.AddListener(OnSfxVolumeChanged);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused) Resume();
            else Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
    }

    public void QuitToEndScreen()
    {
        Time.timeScale = 1f;
        IsPaused = false;
        SceneManager.LoadScene("EndScene");
    }

    void OnMusicVolumeChanged(float value)
    {
        if (MusicManager.Instance != null)
            MusicManager.Instance.SetMusicVolume(value);
    }

    void OnSfxVolumeChanged(float value)
    {
        if (SoundManager.Instance != null)
            SoundManager.Instance.sfxVolume = value;
    }
}