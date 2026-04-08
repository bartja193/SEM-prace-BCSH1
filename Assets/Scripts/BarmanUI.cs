using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BarmanUI : MonoBehaviour
{
    public static BarmanUI Instance;

    public GameObject barmanPanel;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI energyText;
    public Button sleepButton;
    public Button eatButton;

    private bool isOpen = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        barmanPanel.SetActive(false);

        sleepButton.onClick.AddListener(() => Sleep());
        eatButton.onClick.AddListener(() => Eat());
    }

    void Update()
    {
        if (isOpen)
        {
            energyText.text = "Energie: " + EnergyManager.Instance.currentEnergy + "/"
                            + EnergyManager.Instance.maxEnergy
                            + "\nPeníze: $" + InventoryManager.Instance.money.ToString("F0");
        }
    }

    public void ToggleShop()
    {
        if (isOpen) CloseShop();
        else OpenShop();
    }

    void OpenShop()
    {
        isOpen = true;
        barmanPanel.SetActive(true);
        titleText.text = "Hospoda\nSpánek: $100 | Jídlo: $20";
    }

    void CloseShop()
    {
        isOpen = false;
        barmanPanel.SetActive(false);
    }

    void Sleep()
    {
        EnergyManager.Instance.Sleep();
        CloseShop();
    }

    void Eat()
    {
        EnergyManager.Instance.EatFood();
    }
}