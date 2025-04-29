using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public List<GameObject> spawnerList;

    public List<GameObject> doorList;
    public List<GameObject> loadpointList; 
    private int listLength;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        listLength = spawnerList.Count;
        foreach(var door in doorList) 
        {
            door.SetActive(true);
        }
        foreach(var loadpoint in loadpointList)
        {
            loadpoint.SetActive(false);
        }
    }

    public void OnEnemyKilled()
    {
        Debug.Log("�� óġ");
        listLength--;
        if(listLength == 0 )
        {
            Debug.Log("��� �� óġ!");
            foreach(var door in doorList)
            {
                door.SetActive(false);
            }
            foreach (var loadpoint in loadpointList)
            {
                loadpoint.SetActive(true);
            }
        }
    }
}
