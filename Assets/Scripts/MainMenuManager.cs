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

        GameObject header = Instantiate(leaderboardEntryPrefab, leaderboardContent);
        TMP_Text[] headerTexts = header.GetComponentsInChildren<TMP_Text>();
        headerTexts[0].text = "Rank";
        headerTexts[1].text = "Name";
        headerTexts[2].text = "Balance";

        for (int i = 0; i < 10; i++)
        {
            GameObject go = Instantiate(leaderboardEntryPrefab, leaderboardContent);
            TMP_Text[] texts = go.GetComponentsInChildren<TMP_Text>();
            texts[0].text = (i + 1) + ".";
            texts[1].text = i < entries.Count ? entries[i].nick : "---";
            texts[2].text = i < entries.Count ? "$" + string.Format("{0:N0}", entries[i].score).Replace(",", " ") : "";
        }
    }
}