using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public float gold = 0f;
    public float money = 0f;

    void Awake()
    {
        // Singleton - jen jedna instance existuje ve hře
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddGold(float amount)
    {
        gold += amount;
    }

    public void SpendGold(float amount)
    {
        gold -= amount;
        gold = Mathf.Max(gold, 0f);
    }

    public void AddMoney(float amount)
    {
        money += amount;
    }

    public void SpendMoney(float amount)
    {
        money -= amount;
        money = Mathf.Max(money, 0f);
    }
}