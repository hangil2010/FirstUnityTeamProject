using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestHangil : MonoBehaviour
{
    // 1. �÷��̾ ���迡 ��´�.
    // 2. ���谡 ��Ȱ��ȭ�ǰ� ���ڵ� ��Ȱ��ȭ�ȴ�
    // 3. ���ڴ� ��Ȱ��ȭ�ǰ�, ������ ��ġ�� �������� �����Ѵ�.
    [SerializeField] GameObject item;
    // Start is called before the first frame update
    public void Open()
    {
        gameObject.SetActive(false);
        GameObject tresure = Instantiate(item, transform.position, Quaternion.identity);
    }
}
