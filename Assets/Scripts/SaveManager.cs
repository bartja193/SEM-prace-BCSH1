using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    private string savePath;

    [System.Serializable]
    public class SaveData
    {
        public float gold;
        public float money;
        public int unlockedLevel;
        public int currentToolIndex;
        public float supplyPressure;
    }

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

        savePath = Application.persistentDataPath + "/save.json";
    }

    public void Save()
    {
        SaveData data = new SaveData();
        data.gold = InventoryManager.Instance.gold;
        data.money = InventoryManager.Instance.money;
        data.supplyPressure = MarketManager.Instance.supplyPressure;
        data.currentToolIndex = System.Array.IndexOf(
        ShopManager.Instance.availableTools,
        ShopManager.Instance.GetCurrentTool()
        );
        data.unlockedLevel = 1; // zatím pevně, později dynamicky

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);

        Debug.Log("Uloženo do: " + savePath);
    }

    public void Load()
    {
        if (!File.Exists(savePath))
        {
            Debug.Log("Save soubor neexistuje, začínám novou hru.");
            return;
        }

        string json = File.ReadAllText(savePath);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        ShopManager.Instance.BuyTool(data.currentToolIndex);
        MarketManager.Instance.supplyPressure = data.supplyPressure;
        InventoryManager.Instance.gold = data.gold;
        InventoryManager.Instance.money = data.money;

        Debug.Log("Načteno! Peníze: $" + data.money);
    }
}