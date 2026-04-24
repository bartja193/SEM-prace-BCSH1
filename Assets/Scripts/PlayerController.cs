using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 movement;

    public int maxHp = 10;
    private int currentHp;
    public Slider healthSlider;

    public float attackRange = 1.5f;
    public float attackCooldown = 150f;
    private float lastAttackTime = 0f;

    void Awake()
    {
        if (FindObjectsByType<PlayerController>(FindObjectsSortMode.None).Length > 1)
        {
            Destroy(gameObject);
            return;
        }

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
        UpdateSlider();

        if (currentHp <= 0)
        {
            Debug.Log("SceneTransition flag: " + PlayerPrefs.GetInt("SceneTransition", 0));
            Debug.Log("SpawnX: " + PlayerPrefs.GetFloat("SpawnX", -999));
            Debug.Log("Hráč zemřel");
            currentHp = maxHp;
            if (InventoryManager.Instance != null)
            {
                InventoryManager.Instance.SpendMoney(500f);
            }
            PlayerPrefs.SetFloat("SpawnX", -65f);
            PlayerPrefs.SetFloat("SpawnY", 20f);
            PlayerPrefs.SetInt("SceneTransition", 1);
            SceneManager.LoadScene("Level1");

            Debug.Log("SceneTransition flag: " + PlayerPrefs.GetInt("SceneTransition", 0));
            Debug.Log("SpawnX: " + PlayerPrefs.GetFloat("SpawnX", -999));
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

        lastAttackTime = Time.time;
        Debug.Log("attack");



        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange);
        foreach (Collider2D hit in hits)
        {
            EnemyAI enemy = hit.GetComponent<EnemyAI>();
            if (enemy != null)
            {
                animator.SetTrigger("Attack");
                enemy.TakeDamage(1);
            }
        }
    }

    public void Heal(int amount)
    {
        currentHp += amount;
        currentHp = Mathf.Min(currentHp, maxHp);
        UpdateSlider();
    }
}
