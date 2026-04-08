using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public TextMeshProUGUI goldText;
    public TextMeshProUGUI moneyText;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        if (InventoryManager.Instance == null) return;

        goldText.text = "Zlato: " + InventoryManager.Instance.gold.ToString("F1") + "g";
        moneyText.text = "Peníze: $" + InventoryManager.Instance.money.ToString("F0");
    }
}