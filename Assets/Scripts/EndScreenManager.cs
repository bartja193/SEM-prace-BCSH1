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
        finalScoreText.text = "END \n Your Final Balance: " + finalScore.ToString("N0") + " $";

        submitButton.onClick.AddListener(SubmitScore);
        mainMenuButton.onClick.AddListener(() => SceneManager.LoadScene("MainMenu"));

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