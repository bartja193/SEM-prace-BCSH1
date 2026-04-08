using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movement.normalized * moveSpeed;
    }
}
