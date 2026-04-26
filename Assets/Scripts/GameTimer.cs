using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    public static GameTimer Instance;

    [SerializeField] private TMP_Text timerText;
    private float timeRemaining = 600f;
    private bool timerRunning = true;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    void Update()
    {
        if (!timerRunning) return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            timerRunning = false;
            EndGame();
        }

        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void EndGame()
    {
        if (InventoryManager.Instance != null)
            PlayerPrefs.SetFloat("FinalScore", InventoryManager.Instance.money);
        Destroy(GameManager.Instance.gameObject);
        Destroy(PlayerController.Instance.gameObject);
        SceneManager.LoadScene("EndScene");
    }
}
