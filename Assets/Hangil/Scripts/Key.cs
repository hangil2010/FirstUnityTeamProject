using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Key : MonoBehaviour
{
    [SerializeField] UnityEvent OnCollide;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("�÷��̾ ���踦 ȹ���Ͽ����ϴ�.");
            OnCollide?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
