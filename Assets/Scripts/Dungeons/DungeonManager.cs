using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public static DungeonManager Instance;

    private bool[] dungeonActive = { true, true, true };

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

    public bool IsDungeonActive(int index)
    {
        return dungeonActive[index];
    }

    public void DeactivateDungeon(int index)
    {
        dungeonActive[index] = false;
    }

    public void ResetAllDungeons()
    {
        for (int i = 0; i < dungeonActive.Length; i++)
            dungeonActive[i] = true;
    }
}