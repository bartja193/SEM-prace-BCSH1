using UnityEngine;
using UnityEngine.UI;

public class EnergyManager : MonoBehaviour
{
    public static EnergyManager Instance;

    public float maxEnergy = 100f;
    public float currentEnergy;
    public Slider energyBar;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        currentEnergy = maxEnergy;
        energyBar.maxValue = maxEnergy;
        energyBar.value = currentEnergy;
    }

    void Update()
    {
        energyBar.value = currentEnergy;
    }

    public bool HasEnergy(float amount)
    {
        return currentEnergy >= amount;
    }

    public void SpendEnergy(float amount)
    {
        currentEnergy -= amount;
        currentEnergy = Mathf.Max(currentEnergy, 0f);
    }

    public void AddEnergy(float amount)
    {
        currentEnergy += amount;
        currentEnergy = Mathf.Min(currentEnergy, maxEnergy);
    }

    public void Sleep()
    {
        if (InventoryManager.Instance.money < 100f)
        {
            return;
        }

        GoldDeposit[] deposits = FindObjectsByType<GoldDeposit>(FindObjectsSortMode.None);
        foreach (GoldDeposit deposit in deposits)
            deposit.ResetDeposit();

        River river = FindFirstObjectByType<River>();
        if (river != null)
            river.ResetRiver();

        InventoryManager.Instance.SpendMoney(100f);
        currentEnergy = maxEnergy;
        MarketManager.Instance.supplyPressure = 0f;
        SaveManager.Instance.Save();
        Debug.Log("Spal jsi! Energie obnovena, trh resetován, hra uložena.");
    }

    public void EatFood()
    {
        if (InventoryManager.Instance.money < 20f)
        {
            Debug.Log("Nemáš dost peněz na jídlo! Potřebuješ $20");
            return;
        }

        InventoryManager.Instance.SpendMoney(20f);
        AddEnergy(10f);
        Debug.Log("Snědl jsi jídlo! +10 energie");
    }
}