using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EquipmentUIManager : MonoBehaviour
{
    // ���� 2�� ����
    public Image[] itemSlots = new Image[2];

    // ���� ���Կ� ǥ�õ� ��������Ʈ ť (���Լ���)
    private Queue<Sprite> itemSpriteQueue = new Queue<Sprite>();

    // ������ �̹��� �߰�
    public void AddItemToUI(Sprite itemSprite)
    {
        // 2���� �̹� �������� ���� ������ �� ����
        if (itemSpriteQueue.Count >= 2)
        {
            itemSpriteQueue.Dequeue(); // ť���� ����
        }

        itemSpriteQueue.Enqueue(itemSprite); // �� �̹��� �߰�

        // UI�� �ݿ�
        UpdateUISlots();
    }

    // ���� �̹��� ���� ����
    private void UpdateUISlots()
    {
        Sprite[] sprites = itemSpriteQueue.ToArray();

        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (i < sprites.Length)
            {
                itemSlots[i].sprite = sprites[i];
                itemSlots[i].enabled = true;
            }
            else
            {
                itemSlots[i].sprite = null;
                itemSlots[i].enabled = false;
            }
        }
    }
}