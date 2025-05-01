using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public event Action OnDeath; // ���� �̺�Ʈ

    public void OnDisable()
    {
        Die();
    }

    public void Die()
    {
        OnDeath?.Invoke(); // �̺�Ʈ ȣ��
        Destroy(gameObject);
    }
}
