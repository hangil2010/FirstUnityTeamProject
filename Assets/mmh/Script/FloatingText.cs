using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float duration = 2f;
    public float floatSpeed = 1f;
    private TextMeshProUGUI text;

    void Awake()
    {
        // �ڽ� ������Ʈ �����ؼ� TextMeshProUGUI ������Ʈ ã��
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetText(string message)
    {
        if (text != null)
        {
            text.SetText(message);
        }
        else
        {
            Debug.LogWarning("FloatingText: TextMeshProUGUI�� ������� �ʾҽ��ϴ�.");
        }
    }

    void Start()
    {
        Destroy(gameObject, duration);
    }

    void Update()
    {
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;
    }
}
