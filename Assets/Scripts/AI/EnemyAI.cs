using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float detectionRange = 12f;
    public float moveSpeed = 3f;
    public int maxHp = 3;
    public float reward = 5;

    private int currentHp;
    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;


    private float damageCooldown = 2f;
    private float lastDamageTime = 0f;



    void Start()
    {
        currentHp = maxHp;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null)
        {
            return;
        }


        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.linearVelocity = direction * moveSpeed;
            if (direction.x < 0)
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
            else if (direction.x > 0)
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
            rb.linearVelocity = Vector2.zero;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player") && Time.time - lastDamageTime >= damageCooldown)
        {
            animator.SetBool("IsAttacking", true);
            lastDamageTime = Time.time;
            col.GetComponent<PlayerController>().TakeDamage(1);
            Debug.Log("Damage!");
        }
    }

    public void TakeDamage(int amount)
    {
        currentHp -= amount;
        if (currentHp <= 0)
        {
            InventoryManager.Instance.AddGold(reward);
            Destroy(gameObject);
        }
    }
}