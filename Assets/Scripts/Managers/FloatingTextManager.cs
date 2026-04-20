using UnityEngine;

public class FloatingTextManager : MonoBehaviour
{
    public static FloatingTextManager Instance { get; private set; }

    [SerializeField] private GameObject floatingTextPrefab;
    private Transform playerTransform;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    public void Show(string text, Color color)
    {
        Vector3 pos = playerTransform.position + Vector3.up * 2f;
        GameObject go = Instantiate(floatingTextPrefab, pos, Quaternion.identity);
        go.GetComponent<FloatingText>().SetText(text, color);
    }
}