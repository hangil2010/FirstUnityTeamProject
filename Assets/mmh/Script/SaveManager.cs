using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public Transform player;
    public int playerHealth = 3; // ����: HealthManager ���ᵵ ����
    public int itemCount = 0;    // ����: ���� ������ �� ��

    private string GetKey(string baseKey, int slot)
    {
        return $"{baseKey}_Slot{slot}";
    }

    public void SaveGame(int slot)
    {
        PlayerPrefs.SetFloat(GetKey("PlayerPosX", slot), player.position.x);
        PlayerPrefs.SetFloat(GetKey("PlayerPosY", slot), player.position.y);
        PlayerPrefs.SetFloat(GetKey("PlayerPosZ", slot), player.position.z);

        PlayerPrefs.SetInt(GetKey("Health", slot), playerHealth);
        PlayerPrefs.SetInt(GetKey("ItemCount", slot), itemCount);

        PlayerPrefs.Save();
        Debug.Log($"������ ���� {slot}�� ����Ǿ����ϴ�.");
    }

    public void LoadGame(int slot)
    {
        if (!PlayerPrefs.HasKey(GetKey("PlayerPosX", slot)))
        {
            Debug.LogWarning("�ش� ���Կ� ����� �����Ͱ� �����ϴ�.");
            return;
        }

        Vector3 pos = new Vector3(
            PlayerPrefs.GetFloat(GetKey("PlayerPosX", slot)),
            PlayerPrefs.GetFloat(GetKey("PlayerPosY", slot)),
            PlayerPrefs.GetFloat(GetKey("PlayerPosZ", slot))
        );

        player.position = pos;

        playerHealth = PlayerPrefs.GetInt(GetKey("Health", slot));
        itemCount = PlayerPrefs.GetInt(GetKey("ItemCount", slot));

        Debug.Log($"������ ���� {slot}���� �ҷ��������ϴ�.");
    }

    public void DeleteGame(int slot)
    {
        PlayerPrefs.DeleteKey(GetKey("PlayerPosX", slot));
        PlayerPrefs.DeleteKey(GetKey("PlayerPosY", slot));
        PlayerPrefs.DeleteKey(GetKey("PlayerPosZ", slot));
        PlayerPrefs.DeleteKey(GetKey("Health", slot));
        PlayerPrefs.DeleteKey(GetKey("ItemCount", slot));

        Debug.Log($"���� {slot}�� ���� �����͸� �����߽��ϴ�.");
    }
}