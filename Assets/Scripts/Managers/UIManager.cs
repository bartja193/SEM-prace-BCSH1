using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public TextMeshProUGUI goldText;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI goldMText;
    public TextMeshProUGUI healthText;
    public Slider attackCooldownSlider;

    private bool isOpen = false;

    void Awake()
    {
        isOpen = false;

        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

    }

    void Update()
    {
        if (InventoryManager.Instance == null) return;

        goldText.text = InventoryManager.Instance.gold.ToString("F1") + "g";
        moneyText.text = InventoryManager.Instance.money.ToString("F0") + "$";
        healthText.text = PlayerController.Instance.currentHp.ToString("F0") + "/" + PlayerController.Instance.maxHp.ToString("F0");

        if (attackCooldownSlider != null)
        {
            attackCooldownSlider.maxValue = PlayerController.Instance.attackCooldown;
            attackCooldownSlider.value = Time.time - PlayerController.Instance.lastAttackTime;
        }

        if (isOpen)
        {
            goldMText.text = "Cena Zlata:\n" + MarketManager.Instance.currentPrice.ToString("F0") + " $";
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            TabUI();
        } 

            
    }

    void TabUI()
    {
            if (!isOpen) TabOpen();
            else TabClose();
    }

    void TabOpen()
    {
        goldMText.text = "Cena Zlata:\n" + MarketManager.Instance.currentPrice.ToString("F0") + " $";
        isOpen = true;
    }
    void TabClose()
    {
        goldMText.text = null;
        isOpen = false;
    }

}