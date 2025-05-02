using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class BossBullet_ObjectPool : MonoBehaviour
{
    [SerializeField] List<PooledObject> pool = new List<PooledObject>(); //������Ʈ Ǯ�� ������ �ڷ���
    [SerializeField] PooledObject prefab; // Ǯ���� ������Ʈ
    [SerializeField] int size; //�ִ����

    private void Awake()
    {
        for (int i = 0; i < size; i++) //�ִ������ ������ ��ŭ ���� ������Ʈ Ǯ ����
        {
            PooledObject instance = Instantiate(prefab);
            instance.gameObject.SetActive(false);
            pool.Add(instance);
        }
    }

    public PooledObject GetPool(Vector3 position, Quaternion rotation) //������
    {
        if (pool.Count == 0)
        {
            return Instantiate(prefab, position, rotation);
        }
        
        PooledObject instance = pool[pool.Count - 1];
        pool.RemoveAt(pool.Count -1);

        instance.returnPool = this;
        instance.transform.position = position;
        instance.transform.rotation = rotation;      
        instance.gameObject.SetActive(true);

        return instance;
    }

    public void ReturnPool(PooledObject instance) //�ݳ��ϱ�
    { 
        instance.gameObject.SetActive(false);
        pool.Add(instance);
    }

}
