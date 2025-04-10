using UnityEngine;
using TMPro;

public class HungerSystem : MonoBehaviour
{
    [SerializeField] private TextMeshPro hungerText;
    [SerializeField] private EconomyController economy; 
    public int currentHunger = 100;
    private float hungerDecreaseInterval = 1.55f; 
    private float timer = 0f;

    private const string HungerKey = "Hunger";

    [SerializeField] private AudioSource soundDamage;
    [SerializeField] private GameObject panelReject;
    [SerializeField] private TextMeshProUGUI textRejection;

    void Start()
    {
        currentHunger = PlayerPrefs.GetInt(HungerKey, 100);
        UpdateHungerText();
    }

    void Update()
    {
        if (hungerText.gameObject.activeInHierarchy)
        {
            timer += Time.deltaTime;

            if (timer >= hungerDecreaseInterval)
            {
                if (currentHunger > 0)
                {
                    currentHunger = Mathf.Max(0, currentHunger - 1);
                    UpdateHungerText();
                }
                else
                {
                    int currentEnergy = economy.GetBanaceForce();
                    if (currentEnergy > 0)
                    {
                        soundDamage.Play();
                        economy.MinusBanaceForce(1);
                        panelReject.SetActive(true);
                        textRejection.text = "Питомец голоден \r\nТы теряешь энергию";

                        currentHunger = Mathf.Min(100, currentHunger + 1);
                        UpdateHungerText();
                    }
                }

                // Сбрасываем таймер
                timer = 0f;
            }
        }
    }

    public void UpdateHungerText()
    {
        hungerText.text = $"{currentHunger}/100";
    }

    void OnApplicationQuit()
    {
        SaveHunger();
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            SaveHunger();
        }
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SaveHunger();
        }
    }

    private void SaveHunger()
    {
        PlayerPrefs.SetInt(HungerKey, currentHunger);
        PlayerPrefs.Save();
    }
}
