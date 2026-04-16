using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public static ShopUI Instance;

    public GameObject shopPanel;
    public TextMeshProUGUI shopTitle;
    public Button buyPickaxeButton;
    public Button buyDrillButton;
    public Button buyBridgeButton;
    public Button exitShopButton;
    public TextMeshProUGUI pickaxePriceText;
    public TextMeshProUGUI drillPriceText;
    public TextMeshProUGUI bridgePriceText;

    public GameObject bridge;

    private bool isOpen = false;
    private bool bridgeBought = false;
    private bool pickaxeBought = false;
    private bool drillBought = false;


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

        buyPickaxeButton.onClick.AddListener(() => BuyTool(1));
        buyDrillButton.onClick.AddListener(() => BuyTool(2));
        buyBridgeButton.onClick.AddListener(() => BuyBridge());
        exitShopButton.onClick.AddListener(() => CloseShop());

        pickaxePriceText.text = "Krumpáč - $" + ShopManager.Instance.availableTools[1].price;
        drillPriceText.text = "Vrtačka - $" + ShopManager.Instance.availableTools[2].price;
        bridgePriceText.text = "Most - $5000";
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
        shopTitle.text = "Obchod";

        buyBridgeButton.gameObject.SetActive(!bridgeBought);
        buyPickaxeButton.gameObject.SetActive(!bridgeBought);
    }

    void CloseShop()
    {
        isOpen = false;
        shopPanel.SetActive(false);
    }

    void BuyTool(int index)
    {
        ShopManager.Instance.BuyTool(index);
        if (index == 0) {
            pickaxeBought = true;
        } else if (index == 1) {
            drillBought = true;
        }
    }

    void BuyBridge()
    {
        if (InventoryManager.Instance.money < 5000f)
        {
            Debug.Log("Nemáš dost peněz!");
            return;
        }

        InventoryManager.Instance.SpendMoney(5000f);
        bridgeBought = true;

        Physics2D.IgnoreLayerCollision(
            LayerMask.NameToLayer("Default"),
            LayerMask.NameToLayer("Bridge"),
            true
        );

        bridge.SetActive(true);
        Debug.Log("Most postaven!");
        CloseShop();
    }
}