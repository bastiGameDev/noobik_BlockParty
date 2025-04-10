using TMPro;
using UnityEngine;

public class CheatControll : MonoBehaviour
{
    public EconomyController economyController;
    [SerializeField] private GameObject cheatPanelAuth;
    [SerializeField] private GameObject cheatPanel;
    private bool yPressed = false;
    private bool uPressed = false;

    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private GameObject player;

    

    public void AuthClick()
    {
        if (inputField.text == "qwer12353")
        {
            cheatPanelAuth.SetActive(false);
            cheatPanel.SetActive(true);
        }
    }

    public void ChangeBalanceMoney(int delta)
    {
        if (delta < 0 && economyController.GetBanaceMoney() + delta < 0)
        {
            Debug.LogWarning("Нельзя уйти в отрицательное число.");
            return;
        }

        economyController.PlusBanaceMoney(delta);

    }

    public void ChangeBalanceForce(int delta)
    {
        if (delta < 0 && economyController.GetBanaceForce() + delta < 0)
        {
            Debug.LogWarning("Нельзя уйти в отрицательное число.");
            return;
        }

        economyController.PlusBanaceForce(delta);

    }

    public void ResetProgress()
    {
        PlayerPrefs.DeleteAll();
    }

    public void ResetBalanceMoney()
    {
        economyController.PlusBanaceMoney(-economyController.GetBanaceMoney());
    }

    public void ResetBalanceForce()
    {
        economyController.PlusBanaceForce(-economyController.GetBanaceForce());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            yPressed = true;
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            uPressed = true;
        }

        if (yPressed && uPressed)
        {
            player.GetComponent<movement>().enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            cheatPanelAuth.SetActive(true);
            yPressed = false;
            uPressed = false;
        }

        if (Input.GetKeyUp(KeyCode.Y))
        {
            yPressed = false;
        }

        if (Input.GetKeyUp(KeyCode.U))
        {
            uPressed = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClosePanels();
        }
    }

    void ClosePanels()
    {
        cheatPanelAuth.SetActive(false);
        cheatPanel.SetActive(false);

        player.GetComponent<movement>().enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
