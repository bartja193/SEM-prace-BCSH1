using UnityEngine;

public class GoldMerchant : MonoBehaviour
{
    private bool playerNearby = false;

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            float gold = InventoryManager.Instance.gold;

            if (gold <= 0)
            {
                Debug.Log("Nemáš žádné zlato!");
                return;
            }

            MarketManager.Instance.SellGold(gold);
            Debug.Log("Prodáno! Peníze: $" + InventoryManager.Instance.money);
        }
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
}