using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item; // ������ ������

    // Inspector â���� ������ �Ҵ��� �����ϰ� �ϱ� ���� ��� (���� ����)
    public string itemNameForEditor;

    private void OnValidate()
    {
        // �����Ϳ��� itemNameForEditor ������� ������ ���� (������ ����)
        if (!string.IsNullOrEmpty(itemNameForEditor))
        {
            switch (itemNameForEditor)
            {
                case "SadOnion":
                    item = new SadOnion();
                    break;
                case "TheInnerEye":
                    item = new TheInnerEye();
                    break;
                case "Pentagram":
                    item = new Pentagram();
                    break;
                // �ٸ� ������ ���̽� �߰�
                default:
                    item = null;
                    break;
            }
            itemNameForEditor = ""; // �� �ʱ�ȭ
        }
    }

}
