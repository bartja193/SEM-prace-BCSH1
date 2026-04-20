using UnityEngine;

public class GoldDeposit : MonoBehaviour
{
    public float maxGold = 100f;
    public float currentGold;

    private bool playerNearby = false;

    void Start()
    {
        currentGold = maxGold;
    }

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            ToolData currentTool = ShopManager.Instance.GetCurrentTool();
            if (currentTool == null || currentTool.miningSpeed <= 1f)
            {
                FloatingTextManager.Instance.Show("Potřebuješ lepší nástroj!", Color.red);
                return;
            }

            if (currentGold <= 0)
            {
                FloatingTextManager.Instance.Show("Žíla je vyčerpaná!", Color.red);
                return;
            }

            MiningSystem.Instance.StartDepositMining(this);
        }
    }

    public void MinedAmount(float amount)
    {
        currentGold -= amount;
        currentGold = Mathf.Max(currentGold, 0f);
        InventoryManager.Instance.AddGold(amount);
        Debug.Log("Vytěženo: " + amount + "g | zbývá: " + currentGold + "g");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerNearby = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerNearby = false;
    }

    public void ResetDeposit()
    {
        currentGold = maxGold;
        Debug.Log("Žíla resetována!");
    }

}