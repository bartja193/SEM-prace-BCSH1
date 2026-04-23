using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlotShopUI : MonoBehaviour
{
    public static PlotShopUI Instance;

    public GameObject shopPanel;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI plotPriceText;
    public TextMeshProUGUI minerPriceText;
    public Button buyPlotButton;
    public Button buyMinerButton;
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
        buyPlotButton.onClick.AddListener(BuyPlot);
        buyMinerButton.onClick.AddListener(BuyMiner);
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
        titleText.text = "Pozemkový obchod";

        // ploty
        if (PlotManager.Instance.CanBuyPlot())
        {
            buyPlotButton.gameObject.SetActive(true);
            plotPriceText.text = "Pozemek - $" + PlotManager.Instance.GetNextPlotPrice();
        }
        else
        {
            buyPlotButton.gameObject.SetActive(false);
            plotPriceText.text = "Všechny pozemky koupeny";
        }

        // těžaři
        if (PlotManager.Instance.CanBuyMiner())
        {
            buyMinerButton.gameObject.SetActive(true);
            minerPriceText.text = "Těžař - $" + PlotManager.Instance.GetNextMinerPrice();
        }
        else
        {
            buyMinerButton.gameObject.SetActive(false);
            minerPriceText.text = PlotManager.Instance.CanBuyPlot() ? "Nejdřív kup pozemek" : "Všichni těžaři koupeni";
        }
    }

    void BuyPlot()
    {
        if (PlotManager.Instance.BuyNextPlot())
            UpdateUI();
    }

    void BuyMiner()
    {
        if (PlotManager.Instance.BuyNextMiner())
            UpdateUI();
    }
}