using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    public int maxHp = 10;
    public int damage = 1;
    public float moveSpeed = 5f;
    public int currentHp;

    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 movement;

    public Slider healthSlider;

    public float attackRange = 1.5f;
    public float attackCooldown = 150f;
    public float lastAttackTime = 0f;

    void Awake()
    {
        if (FindObjectsByType<PlayerController>(FindObjectsSortMode.None).Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHp = maxHp;
        UpdateSlider();
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (PlayerPrefs.GetInt("SceneTransition", 0) == 1)
        {
            float x = PlayerPrefs.GetFloat("SpawnX");
            float y = PlayerPrefs.GetFloat("SpawnY");
            transform.position = new Vector2(x, y);
            PlayerPrefs.DeleteKey("SpawnX");
            PlayerPrefs.DeleteKey("SpawnY");
            PlayerPrefs.DeleteKey("SceneTransition");
        }
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.F5))
            SaveManager.Instance.Save();

        if (Input.GetKeyDown(KeyCode.F9))
            SaveManager.Instance.Load();

        bool isMoving = movement.magnitude > 0;
        animator.SetBool("isMoving", isMoving);

        if (movement.x < 0)
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
        else if (movement.x > 0)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            damage = WeaponShopManager.Instance.GetCurrentDMG();
            Attack();
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movement.normalized * moveSpeed;
    }

    public void TakeDamage(int amount)
    {
        currentHp -= amount;
        currentHp = Mathf.Max(currentHp, 0);
        FloatingTextManager.Instance.Show("-" + amount, Color.red);
        UpdateSlider();

        if (currentHp <= 0)
        {
            Debug.Log("Hráč zemřel");
            if (InventoryManager.Instance != null)
            {
                InventoryManager.Instance.SpendMoney(500f);
            }
            currentHp = maxHp;
            UpdateSlider();
            PlayerPrefs.SetFloat("SpawnX", -65f);
            PlayerPrefs.SetFloat("SpawnY", 20f);
            PlayerPrefs.SetInt("SceneTransition", 1);
            SceneManager.LoadScene("Level1");
        }
    }

    void UpdateSlider()
    {
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHp;
            healthSlider.value = currentHp;
        }
    }

    void Attack()
    {
        if (Time.time - lastAttackTime < attackCooldown)
        {
            Debug.Log("Cd na attack");
            return;
        }

        Debug.Log("attack");


        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, WeaponShopManager.Instance.GetCurrentRange());
        foreach (Collider2D hit in hits)
        {
            EnemyAI enemy = hit.GetComponent<EnemyAI>();
            if (enemy != null)
            {
                lastAttackTime = Time.time;
                FloatingTextManager.Instance.Show("-" + damage, Color.green);
                animator.SetTrigger("Attack");
                enemy.TakeDamage(damage);
            }
        }
    }

    public void Heal(int amount)
    {
        currentHp += amount;
        currentHp = Mathf.Min(currentHp, maxHp);
        UpdateSlider();
    }

    public void AddMaxHP(int amount)
    {
        maxHp += amount;
        currentHp += amount;
        UpdateSlider();
    }

    public void AddDMG(int amount)
    {
        damage += amount;
    }


    public void AddSpeed(float amount)
    {
        moveSpeed += amount;
    }
}
