using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;      // AudioMixer �� �Ÿ�
using TMPro;                  // TextMeshPro �� �Ÿ�

public class SettingsManager : MonoBehaviour
{
    [Header("UI References")]
    public Slider volumeSlider;       // 0~4 ����
    public TMP_Text volumeText;       // "0%", "25%" ... ǥ��

    [Header("Audio")]
    public AudioMixer audioMixer;     // AudioMixer ��� ��
    // (���� AudioListener.volume�� ���� �Ÿ� �̰� ����� AudioListener.volume=normalized; ���)

    private const float STEPS = 4f;   // 0~4 ���� �� 4�� ������ 0~1.0

    void Start()
    {
        // �����̴� ����
        volumeSlider.wholeNumbers = true;
        volumeSlider.minValue = 0;
        volumeSlider.maxValue = STEPS;

        // �� �ҷ����� (������ 4ĭ = 100%)
        float saved = PlayerPrefs.GetFloat("VolumeStep", STEPS);
        volumeSlider.value = saved;

        // �̺�Ʈ ���
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);

        // �ʱ� �ݿ�
        OnVolumeChanged(saved);
    }

    public void OnVolumeChanged(float step)
    {
        float normalized = step / STEPS;       // 0.0 0.25 0.5 0.75 1.0

        // 1) ȭ�鿡 ǥ��
        volumeText.text = $"{normalized * 100:0}%";

        // 2) ���� ���� �ݿ�
        if (audioMixer != null)
        {
            // AudioMixer�� ExposedParam "MasterVolume"�� �ִٰ� ����
            //  ����(���ú�) ������ ��ȯ: 20*log10(��������)
            float dB = Mathf.Log10(Mathf.Max(normalized, 0.0001f)) * 20f;
            audioMixer.SetFloat("MasterVolume", dB);
        }
        else
        {
            AudioListener.volume = normalized;
        }

        // 3) ���� ������ ���� ����
        PlayerPrefs.SetFloat("VolumeStep", step);
    }
}