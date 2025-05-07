using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PassiveEquipmentUI : MonoBehaviour
{
    [Header("UI�� ������ �нú� ���� �̹����� (10��)")]
    public Image[] passiveSlots; // 10�� ������ Inspector�� �迭�� ����
    private Queue<Sprite> passiveQueue = new Queue<Sprite>();

    void Start()
    {
        // ���� �� ��� ���� �ʱ�ȭ
        foreach (var slot in passiveSlots)
        {
            slot.sprite = null;
            slot.enabled = false;
        }
    }

    public void AddPassiveItem(Sprite icon)
    {
        if (icon == null)
        {
            Debug.LogWarning("PassiveEquipmentUI: ���� �������� null�Դϴ�!");
            return;
        }

        if (passiveSlots == null || passiveSlots.Length == 0)
        {
            Debug.LogError("PassiveEquipmentUI: passiveSlots�� ������� �ʾҽ��ϴ�!");
            return;
        }

        passiveQueue.Enqueue(icon);

        // ���� ���� �ʰ� �� ���� ������ �� ����
        if (passiveQueue.Count > passiveSlots.Length)
        {
            passiveQueue.Dequeue();
        }

        UpdateSlots();
    }

    void UpdateSlots()
    {
        Sprite[] currentIcons = passiveQueue.ToArray();

        for (int i = 0; i < passiveSlots.Length; i++)
        {
            if (i < currentIcons.Length && currentIcons[i] != null)
            {
                passiveSlots[i].sprite = currentIcons[i];
                passiveSlots[i].enabled = true;
            }
            else
            {
                passiveSlots[i].sprite = null;
                passiveSlots[i].enabled = false;
            }
        }
    }

    public void ResetUI()
    {
        foreach (var slot in passiveSlots) 
        {
            slot.sprite = null;
            slot.enabled = false;
        }

        // ������ ����
        //currentIndex = 0;
    }
}