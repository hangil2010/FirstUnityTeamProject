using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMove : MonoBehaviour
{
    public float moveDistance = 1080f;  // �� �� �̵��� �Ÿ�
    public float moveSpeed = 500f;         // �̵� �ӵ�

    private Vector3 targetPosition;     // ��ǥ ��ġ
    private bool isMoving = false;       // �̵� �� ����

    void Start()
    {
        targetPosition = transform.position;  // ���� ��ġ ����
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) && !isMoving)
        {
            targetPosition = transform.position + new Vector3(0, -moveDistance, 0);
            isMoving = true;
        }

        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                transform.position = targetPosition;
                isMoving = false;
            }
        }
    }
}