using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "Level1") return;

        bridge = bridge = GameObject.Find("River").transform.Find("Bridge").gameObject;

        if (PlayerPrefs.GetInt("BridgeBought", 0) == 1 && bridge != null)
        {
            bridgeBought = true;
            bridge.SetActive(true);
            Physics2D.IgnoreLayerCollision(
                LayerMask.NameToLayer("Default"),
                LayerMask.NameToLayer("Bridge"),
                true
            );
        }
        else
        {
            Physics2D.IgnoreLayerCollision(
                LayerMask.NameToLayer("Default"),
                LayerMask.NameToLayer("Bridge"),
                false
            );
        }
    }

    void Start()
    {
        shopPanel.SetActive(false);

        if (bridge != null)
            bridge.SetActive(false);

        buyPickaxeButton.onClick.AddListener(() => BuyTool(1));
        buyDrillButton.onClick.AddListener(() => BuyTool(2));
        buyBridgeButton.onClick.AddListener(() => BuyBridge());
        exitShopButton.onClick.AddListener(() => CloseShop());

        pickaxePriceText.text = "Krumpáč - $" + ShopManager.Instance.availableTools[1].price;
        drillPriceText.text = "Vrtačka - $" + ShopManager.Instance.availableTools[2].price;
        bridgePriceText.text = "Most - $1500";

        if (PlayerPrefs.GetInt("BridgeBought", 0) == 1 && bridge != null)
        {
            bridgeBought = true;
            bridge.SetActive(true);
            Physics2D.IgnoreLayerCollision(
                LayerMask.NameToLayer("Default"),
                LayerMask.NameToLayer("Bridge"),
                true
            );
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
        shopPanel.SetActive(true);
        shopTitle.text = "Obchod";

        buyBridgeButton.gameObject.SetActive(!bridgeBought);
        buyPickaxeButton.gameObject.SetActive(!pickaxeBought);
        buyDrillButton.gameObject.SetActive(!drillBought);
    }

    public void CloseShop()
    {
        isOpen = false;
        shopPanel.SetActive(false);
    }

    void BuyTool(int index)
    {
        ShopManager.Instance.BuyTool(index);
        if (index == 1)
            pickaxeBought = true;
        else if (index == 2)
            drillBought = true;
    }

    void BuyBridge()
    {
        if (InventoryManager.Instance.money < 1500f)
        {
            Debug.Log("Nemáš dost peněz!");
            return;
        }

        PlayerPrefs.SetInt("BridgeBought", 1);
        InventoryManager.Instance.SpendMoney(1500f);
        bridgeBought = true;

        Physics2D.IgnoreLayerCollision(
            LayerMask.NameToLayer("Default"),
            LayerMask.NameToLayer("Bridge"),
            true
        );

        if (bridge != null)
            bridge.SetActive(true);

        Debug.Log("Most postaven!");
        CloseShop();
    }
}