using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hazard : MonoBehaviour
{
    public Transform teleportPos;
    public float teleportYPosPadding = 5.5f;
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
                other.transform.position = new Vector3(teleportPos.position.x, teleportPos.position.y + teleportYPosPadding, teleportPos.position.z);
            }
        }
    }

    public void Fall()
    {
        Debug.Log("������ �������ϴ�.");
    }
}
