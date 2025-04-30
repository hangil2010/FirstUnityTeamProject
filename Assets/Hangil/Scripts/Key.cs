using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Key : MonoBehaviour
{
    [SerializeField] UnityEvent OnCollide;
    [Range(30, 120)][SerializeField] int rotateSpeed;
    private void Update()
    {
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
    }
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
