using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hazard : MonoBehaviour
{
    public Transform teleportPos;
    [Header("������ ����� �� �ߵ��� ȿ���� ����")]
    public UnityEvent OnHazardFall;
    // TODO : ���� ��� ����Ʈ �߰��ϱ�?
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (teleportPos != null)
            {
                OnHazardFall?.Invoke();
                other.transform.position = new Vector3(teleportPos.position.x, teleportPos.position.y + 1, teleportPos.position.z);
            }
        }
    }

    public void Fall()
    {
        Debug.Log("������ �������ϴ�.");
    }
}
