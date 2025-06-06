using TMPro;
using UnityEngine;

public class MoneyText : MonoBehaviour
{
    public float lifetime = 2f;           // ������� ������� ����
    public float floatSpeed = 0.5f;       // �������� �������
    public float fadeDuration = 1.5f;     // ����� ������������
    public Vector3 floatDirection = Vector3.up;

    [SerializeField] private TextMeshProUGUI textMesh;
    private Color originalColor;
    private float timer;

    void Start()
    {
        //textMesh = GetComponent<TextMeshProUGUI>();
        originalColor = textMesh.color;
        timer = 0f;
    }

    void Update()
    {
        timer += Time.deltaTime;

        // ��������� ������
        transform.position += floatDirection * floatSpeed * Time.deltaTime;

        transform.LookAt(Camera.main.transform.position);
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);

        // ������ ��������� �����
        float fade = 1f - Mathf.Clamp01(timer / fadeDuration);
        textMesh.color = new Color(originalColor.r, originalColor.g, originalColor.b, fade);

        // ������� ����� ��������� �����
        if (timer >= lifetime)
        {
            Destroy(gameObject);
        }
    }

    // ��������� ������ ����� ��� ������
    public void SetText(string message, Color? color = null)
    {
        //if (textMesh == null) textMesh = GetComponent<TextMeshProUGUI>();
        textMesh.text = message;
        if (color.HasValue)
        {
            textMesh.color = color.Value;
            originalColor = color.Value;
        }
    }
}
