using TMPro;
using UnityEngine;

public class PickupTextWorldUI : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float lifetime = 2f;
    public Vector3 floatOffset = new Vector3(0, 2, 0);
    public float floatSpeed = 1f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.position += floatOffset * floatSpeed * Time.deltaTime;
        transform.LookAt(Camera.main.transform); // �׻� ī�޶� �ٶ󺸰�
    }

    public void SetText(string message)
    {
        text.text = message;
    }
}