using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    public float moveSpeed = 1.5f;
    public float fadeDuration = 1.5f;

    private TextMeshPro tmp;
    private Color originalColor;
    private float timer;

    void Awake()
    {
        tmp = GetComponent<TextMeshPro>();
        GetComponent<MeshRenderer>().sortingOrder = 1000;
        tmp.fontSize = 5;
        tmp.color = Color.red;
        Debug.Log("FloatingText pozice: " + transform.position);
    }

    void Update()
    {
        timer += Time.deltaTime;

        // pohyb nahoru
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;

        // postupné mizení
        float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
        tmp.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

        if (timer >= fadeDuration)
            Destroy(gameObject);
    }

    public void SetText(string text, Color color)
    {
        tmp.text = text;
        tmp.color = color;
        originalColor = color;
    }
}