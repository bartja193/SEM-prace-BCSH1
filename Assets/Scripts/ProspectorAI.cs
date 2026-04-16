using UnityEngine;

public class ProspectorAI : MonoBehaviour
{

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    public float moveSpeed = 2f;
    public float miningTime = 3f;
    public float goldTarget = 10f;

    private float collectedGold = 0f;
    private float miningTimer = 0f;

    private Transform riverEdge;
    private Transform merchantEdge;

    private enum State { GoToRiver, Mining, GoToMerchant, Selling }
    private State currentState = State.GoToRiver;

    void Start()
    {
        riverEdge = GameObject.Find("riverEdge").transform;
        merchantEdge = GameObject.Find("merchantEdge").transform;

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

        animator.SetBool("isMoving", false);
        switch (currentState)
        {
            case State.GoToRiver:
                MoveToTarget(riverEdge.position);
                if (Vector2.Distance(transform.position, riverEdge.position) < 1f)
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
                MoveToTarget(merchantEdge.position);
                if (Vector2.Distance(transform.position, merchantEdge.position) < 1f)
                {
                    if (MarketManager.Instance.GetCurrentPrice() >= 40f)
                    {
                        currentState = State.Selling;
                    }
                    else
                    {
                        Debug.Log("Čeká na lepší cenu");
                    }
                }
                break;

            case State.Selling:
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
        animator.SetBool("isMoving", true);

        if (direction.x < 0)
            spriteRenderer.flipX = true;
        else if (direction.x > 0)
            spriteRenderer.flipX = false;
    }
}