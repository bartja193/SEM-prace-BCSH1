using UnityEngine;
using UnityEngine.SceneManagement;

public class MinerAI : MonoBehaviour
{
    public Transform minePoint;
    public Transform depositPoint;
    public float moveSpeed = 4f;
    public float miningTime = 30f;
    public float goldPerTrip = 20f;


    private enum State { GoingToMine, Mining, Returning }
    private State state = State.GoingToMine;

    private Rigidbody2D rb;
    private float miningTimer = 0f;
    private Animator animator;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        switch (state)
        {
            case State.GoingToMine:
                MoveTowards(minePoint.position);
                if (Vector2.Distance(transform.position, minePoint.position) < 0.2f)
                {
                    animator.SetBool("IsWalking", true);
                    animator.SetBool("IsMining", false);
                    rb.linearVelocity = Vector2.zero;
                    miningTimer = 0f;
                    state = State.Mining;
                }
                break;

            case State.Mining:
                animator.SetBool("IsWalking", false);
                animator.SetBool("IsMining", true);
                miningTimer += Time.deltaTime;
                if (miningTimer >= miningTime)
                    state = State.Returning;
                break;

            case State.Returning:
                animator.SetBool("IsWalking", true);
                animator.SetBool("IsMining", false);
                MoveTowards(depositPoint.position);
                if (Vector2.Distance(transform.position, depositPoint.position) < 0.2f)
                {
                    rb.linearVelocity = Vector2.zero;
                    InventoryManager.Instance.AddGold(goldPerTrip);
                    FloatingTextManager.Instance.Show("+" + goldPerTrip + " zlato", Color.yellow);
                    state = State.GoingToMine;
                }
                break;
        }
    }

    void MoveTowards(Vector3 target)
    {
        Vector2 direction = (target - transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed;

        if (direction.x < 0)
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
        else if (direction.x > 0)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
    }
}