using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("�÷��̾� ����");
        Instantiate(player, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
        DontDestroyOnLoad(player);
    }
}
