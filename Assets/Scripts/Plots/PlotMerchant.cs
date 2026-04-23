using UnityEngine;

public class PlotMerchant : MonoBehaviour
{
    private bool playerNearby = false;

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            PlotShopUI.Instance.ToggleShop();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            FloatingTextManager.Instance.Show("E - Koupit pozemek", Color.white);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            PlotShopUI.Instance.CloseShop();
        }
    }
}