using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PassiveEquipmentUI : MonoBehaviour
{
    public Image[] passiveSlots;
    private Queue<Sprite> passiveQueue = new Queue<Sprite>();

    void Start()
    {
        foreach (var slot in passiveSlots)
        {
            if (slot != null)
            {
                slot.sprite = null;
                slot.enabled = false;  // �Ͼ� �ڽ� �� ���̵��� ��Ȱ��ȭ
            }
        }
    }

    public void AddPassiveItem(Sprite icon)
    {
        if (icon == null) return;
        if (passiveSlots == null || passiveSlots.Length == 0) return;

        passiveQueue.Enqueue(icon);
        if (passiveQueue.Count > passiveSlots.Length)
        {
            passiveQueue.Dequeue();
        }

        int index = 0;
        foreach (var item in passiveQueue)
        {
            passiveSlots[index].sprite = item;
            passiveSlots[index].enabled = true;
            index++;
        }

        // ���� ���� �ʱ�ȭ (�Ͼ� �ڽ� ����)
        for (int i = index; i < passiveSlots.Length; i++)
        {
            passiveSlots[i].sprite = null;
            passiveSlots[i].enabled = false;
        }
    }

    public void ResetUI()
    {
        foreach (var slot in passiveSlots)
        {
            slot.sprite = null;
            slot.enabled = false;
        }
        passiveQueue.Clear();
    }
}