using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int damageAmount;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject); // �ٴ��̳� ���� �ε����� �Ѿ��� �ı��մϴ�.
        }
        else if (collision.gameObject.tag == "Monster")
        {
            Monster monster = collision.gameObject.GetComponent<Monster>();
            if (monster != null)
            {
                //monster.TakeDamage(damage);
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("'Monster' �±װ� �پ����� Monster ��ũ��Ʈ�� ���� ������Ʈ�� �浹�߽��ϴ�!");
                Destroy(gameObject);
            }
        }
    }

    public void SetDamage(int damage)
    {
        damageAmount = damage;
    }
}