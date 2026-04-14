using UnityEngine;
using UnityEngine.UI;

public class MiningSystem : MonoBehaviour
{
    public static MiningSystem Instance;

    public Slider miningBar;
    public float miningTime = 2f;
    public float goldPerMine = 1f;

    private bool isNearRiver = false;
    private bool isMining = false;
    private float miningProgress = 0f;
    private GoldDeposit currentDeposit = null;
    private Animator playerAnimator;

    private enum InteractType { None, River, Deposit }
    private InteractType currentInteract = InteractType.None;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        playerAnimator = GameObject.FindWithTag("Player").GetComponent<Animator>();
        miningBar.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isNearRiver
            && currentInteract == InteractType.None
            && !isMining
            && Input.GetKeyDown(KeyCode.E))
        {
            currentInteract = InteractType.River;
            isMining = true;
            miningBar.gameObject.SetActive(true);
            playerAnimator.SetBool("isPanning", true);
        }

        if (isMining)
        {
            miningProgress += Time.deltaTime;
            miningBar.value = miningProgress / miningTime;

            if (miningProgress >= miningTime)
                CompleteMining();
        }

        if (!isNearRiver && currentInteract == InteractType.River && isMining)
            ResetMining();
    }

    public bool IsMining()
    {
        return isMining;
    }

    public void StartDepositMining(GoldDeposit deposit)
    {
        if (isMining) return;
        currentDeposit = deposit;
        currentInteract = InteractType.Deposit;
        isMining = true;
        miningBar.gameObject.SetActive(true);
        playerAnimator.SetBool("isMining", true);
    }

    void CompleteMining()
    {
        // Těžení a rýžování vyžaduje energii
        if (!EnergyManager.Instance.HasEnergy(5f))
        {
            Debug.Log("Nemáš dost energie! Jdi se najíst nebo spát.");
            ResetMining();
            return;
        }

        EnergyManager.Instance.SpendEnergy(5f);



        if (currentInteract == InteractType.Deposit && currentDeposit != null)
        {
            float amount = ShopManager.Instance.GetMiningSpeed();
            currentDeposit.MinedAmount(amount);
            currentDeposit = null;
        }
        else if (currentInteract == InteractType.River)
        {
            River river = FindFirstObjectByType<River>();
            if (river != null && !river.CanMine())
            {
                Debug.Log("Řeka je vyčerpaná! Jdi spát.");
                ResetMining();
                return;
            }

            float earned = goldPerMine * ShopManager.Instance.GetMiningSpeed();
            InventoryManager.Instance.AddGold(earned);
            river?.Mine();
            Debug.Log("Vyrýžováno: " + earned + "g");
        }

        currentInteract = InteractType.None;
        ResetMining();
    }

    void ResetMining()
    {
        isMining = false;
        miningProgress = 0f;
        miningBar.value = 0f;
        miningBar.gameObject.SetActive(false);
        currentDeposit = null;
        currentInteract = InteractType.None;
        playerAnimator.SetBool("isPanning", false);
        playerAnimator.SetBool("isMining", false);
    }

    public void SetNearRiver(bool value)
    {
        isNearRiver = value;
        if (!value && currentInteract == InteractType.River)
            ResetMining();
    }
}