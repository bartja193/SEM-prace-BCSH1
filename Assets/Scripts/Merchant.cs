using UnityEngine;

public class Merchant : MonoBehaviour
{
    private bool playerNearby = false;
    public GameObject gymPanel;

    void Update()
    {

        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            ShopUI.Instance.ToggleShop();
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
        ShopUI.Instance.CloseShop();
    }
}