using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // ������ ���� �� ������ ���縦 �����ϴ� ������Ʈ
    // �� ������ ������ �����ؾ� ��
    // ���� 0���� �Ǹ� �� ������Ʈ�� ��Ȱ��ȭ
    // �� ���� = ������ ������ ����

    [SerializeField] List<GameObject> enemySpanwers;
    [SerializeField] List<GameObject> Doors;
    private int enemyCount;
    // Start is called before the first frame update
    void Start()
    {
        enemyCount = enemySpanwers.Count;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEnemyKill()
    {
        enemyCount--;
        if(enemyCount == 0)
        {
            OpenDoors();
        }
    }

    public void OpenDoors()
    {
        foreach(var door in Doors)
        {
            door.SetActive(false);
        }
    }
}
