using UnityEngine;

public class GoldMerchant : MonoBehaviour
{
    private bool playerNearby = false;

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
            ProdejZlato();
    }

    void ProdejZlato()
    {
        float gold = InventoryManager.Instance.gold;

        if (gold <= 0)
        {
            FloatingTextManager.Instance.Show("Nemáš žádné zlato!", Color.red);
            return;
        }

        MarketManager.Instance.SellGold(gold);
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