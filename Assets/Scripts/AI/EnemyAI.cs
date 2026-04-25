using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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

    public Slider hpSlider;
    public TextMeshProUGUI levelText;
    public int level = 1;
    private SpriteRenderer sr;


    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        currentHp = maxHp;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;
        animator = GetComponent<Animator>();
        hpSlider.maxValue = maxHp;
        hpSlider.value = currentHp;
        levelText.text = "Lv " + level;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > 0.5f && distance <= detectionRange)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.linearVelocity = direction * moveSpeed;

            if (direction.x < 0)
                sr.flipX = true;
            else if (direction.x > 0)
                sr.flipX = false;

            animator.SetBool("IsWalking", true);
        }
        else if (distance <= 0.5f)
        {
            rb.linearVelocity = Vector2.zero;
            animator.SetBool("IsWalking", false);
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            animator.SetBool("IsWalking", false);
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
        hpSlider.value = currentHp;
        if (currentHp <= 0)
        {
            InventoryManager.Instance.AddGold(reward);
            Destroy(gameObject);
        }
    }
}