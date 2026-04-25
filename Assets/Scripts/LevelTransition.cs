using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    public string targetScene;
    public Vector2 spawnPosition;

    public bool isDungeonEntrance = false;
    public int dungeonIndex = 0;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (isDungeonEntrance && !DungeonManager.Instance.IsDungeonActive(dungeonIndex))
            {
                FloatingTextManager.Instance.Show("Dungeon je uzavřen!", Color.red);
                return;
            }

            if (isDungeonEntrance)
                DungeonManager.Instance.DeactivateDungeon(dungeonIndex);

            PlayerPrefs.SetFloat("SpawnX", spawnPosition.x);
            PlayerPrefs.SetFloat("SpawnY", spawnPosition.y);
            PlayerPrefs.SetInt("SceneTransition", 1);
            SceneManager.LoadScene(targetScene);
        }
    }
}
