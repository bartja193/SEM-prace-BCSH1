using UnityEngine;

public class BridgeController : MonoBehaviour
{
    public Collider2D riverBlocker; // fyzický collider řeky

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void BuildBridge()
    {
        gameObject.SetActive(true);
        if (riverBlocker != null)
            riverBlocker.enabled = false;
    }
}