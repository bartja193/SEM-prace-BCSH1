using UnityEngine;
using TMPro;
using UnityEngine.UI;



public class Gym : MonoBehaviour
{
    public static Gym Instance;

    public GameObject GymPanel;
    public Button buyMaxHPButton;
    public Button buyDMGButton;
    public Button buySpeedButton;
    public Button buyEnergyButton;

    private int cHP = 0;
    private int cDMG = 0;
    private int cSpeed = 0;
    private int cEnergy = 0;
    private bool HPbuy = false;
    private bool DMGbuy = false;
    private bool Speedbuy = false;
    private bool Energybuy = false;


    private bool playerNearby = false;
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
        GymPanel.SetActive(false);

        buyMaxHPButton.onClick.AddListener(() => AddMaxHP());
        buyDMGButton.onClick.AddListener(() => AddDMG());
        buySpeedButton.onClick.AddListener(() => AddSpeed());
        buyEnergyButton.onClick.AddListener(() => AddEnergy());
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

    void ToggleShop()
    {
        if (isOpen) CloseShop();
        else OpenShop();
    }

    void OpenShop()
    {
        isOpen = true;
        GymPanel.SetActive(true);

        buyMaxHPButton.gameObject.SetActive(!HPbuy);
        buyDMGButton.gameObject.SetActive(!DMGbuy);
        buySpeedButton.gameObject.SetActive(!Speedbuy);
    }

    void CloseShop()
    {
        isOpen = false;
        GymPanel.SetActive(false);
    }

    void AddMaxHP()
    {
        if (cHP < 30)
        {

            if (InventoryManager.Instance.money < 500f)
            {
                Debug.Log("Nemáš dost peněz! Potřebuješ $500");
                return;
            }

            InventoryManager.Instance.SpendMoney(500f);
            FindObjectOfType<PlayerController>().AddMaxHP(1);
            cHP++;
        }
        else
        {
            HPbuy = true;
            Debug.Log("MaxHPKoupeno!");
            buyMaxHPButton.gameObject.SetActive(false);
        }
    }
    void AddDMG()
    {
        if (cDMG < 10)
        {
            if (InventoryManager.Instance.money < 500f)
            {
                Debug.Log("Nemáš dost peněz! Potřebuješ $500");
                return;
                cDMG++;
            }

            InventoryManager.Instance.SpendMoney(500f);
            FindObjectOfType<PlayerController>().AddDMG(1);
        }
        else
        {
            DMGbuy = true;
            Debug.Log("MaxDMGKoupen!");
            buyDMGButton.gameObject.SetActive(false);
        }
    }
    void AddSpeed()
    {
        if (cSpeed < 10)
        {
            if (InventoryManager.Instance.money < 500f)
            {
                Debug.Log("Nemáš dost peněz! Potřebuješ $500");
                return;
            }

            InventoryManager.Instance.SpendMoney(500f);
            FindObjectOfType<PlayerController>().AddSpeed(1);
            cSpeed++;
        }
        else
        {
            Speedbuy = true;
            Debug.Log("MaxSpeedKoupen!");
            buySpeedButton.gameObject.SetActive(false);    
        }
    }

    void AddEnergy()
    {
        if (cSpeed < 10)
        {
            if (InventoryManager.Instance.money < 500f)
            {
                Debug.Log("Nemáš dost peněz! Potřebuješ $500");
                return;
            }

            InventoryManager.Instance.SpendMoney(500f);
            FindObjectOfType<EnergyManager>().AddMaxEnergy(10f);
            cEnergy++;
        }
        else
        {
            Energybuy = true;
            Debug.Log("MaxSpeedKoupen!");
            buyEnergyButton.gameObject.SetActive(false);
        }
    }

}

