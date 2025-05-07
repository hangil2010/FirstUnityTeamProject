using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossHPBarController : MonoBehaviour
{
    public Image bossHPFill;
    private float maxHP = 500f;
    private float currentHP;
    private float displayHP;
    public float smoothSpeed = 5f;

    void Start()
    {
        // Level8�� �ƴ� ���, ü�¹� ��Ȱ��ȭ
        if (SceneManager.GetActiveScene().name != "Level8")
        {
            gameObject.SetActive(false); // BossHPBar ������Ʈ ��Ȱ��ȭ
            return;
        }

        currentHP = maxHP;
        displayHP = maxHP;
        UpdateUI();
    }

    void Update()
    {
        if (!gameObject.activeSelf) return;

        if (displayHP != currentHP)
        {
            displayHP = Mathf.Lerp(displayHP, currentHP, Time.deltaTime * smoothSpeed);
            UpdateUI();
        }
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
    }

    private void UpdateUI()
    {
        bossHPFill.fillAmount = displayHP / maxHP;
    }
}