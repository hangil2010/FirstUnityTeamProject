using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform muzzlePoint;

    [Range(10, 30)]
    [SerializeField] float bulletSpeed;

    
    public void Fire(float damage)
    {
        GameObject instance = Instantiate(bulletPrefab, muzzlePoint.position, Quaternion.LookRotation(Vector3.forward));
        Rigidbody bulletRigidbody = instance.GetComponent<Rigidbody>();
        bulletRigidbody.velocity = muzzlePoint.forward * bulletSpeed;

        Bullet bulletComponent = instance.GetComponent<Bullet>();
        if (bulletComponent != null)
        {
            bulletComponent.SetDamage(damage);
        }
        else
        {
            Debug.LogError("������ �Ѿ� ������Ʈ�� Bullet ��ũ��Ʈ�� �����ϴ�.");
        }

    }

    

}
