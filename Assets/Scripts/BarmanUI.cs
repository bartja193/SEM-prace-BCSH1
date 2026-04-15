using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BarmanUI : MonoBehaviour
{
    public static BarmanUI Instance;

    public GameObject barmanPanel;
    public Button sleepButton;
    public Button eatButton;
    public Button exitButton;

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
        exitButton.onClick.AddListener(() => CloseShop());
    }

    void Update()
    {

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
    }

    void CloseShop()
    {
        isOpen = false;
        barmanPanel.SetActive(false);
    }

    void Sleep()
    {
        EnergyManager.Instance.Sleep();
    }

    void Eat()
    {
        EnergyManager.Instance.EatFood();
    }
}