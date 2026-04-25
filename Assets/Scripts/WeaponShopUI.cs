using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WeaponShopUI : MonoBehaviour 
{
    public static WeaponShopUI Instance;

    public GameObject wShopPanel;
    public Button buyKnifeButton;
    public Button buyPitchForkButton;
    public Button exitShopButton;

    private bool isOpen = false;
    private bool  knifeBought = false;
    private bool pForkBought = false;

    private bool playerNearby = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        wShopPanel.SetActive(false);
        buyKnifeButton.onClick.AddListener(() => buyWeapon(1));
        buyPitchForkButton.onClick.AddListener(() => buyWeapon(2));
        exitShopButton.onClick.AddListener(() => CloseShop());
    }

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            ToggleShop();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            FloatingTextManager.Instance.Show("klikni E pro otevření obchodu!", Color.green);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerNearby = false;
        CloseShop();
    }

    public void ToggleShop()
    {
        if (isOpen) CloseShop();
        else OpenShop();
    }

    void OpenShop()
    {
        isOpen = true;
        wShopPanel.SetActive(true);

        buyKnifeButton.gameObject.SetActive(!knifeBought);
        buyPitchForkButton.gameObject.SetActive(!pForkBought);
    }

    public void CloseShop()
    {
        isOpen = false;
        wShopPanel.SetActive(false);
    }

    void buyWeapon(int index)
    {
        if (WeaponShopManager.Instance.GetCurrentPrice(index) > InventoryManager.Instance.money)
        {
            return;
        }
        WeaponShopManager.Instance.BuyWeapon(index);
        if (index == 1)
        {
            knifeBought = true;
        }
        else if (index == 2)
        {
            pForkBought = true;
        }
    }



}
