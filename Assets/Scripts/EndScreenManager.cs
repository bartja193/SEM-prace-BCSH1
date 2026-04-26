using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreenManager : MonoBehaviour
{
    public TMP_Text finalScoreText;
    public TMP_InputField nickInput;
    public Button submitButton;
    public Button mainMenuButton;
    public Transform leaderboardContent;
    public GameObject leaderboardEntryPrefab;

    private float finalScore;
    private LeaderboardManager leaderboardManager;

    void Start()
    {
        leaderboardManager = GetComponent<LeaderboardManager>();
        finalScore = PlayerPrefs.GetFloat("FinalScore", 0f);
        finalScoreText.text = "Výsledek: $" + finalScore.ToString("F0");

        submitButton.onClick.AddListener(SubmitScore);
        mainMenuButton.onClick.AddListener(() => SceneManager.LoadScene("Level1"));

        LoadLeaderboard();
    }

    void SubmitScore()
    {
        string nick = nickInput.text.Trim();
        if (string.IsNullOrEmpty(nick)) return;

        leaderboardManager.AddEntry(nick, finalScore);

        LoadLeaderboard();
        submitButton.interactable = false;
        nickInput.interactable = false;
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