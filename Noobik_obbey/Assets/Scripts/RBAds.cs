using UnityEngine;
using System.Runtime.InteropServices;

public class RBAds : MonoBehaviour
{

    [DllImport("__Internal")]
    public static extern void ShowAdv();


    // Когда закрываем рекламу, вызываем этот метод (можете добавить свои действия)
    public void ResumeGame()
    {
        // Включаем звуки
        Time.timeScale = 1f;
        AudioListener.volume = 1;
        AudioListener.pause = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Когда открывается какая-либо реклама, вызываем этот метод (можете добавить свои действия)
    public void MuteGame()
    {
        // Выключаем звуки
        AudioListener.volume = 0;
        AudioListener.pause = true;
        Time.timeScale = 0f;
    }

    // Фуллскрин
    public void OnOpen()
    {
        Debug.Log("Фуллскрин реклама открыта");

        MuteGame();
    }
    public void OnClose(string id)
    {
        if (id == "notClosed")
        {
            Debug.Log("Другая фуллскрин реклама еще не закрыта");
        }
        else if (id == "Closed")
        {
            Debug.Log("Фуллскрин реклама закрыта, включаем обратно звуки");

            ResumeGame();
        }
    }
    public void OnError()
    {
        Debug.Log("Фуллскрин реклама выдала ошибку");

        ResumeGame();
    }
    public void OnOffline()
    {
        Debug.Log("Фуллскрин реклама сломалась нестабильности сети");

        ResumeGame();
    }

    // Ревард
    public void OnOpenReward()
    {
        Debug.Log("Ревард реклама открыта");

        MuteGame();
    }
    public void OnRewarded()
    {
        Debug.Log("Даем вознаграждение за просмотр ревард рекламы");
    }
    public void OnCloseReward()
    {
        Debug.Log("Ревард реклама закрыта");

        ResumeGame();
    }
    public void OnErrorReward()
    {
        Debug.Log("Ревард реклама выдала ошибку");
        ResumeGame();
    }
}