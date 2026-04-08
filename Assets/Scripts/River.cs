using UnityEngine;

public class River : MonoBehaviour
{
    public int maxMines = 20;
    private int currentMines;

    void Start()
    {
        currentMines = maxMines;
    }

    public bool CanMine()
    {
        return currentMines > 0;
    }

    public void Mine()
    {
        currentMines--;
        Debug.Log("Těžení v řece: " + currentMines + "/" + maxMines);
    }

    public void ResetRiver()
    {
        currentMines = maxMines;
        Debug.Log("Řeka resetována!");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            MiningSystem.Instance.SetNearRiver(true);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            MiningSystem.Instance.SetNearRiver(false);
    }
}