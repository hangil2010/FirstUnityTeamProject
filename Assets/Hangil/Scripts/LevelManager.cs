using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<GameObject> spawnerList;

    public List<GameObject> doorList;
    public List<GameObject> loadpointList; 
    private int listLength;

    private void Awake()
    {
        listLength = spawnerList.Count;
        if(doorList != null)
        {
            foreach (var door in doorList)
            {
                door.SetActive(true);
            }
        }
        if(loadpointList != null)
        {
            foreach (var loadpoint in loadpointList)
            {
                loadpoint.SetActive(false);
            }
        }
    }

    public void OnEnemyKilled()
    {
        Debug.Log("�� óġ");
        listLength--;
        if(listLength == 0 )
        {

            Debug.Log("��� �� óġ!");
            if (doorList != null)
            {
                foreach (var door in doorList)
                {
                    door.SetActive(false);
                }
            }
            if (loadpointList != null)
            {
                foreach (var loadpoint in loadpointList)
                {
                    loadpoint.SetActive(true);
                }
            }
        }
    }
}
