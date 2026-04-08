using UnityEngine;

public class ProspectorAI : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float miningTime = 3f;
    public float goldTarget = 10f;

    private float collectedGold = 0f;
    private float miningTimer = 0f;

    private Transform river;
    private Transform goldMerchant;

    private enum State { GoToRiver, Mining, GoToMerchant, Selling }
    private State currentState = State.GoToRiver;

    void Start()
    {
        river = GameObject.Find("River").transform;
        goldMerchant = GameObject.Find("GoldMerchant").transform;
    }

    void Update()
    {
        switch (currentState)
        {
            case State.GoToRiver:
                MoveToTarget(river.position);
                if (Vector2.Distance(transform.position, river.position) < 1f)
                    currentState = State.Mining;
                break;

            case State.Mining:
                miningTimer += Time.deltaTime;
                if (miningTimer >= miningTime)
                {
                    miningTimer = 0f;
                    collectedGold += 1f;
                    Debug.Log("NPC vyrýžoval 1g | celkem: " + collectedGold);

                    if (collectedGold >= goldTarget)
                        currentState = State.GoToMerchant;
                }
                break;

            case State.GoToMerchant:
                MoveToTarget(goldMerchant.position);
                if (Vector2.Distance(transform.position, goldMerchant.position) < 1f)
                    currentState = State.Selling;
                break;

            case State.Selling:
                // Ovlivní cenu na trhu
                MarketManager.Instance.supplyPressure += collectedGold * 0.01f;
                Debug.Log("NPC prodal " + collectedGold + "g | trh ovlivněn");
                collectedGold = 0f;
                currentState = State.GoToRiver;
                break;
        }
    }

    void MoveToTarget(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }
}