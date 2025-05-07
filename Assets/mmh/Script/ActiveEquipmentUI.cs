using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ActiveEquipmentUI : MonoBehaviour
{
    public Image[] activeSlots;  // �ν����Ϳ��� ���� �Ҵ��� ���Ե�
    private Queue<Sprite> activeQueue = new Queue<Sprite>();

    void Start()
    {
        foreach (var slot in activeSlots)
        {
            if (slot != null)
            {
                slot.sprite = null;
                slot.enabled = false;  // ó���� �ƹ��͵� �� ���̰�
            }
        }
    }

    public void AddActiveItem(Sprite icon)
    {
        if (icon == null) return;

        if (activeSlots == null || activeSlots.Length == 0) return;

        activeQueue.Enqueue(icon);
        if (activeQueue.Count > activeSlots.Length)
        {
            activeQueue.Dequeue();
        }

        int index = 0;
        foreach (var item in activeQueue)
        {
            activeSlots[index].sprite = item;
            activeSlots[index].enabled = true;  // �߿�: ������ Ȱ��ȭ�ؾ� ������!
            index++;
        }

        // ���� ���� ����
        for (int i = index; i < activeSlots.Length; i++)
        {
            activeSlots[i].sprite = null;
            activeSlots[i].enabled = false;
        }
    }

    public void ResetUI()
    {
        foreach (var slot in activeSlots)
        {
            slot.sprite = null;
            slot.enabled = false;
        }

        // ������ ����
        //currentIndex = 0;
    }
}