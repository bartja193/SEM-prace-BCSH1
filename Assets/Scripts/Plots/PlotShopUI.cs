using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlotShopUI : MonoBehaviour
{
    public static PlotShopUI Instance;

    public GameObject shopPanel;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI priceText;
    public Button buyButton;
    public Button closeButton;

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
        shopPanel.SetActive(false);
        buyButton.onClick.AddListener(BuyPlot);
        closeButton.onClick.AddListener(CloseShop);
    }

    public void ToggleShop()
    {
        if (isOpen) CloseShop();
        else OpenShop();
    }

    void OpenShop()
    {
        isOpen = true;
        shopPanel.SetActive(true);
        UpdateUI();
    }

    public void CloseShop()
    {
        isOpen = false;
        shopPanel.SetActive(false);
    }

    void UpdateUI()
    {
        if (!PlotManager.Instance.CanBuyMore())
        {
            titleText.text = "Všechny pozemky koupeny";
            priceText.text = "";
            buyButton.gameObject.SetActive(false);
            return;
        }

        titleText.text = "Pozemkový obchod";
        priceText.text = "Cena: $" + PlotManager.Instance.GetNextPlotPrice();
        buyButton.gameObject.SetActive(true);
    }

    void BuyPlot()
    {
        if (PlotManager.Instance.BuyNextPlot())
            UpdateUI();
    }
}