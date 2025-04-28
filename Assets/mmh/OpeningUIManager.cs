using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningUIManager : MonoBehaviour
{
    public GameObject[] panels; // TitlePanel, SaveSlotsPanel, MenuPanel ����
    private int currentIndex = 0;

    public GameObject healthUI; // HealthUI ����

    void Start()
    {
        // ���� ������ �� HealthUI�� ���� ����
        if (healthUI != null)
            healthUI.SetActive(false);

        // �г� �ʱ�ȭ (TitlePanel�� �Ѱ� ������ ��)
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(i == 0); // 0�� �ε����� true (TitlePanel)
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentIndex++;
            if (currentIndex >= panels.Length)
                currentIndex = 0;
            UpdatePanels();
        }
    }

    void UpdatePanels()
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(i == currentIndex);
        }
    }

    public void OnNewGameButton()
    {
        Debug.Log("New Game Start!");
        StartGame();
    }

    public void OnContinueButton()
    {
        Debug.Log("Continue Game!");
        StartGame();
    }

    private void StartGame()
    {
        // ��� ������ �г� ����
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }

        // HealthUI �ѱ�
        if (healthUI != null)
            healthUI.SetActive(true);

        // ���⼭ ���߿� "�����÷��� �� �̵�"�� �߰� ����
        // SceneManager.LoadScene("�����÷��̾��̸�");
    }
}