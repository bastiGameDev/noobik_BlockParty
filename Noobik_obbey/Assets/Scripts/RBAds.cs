using UnityEngine;
using System.Runtime.InteropServices;

public class RBAds : MonoBehaviour
{

    [DllImport("__Internal")]
    public static extern void ShowAdv();


    // ����� ��������� �������, �������� ���� ����� (������ �������� ���� ��������)
    public void ResumeGame()
    {
        // �������� �����
        Time.timeScale = 1f;
        AudioListener.volume = 1;
        AudioListener.pause = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // ����� ����������� �����-���� �������, �������� ���� ����� (������ �������� ���� ��������)
    public void MuteGame()
    {
        // ��������� �����
        AudioListener.volume = 0;
        AudioListener.pause = true;
        Time.timeScale = 0f;
    }

    // ���������
    public void OnOpen()
    {
        Debug.Log("��������� ������� �������");

        MuteGame();
    }
    public void OnClose(string id)
    {
        if (id == "notClosed")
        {
            Debug.Log("������ ��������� ������� ��� �� �������");
        }
        else if (id == "Closed")
        {
            Debug.Log("��������� ������� �������, �������� ������� �����");

            ResumeGame();
        }
    }
    public void OnError()
    {
        Debug.Log("��������� ������� ������ ������");

        ResumeGame();
    }
    public void OnOffline()
    {
        Debug.Log("��������� ������� ��������� �������������� ����");

        ResumeGame();
    }

    // ������
    public void OnOpenReward()
    {
        Debug.Log("������ ������� �������");

        MuteGame();
    }
    public void OnRewarded()
    {
        Debug.Log("���� �������������� �� �������� ������ �������");
    }
    public void OnCloseReward()
    {
        Debug.Log("������ ������� �������");

        ResumeGame();
    }
    public void OnErrorReward()
    {
        Debug.Log("������ ������� ������ ������");
        ResumeGame();
    }
}