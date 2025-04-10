using System.Collections;
using TMPro;
using UnityEngine;
using YG;
using UnityEngine.UI;

public class TimerPanelController : MonoBehaviour
{
    public GameObject timerPanel; // ������ �� UI ������ � ��������
    public TextMeshProUGUI timerText; // ������ �� ��������� ��������� ��� ����������� �������
    public float initialDelay = 60f; // ��������� �������� � �������� (1 ������)
    public int timerDuration = 5; // ������������ ������� � ��������

    private float timer;
    private bool isTimerRunning;

    void Start()
    {
        // �������� ������ � �������� ��� �������
        timerPanel.SetActive(false);

        // ��������� �������� ��� ��������� ��������
        StartCoroutine(InitialDelay());
    }

    IEnumerator InitialDelay()
    {
        // ���� 1 ������
        yield return new WaitForSeconds(initialDelay);

        // ���������� ������ � ��������
        timerPanel.SetActive(true);

        // ��������� ������
        StartTimer();
    }

    void StartTimer()
    {
        timer = timerDuration;
        isTimerRunning = true;
        UpdateTimerText();
    }

    void Update()
    {
        if (isTimerRunning)
        {
            timer -= Time.deltaTime;
            UpdateTimerText();

            if (timer <= 0)
            {
                isTimerRunning = false;
                OnTimerEnd();
            }
        }
    }

    void UpdateTimerText()
    {
        timerText.text = Mathf.Ceil(timer).ToString();
    }

    void OnTimerEnd()
    {
        Debug.Log("������ ����������. ��������� ��������.");

        timerPanel.SetActive(false);

        YandexGame.FullscreenShow();

        StartCoroutine(InitialDelay());
    }
}
