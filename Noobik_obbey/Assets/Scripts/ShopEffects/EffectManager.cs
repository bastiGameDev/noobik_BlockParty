using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public GameObject[] effects; // ������ �������� ������������
    private int activeEffectIndex = -1; // ������ ��������� �����������
    private bool[] purchasedEffects; // ������ ��� ������������ ��������� ������������

    private const string EFFECTS_PURCHASED_KEY = "EffectsPurchased";
    private const string ACTIVE_EFFECT_INDEX_KEY = "ActiveEffectIndex";

    public AudioSource buyEffectSoundl;

    void Start()
    {
        // ������������� ������� ��������� ������������
        purchasedEffects = new bool[effects.Length];

        // �������� ������ � ��������� ������������
        LoadPurchasedEffects();

        // �������� ������ �� �������� �����������
        LoadActiveEffect();

        // ��������� �����������, ���� �� ��� ��������
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
            // ��������, �� ������ �� ���������� ���
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
            // ��������, ������ �� ����������
            if (purchasedEffects[index])
            {
                // ������������ �������� ��������� �����������
                if (activeEffectIndex >= 0 && activeEffectIndex < effects.Length)
                {
                    effects[activeEffectIndex].SetActive(false);
                }

                // ���������� ����� ����������
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
            activeEffectIndex = -1; // ���������� ������ ��������� �����������
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
            // ������������ �������� ��������� �����������, ���� �� ����
            if (activeEffectIndex >= 0 && activeEffectIndex < effects.Length)
            {
                effects[activeEffectIndex].SetActive(false);
            }

            // ���������� ����� ����������
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
