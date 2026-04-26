using UnityEngine;

public class PassiveIncome : MonoBehaviour
{
    public static PassiveIncome Instance;

    public float goldPerSecond = 0f;
    private float timer = 0f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1f)
        {
            timer = 0f;
            if (goldPerSecond > 0)
                InventoryManager.Instance.AddGold(goldPerSecond);
        }
    }

    public void AddWorker(float goldAmount)
    {
        goldPerSecond += goldAmount;
    }
}