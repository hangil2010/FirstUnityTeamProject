using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyDeath : MonoBehaviour
{
    //public event Action OnDeath; // ���� �̺�Ʈ
    [SerializeField] UnityEvent OnDeath;
    public void OnDisable()
    {
        Die();
    }

    public void Die()
    {
        OnDeath?.Invoke();
    }
}
