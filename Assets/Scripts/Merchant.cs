using UnityEngine;

public class Merchant : MonoBehaviour
{
    private bool playerNearby = false;

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
            Debug.Log("Hráč u obchodníka");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerNearby = false;
    }
}