using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombLogic : MonoBehaviour
{
    [SerializeField] private float explosionRadius = 3f;
    [SerializeField] private float explosionDamage = 20f;
    [SerializeField] private GameObject explosionEffectPrefab;
    [SerializeField] private float explosionDelay = 0f;




    public void Explode()
    {
        if (explosionDelay > 0)
        {
            Invoke("PerformExplosion", explosionDelay);
        }
        else
        {
            PerformExplosion();
        }
    }

    private void PerformExplosion()
    {
        // 1. �ð� ȿ�� ����
        if (explosionEffectPrefab != null)
        {

        }
        else
        {
            Debug.LogWarning("���� ȿ�� �������� �Ҵ���� �ʾҽ��ϴ�.");
            // �⺻���� �ð� ȿ�� ���� (���� ����)
        }

        // 2. �ֺ� ������Ʈ�� ������ ����
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider hitCollider in hitColliders)
        {
            Monster monster = hitCollider.GetComponent<Monster>();
            if (monster != null)
            {
                Debug.Log($"{monster.gameObject.name}�� ���߿� �¾� {explosionDamage} �������� �Ծ����ϴ�.");
                //monster.TakeDamage(explosionDamage);
                // �˹� �� �߰� ȿ��
                Rigidbody rb = monster.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Vector3 explosionDirection = (monster.transform.position - transform.position).normalized;
                    rb.AddForce(explosionDirection * 5f, ForceMode.Impulse);
                }
            }
            // �ٸ� ������Ʈ�� ���� ó�� �߰� ����
        }

        // 3. ��ź ������Ʈ ����
        Destroy(gameObject);
    }
}
