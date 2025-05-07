using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;           // �̱��� �ν��Ͻ�
    public static int currentMap;                // ���� �� ��ȣ (1���� ����)

    [SerializeField] Camera mainCamera;          // �̵���ų ī�޶�
    public List<GameObject> Maps;                // �� ������ ����Ʈ
    public int startMapPos;                      // ���� �� ��ȣ

    [SerializeField] List<Transform> cameraPos;  // �ʺ� ī�޶� ��ġ ����Ʈ

    private Coroutine cameraMoveCoroutine;       // ī�޶� �̵� �ڷ�ƾ ������

    void Awake()
    {
        // �̱��� ����
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // mainCamera �ڵ� �Ҵ�
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        // �� �ʱ�ȭ
        currentMap = startMapPos;

        // �ʱ⿣ ��� ���� �ѵε�, Start()���� ī�޶� �ش� ������ �̵�
        foreach (var map in Maps)
        {
            if (!map.activeSelf)
                map.SetActive(true);
        }
    }

    void Start()
    {
        MoveCamera(); // ���� �� ī�޶� �̵�
    }

    // �ε巴�� ī�޶� �̵���Ű�� �ڷ�ƾ
    private IEnumerator MoveCameraSmooth(Vector3 targetPosition, float duration = 1f)
    {
        Vector3 startPos = mainCamera.transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            mainCamera.transform.position = Vector3.Lerp(startPos, targetPosition, t);
            yield return null;
        }

        mainCamera.transform.position = targetPosition; // ��Ȯ�� ��ġ ����
    }

    // ī�޶� ���� �� ��ġ�� �ε巴�� �̵�
    public void MoveCamera()
    {
        if (mainCamera != null && currentMap - 1 >= 0 && currentMap - 1 < cameraPos.Count)
        {
            Vector3 targetPos = cameraPos[currentMap - 1].position;

            // ���� �̵� �ߴ�
            if (cameraMoveCoroutine != null)
            {
                StopCoroutine(cameraMoveCoroutine);
            }

            // �� �̵� ����
            cameraMoveCoroutine = StartCoroutine(MoveCameraSmooth(targetPos, 1f)); // duration ���� ����
        }
        else
        {
            Debug.LogWarning("ī�޶� �̵� ����: mainCamera �Ǵ� cameraPos�� �ùٸ��� �ʽ��ϴ�.");
        }
    }

    // �� ��ȯ + ī�޶� �̵� ó�� (���� ���� ���� ��Ȱ��ȭ���� ����!)
    public void ChangeMap(int newMapIndex)
    {
        if (newMapIndex < 1 || newMapIndex > Maps.Count)
        {
            Debug.LogWarning($"��ȿ���� ���� �� ��ȣ: {newMapIndex}");
            return;
        }

        currentMap = newMapIndex;

        // �ش� ���� �����ִٸ� Ȱ��ȭ�� ���ش�
        if (!Maps[currentMap - 1].activeSelf)
        {
            Maps[currentMap - 1].SetActive(true);
        }

        // ī�޶� �̵�
        MoveCamera();
    }
}