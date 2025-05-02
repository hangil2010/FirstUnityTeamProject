using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerDieManager : MonoBehaviour
{
    public GameObject deathPanel;       // You Died �г�
    public Button restartButton;        // ����� ��ư
    public Button quitButton;           // ���� ��ư
    public string sceneName = "GameScene"; // ���� �� �̸� (�ʿ�� ����)

    private void Start()
    {
        // ���� �� DeathPanel ����
        deathPanel.SetActive(false);

        // ��ư�� ��� ����
        restartButton.onClick.AddListener(RestartGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    public void ShowDeathScreen()
    {
        // ��� �� UI ����
        deathPanel.SetActive(true);
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(sceneName); // ���� �� �ٽ� �ε�
    }

    private void QuitGame()
    {
        Application.Quit(); // ���� (�����Ϳ��� �ȵ�)
    }
}
