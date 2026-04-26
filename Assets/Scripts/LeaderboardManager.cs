using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class LeaderboardEntry
{
    public string nick;
    public float score;
}

[System.Serializable]
public class Leaderboard
{
    public List<LeaderboardEntry> entries = new List<LeaderboardEntry>();
}

public class LeaderboardManager : MonoBehaviour
{
    private string filePath;
    private Leaderboard leaderboard;

    void Awake()
    {
        filePath = Application.persistentDataPath + "/leaderboard.json";
        Load();
    }

    public void AddEntry(string nick, float score)
    {
        leaderboard.entries.Add(new LeaderboardEntry { nick = nick, score = score });
        leaderboard.entries.Sort((a, b) => b.score.CompareTo(a.score));
        Save();
    }

    public List<LeaderboardEntry> GetEntries()
    {
        return leaderboard.entries;
    }

    void Save()
    {
        string json = JsonUtility.ToJson(leaderboard, true);
        File.WriteAllText(filePath, json);
    }

    void Load()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            leaderboard = JsonUtility.FromJson<Leaderboard>(json);
        }
        else
        {
            leaderboard = new Leaderboard();
        }
    }
}