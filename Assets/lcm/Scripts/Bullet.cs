using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float damageAmount;
    Player player;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject, 3);
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

    public void SetDamage(float damage)
    {
        damageAmount = damage;
    }
}
