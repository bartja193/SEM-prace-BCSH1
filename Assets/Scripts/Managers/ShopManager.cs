using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;

    public ToolData[] availableTools;
    private ToolData currentTool;

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
        if (availableTools.Length > 0)
            currentTool = availableTools[0];
    }

    public float GetMiningSpeed()
    {
        if (currentTool == null) return 1f;
        return currentTool.miningSpeed;
    }

    public void BuyTool(int index)
    {
        if (index >= availableTools.Length) return;

        ToolData tool = availableTools[index];

        if (InventoryManager.Instance.money < tool.price)
        {
            Debug.Log("Nemáš dost peněz! Potřebuješ $" + tool.price);
            return;
        }

        InventoryManager.Instance.SpendMoney(tool.price);
        currentTool = tool;
        Debug.Log("Koupeno: " + tool.toolName + " | rychlost: " + tool.miningSpeed);
    }

    public ToolData GetCurrentTool()
    {
        return currentTool;
    }
}