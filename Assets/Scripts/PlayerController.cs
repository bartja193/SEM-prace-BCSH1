using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Čte input z klávesnice (WASD nebo šipky)
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // F5 = uložit, F9 = načíst
        if (Input.GetKeyDown(KeyCode.F5))
            SaveManager.Instance.Save();

        if (Input.GetKeyDown(KeyCode.F9))
            SaveManager.Instance.Load();

        // B = Nákup pickaxu
        if (Input.GetKeyDown(KeyCode.B))
        ShopManager.Instance.BuyTool(1);

        bool isMoving = movement.magnitude > 0;
        animator.SetBool("isMoving", isMoving);

        // Otočení podle směru
        if (movement.x < 0)
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
        else if (movement.x > 0)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movement.normalized * moveSpeed;
    }
}
