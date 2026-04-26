using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class MainMenuManager : MonoBehaviour
{
    public Button startButton;
    public Button leaderboardButton;
    public Button quitButton;
    public Button backButton;
    public GameObject leaderboardPanel;
    public Transform leaderboardContent;
    public GameObject leaderboardEntryPrefab;

    private LeaderboardManager leaderboardManager;

    void Start()
    {
        leaderboardManager = GetComponent<LeaderboardManager>();
        leaderboardPanel.SetActive(false);

        startButton.onClick.AddListener(() => SceneManager.LoadScene("Level1"));
        quitButton.onClick.AddListener(() => Application.Quit());
        backButton.onClick.AddListener(() => leaderboardPanel.SetActive(false));
        leaderboardButton.onClick.AddListener(ToggleLeaderboard);
    }

    void ToggleLeaderboard()
    {
        bool isActive = leaderboardPanel.activeSelf;
        leaderboardPanel.SetActive(!isActive);

        if (!isActive)
            LoadLeaderboard();
    }

    void LoadLeaderboard()
    {
        foreach (Transform child in leaderboardContent)
            Destroy(child.gameObject);

        List<LeaderboardEntry> entries = leaderboardManager.GetEntries();

        foreach (var entry in entries)
        {
            GameObject go = Instantiate(leaderboardEntryPrefab, leaderboardContent);
            go.GetComponentInChildren<TMP_Text>().text = entry.nick + " - $" + entry.score.ToString("F0");
        }
    }
}