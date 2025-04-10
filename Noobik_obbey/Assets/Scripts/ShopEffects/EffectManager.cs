using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public GameObject[] effects; // Массив объектов спецэффектов
    private int activeEffectIndex = -1; // Индекс активного спецэффекта
    private bool[] purchasedEffects; // Массив для отслеживания купленных спецэффектов

    private const string EFFECTS_PURCHASED_KEY = "EffectsPurchased";
    private const string ACTIVE_EFFECT_INDEX_KEY = "ActiveEffectIndex";

    public AudioSource buyEffectSoundl;

    void Start()
    {
        // Инициализация массива купленных спецэффектов
        purchasedEffects = new bool[effects.Length];

        // Загрузка данных о купленных спецэффектах
        LoadPurchasedEffects();

        // Загрузка данных об активном спецэффекте
        LoadActiveEffect();

        // Активация спецэффекта, если он был сохранен
        if (activeEffectIndex >= 0 && activeEffectIndex < effects.Length)
        {
            ActivateEffect(activeEffectIndex);
        }
    }

    public bool IsEffectPurchased(int index)
    {
        if (index >= 0 && index < effects.Length)
        {
            return purchasedEffects[index];
        }
        else
        {
            Debug.LogError($"Invalid effect index: {index}");
            return false;
        }
    }

    void LoadPurchasedEffects()
    {
        string purchasedEffectsData = PlayerPrefs.GetString(EFFECTS_PURCHASED_KEY, "");
        if (!string.IsNullOrEmpty(purchasedEffectsData))
        {
            string[] purchasedEffectsArray = purchasedEffectsData.Split(',');
            for (int i = 0; i < purchasedEffectsArray.Length; i++)
            {
                purchasedEffects[i] = bool.Parse(purchasedEffectsArray[i]);
            }
        }
    }

    void LoadActiveEffect()
    {
        activeEffectIndex = PlayerPrefs.GetInt(ACTIVE_EFFECT_INDEX_KEY, -1);
    }

    public void PurchaseEffect(int index)
    {
        if (index >= 0 && index < effects.Length)
        {
            // Проверка, не куплен ли спецэффект уже
            if (!purchasedEffects[index])
            {
                buyEffectSoundl.Play();
                purchasedEffects[index] = true;
                SavePurchasedEffects();
                Debug.Log($"Effect {index} purchased successfully.");
            }
            else
            {
                Debug.Log($"Effect {index} is already purchased.");
            }
        }
        else
        {
            Debug.LogError($"Invalid effect index: {index}");
        }
    }

    public void ActivateEffect(int index)
    {
        if (index >= 0 && index < effects.Length)
        {
            // Проверка, куплен ли спецэффект
            if (purchasedEffects[index])
            {
                // Деактивируем текущего активного спецэффекта
                if (activeEffectIndex >= 0 && activeEffectIndex < effects.Length)
                {
                    effects[activeEffectIndex].SetActive(false);
                }

                // Активируем новый спецэффект
                effects[index].SetActive(true);
                activeEffectIndex = index;
                SaveActiveEffect();
                Debug.Log($"Effect {index} activated successfully.");
            }
            else
            {
                Debug.LogError($"Effect {index} is not purchased yet.");
            }
        }
        else
        {
            Debug.LogError($"Invalid effect index: {index}");
        }
    }

    void SavePurchasedEffects()
    {
        string purchasedEffectsData = string.Join(",", purchasedEffects);
        PlayerPrefs.SetString(EFFECTS_PURCHASED_KEY, purchasedEffectsData);
    }

    void SaveActiveEffect()
    {
        PlayerPrefs.SetInt(ACTIVE_EFFECT_INDEX_KEY, activeEffectIndex);
    }

    public void HideEffect()
    {
        if (activeEffectIndex >= 0 && activeEffectIndex < effects.Length)
        {
            effects[activeEffectIndex].SetActive(false);
            activeEffectIndex = -1; // Сбрасываем индекс активного спецэффекта
            SaveActiveEffect();
            Debug.Log("Active effect hidden.");
        }
        else
        {
            Debug.Log("No active effect to hide.");
        }
    }

    public void ShowEffect(int index)
    {
        if (index >= 0 && index < effects.Length && purchasedEffects[index])
        {
            // Деактивируем текущего активного спецэффекта, если он есть
            if (activeEffectIndex >= 0 && activeEffectIndex < effects.Length)
            {
                effects[activeEffectIndex].SetActive(false);
            }

            // Активируем новый спецэффект
            effects[index].SetActive(true);
            activeEffectIndex = index;
            SaveActiveEffect();
            Debug.Log($"Effect {index} shown.");
        }
        else
        {
            Debug.LogError($"Invalid effect index: {index} or effect is not purchased.");
        }
    }

    public bool IsAnyEffectActive()
    {
        return activeEffectIndex >= 0 && activeEffectIndex < effects.Length && effects[activeEffectIndex].activeSelf;
    }
}
