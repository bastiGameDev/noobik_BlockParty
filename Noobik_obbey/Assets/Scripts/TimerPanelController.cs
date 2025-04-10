using System.Collections;
using TMPro;
using UnityEngine;
using YG;
using UnityEngine.UI;

public class TimerPanelController : MonoBehaviour
{
    public GameObject timerPanel; // Ссылка на UI панель с таймером
    public TextMeshProUGUI timerText; // Ссылка на текстовый компонент для отображения таймера
    public float initialDelay = 60f; // Начальная задержка в секундах (1 минута)
    public int timerDuration = 5; // Длительность таймера в секундах

    private float timer;
    private bool isTimerRunning;

    void Start()
    {
        // Скрываем панель с таймером при запуске
        timerPanel.SetActive(false);

        // Запускаем корутину для начальной задержки
        StartCoroutine(InitialDelay());
    }

    IEnumerator InitialDelay()
    {
        // Ждем 1 минуту
        yield return new WaitForSeconds(initialDelay);

        // Показываем панель с таймером
        timerPanel.SetActive(true);

        // Запускаем таймер
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
        Debug.Log("Таймер закончился. Выполняем действие.");

        timerPanel.SetActive(false);

        YandexGame.FullscreenShow();

        StartCoroutine(InitialDelay());
    }
}
