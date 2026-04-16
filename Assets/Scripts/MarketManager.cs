using UnityEngine;

public class MarketManager : MonoBehaviour
{
    public static MarketManager Instance;

    public float baseGoldPrice = 30f;
    public float currentPrice;
    public float supplyPressure = 0f;

    void Awake()
    {
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

    void Start()
    {
        currentPrice = baseGoldPrice;
    }

    void Update()
    {
        supplyPressure = Mathf.Lerp(supplyPressure, 0f, Time.deltaTime * 0.1f);

        currentPrice = baseGoldPrice * (1f - supplyPressure * 0.5f);
        currentPrice += (Mathf.PerlinNoise(Time.time * 0.2f, 0f) - 0.5f) * 60f;
        currentPrice = Mathf.Max(currentPrice, 10f);
    }

    public float GetCurrentPrice()
    {
        return currentPrice;
    }

    public void SellGold(float amount)
    {
        if (InventoryManager.Instance.gold < amount)
        {
            Debug.Log("Nemáš dost zlata!");
            return;
        }

        float earned = amount * currentPrice;
        supplyPressure += amount * 0.3f;

        InventoryManager.Instance.SpendGold(amount);
        InventoryManager.Instance.AddMoney(earned);

        Debug.Log("Prodáno: " + amount + "g za $" + earned);
        Debug.Log("Aktuální cena: $" + currentPrice);
    }
}