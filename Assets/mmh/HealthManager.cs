using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image[] hearts; // ��Ʈ �̹��� �迭
    public Sprite fullHeart; // ���� ��Ʈ
    public Sprite emptyHeart; // �� ��Ʈ
    public int health = 3; // ���� ü��
    private int maxHealth = 3; // �ִ� ü��

    void Update()
    {
        UpdateHearts();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 0)
            health = 0;
    }

    void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
                hearts[i].sprite = fullHeart;
            else
                hearts[i].sprite = emptyHeart;
        }
    }
}