using UnityEngine;

public class MarketManager : MonoBehaviour
{
    public static MarketManager Instance;

    public float baseGoldPrice = 50f;
    private float currentPrice;
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
        // Cena se postupně vrací na základní hodnotu
        supplyPressure = Mathf.Lerp(supplyPressure, 0f, Time.deltaTime * 0.1f);

        // Cena kolísá + klesá čím víc prodáváš
        currentPrice = baseGoldPrice * (1f - supplyPressure * 0.5f);
        currentPrice += Mathf.Sin(Time.time * 0.3f) * 5f;
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
        supplyPressure += amount * 0.01f;

        InventoryManager.Instance.SpendGold(amount);
        InventoryManager.Instance.AddMoney(earned);

        Debug.Log("Prodáno: " + amount + "g za $" + earned);
        Debug.Log("Aktuální cena: $" + currentPrice);
    }
}