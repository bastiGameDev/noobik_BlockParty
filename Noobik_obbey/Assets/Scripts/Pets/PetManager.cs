using UnityEngine;

public class PetManager : MonoBehaviour
{
    public GameObject[] pets; // Массив объектов питомцев
    private int activePetIndex = -1; // Индекс активного питомца
    private bool[] purchasedPets; // Массив для отслеживания купленных питомцев

    private const string PETS_PURCHASED_KEY = "PetsPurchased";
    private const string ACTIVE_PET_INDEX_KEY = "ActivePetIndex";

    [SerializeField] private GameObject hunger;
    [SerializeField] private AudioSource buyPet;

    void Start()
    {
        // Инициализация массива купленных питомцев
        purchasedPets = new bool[pets.Length];

        // Загрузка данных о купленных питомцах
        LoadPurchasedPets();

        // Загрузка данных об активном питомце
        LoadActivePet();

        // Активация питомца, если он был сохранен
        if (activePetIndex >= 0 && activePetIndex < pets.Length)
        {
            ActivatePet(activePetIndex);
        }

        if (IsAnyPetActive())
        {
            hunger.gameObject.SetActive(true);
        }
    }

    public bool IsPetPurchased(int index)
    {
        if (index >= 0 && index < pets.Length)
        {
            return purchasedPets[index];
        }
        else
        {
            Debug.LogError($"Invalid pet index: {index}");
            return false;
        }
    }


    void LoadPurchasedPets()
    {
        string purchasedPetsData = PlayerPrefs.GetString(PETS_PURCHASED_KEY, "");
        if (!string.IsNullOrEmpty(purchasedPetsData))
        {
            string[] purchasedPetsArray = purchasedPetsData.Split(',');
            for (int i = 0; i < purchasedPetsArray.Length; i++)
            {
                purchasedPets[i] = bool.Parse(purchasedPetsArray[i]);
            }
        }
    }

    void LoadActivePet()
    {
        activePetIndex = PlayerPrefs.GetInt(ACTIVE_PET_INDEX_KEY, -1);


    }

    public void PurchasePet(int index)
    {
        if (index >= 0 && index < pets.Length)
        {
            // Проверка, не куплен ли питомец уже
            if (!purchasedPets[index])
            {
                buyPet.Play();
                purchasedPets[index] = true;
                SavePurchasedPets();
                Debug.Log($"Pet {index} purchased successfully.");
            }
            else
            {
                Debug.Log($"Pet {index} is already purchased.");
            }
        }
        else
        {
            Debug.LogError($"Invalid pet index: {index}");
        }
    }


    public void ActivatePet(int index)
    {
        if (index >= 0 && index < pets.Length)
        {
            // Проверка, куплен ли питомец
            if (purchasedPets[index])
            {
                // Деактивируем текущего активного питомца
                if (activePetIndex >= 0 && activePetIndex < pets.Length)
                {
                    pets[activePetIndex].SetActive(false);
                }

                // Активируем нового питомца                
                pets[index].SetActive(true);
                activePetIndex = index;
                SaveActivePet();
                hunger.gameObject.SetActive(true);
                Debug.Log($"Pet {index} activated successfully.");
            }
            else
            {
                Debug.LogError($"Pet {index} is not purchased yet.");
            }
        }
        else
        {
            Debug.LogError($"Invalid pet index: {index}");
        }
    }


    void SavePurchasedPets()
    {
        string purchasedPetsData = string.Join(",", purchasedPets);
        PlayerPrefs.SetString(PETS_PURCHASED_KEY, purchasedPetsData);
    }

    void SaveActivePet()
    {
        PlayerPrefs.SetInt(ACTIVE_PET_INDEX_KEY, activePetIndex);
    }

    public void HidePet()
    {
        if (activePetIndex >= 0 && activePetIndex < pets.Length)
        {
            pets[activePetIndex].SetActive(false);
            activePetIndex = -1; // Сбрасываем индекс активного питомца
            SaveActivePet();
            Debug.Log("Active pet hidden.");
        }
        else
        {
            Debug.Log("No active pet to hide.");
        }
    }

    public void ShowPet(int index)
    {
        if (index >= 0 && index < pets.Length && purchasedPets[index])
        {
            // Деактивируем текущего активного питомца, если он есть
            if (activePetIndex >= 0 && activePetIndex < pets.Length)
            {
                pets[activePetIndex].SetActive(false);
            }

            // Активируем нового питомца
            pets[index].SetActive(true);
            activePetIndex = index;
            SaveActivePet();
            Debug.Log($"Pet {index} shown.");
        }
        else
        {
            Debug.LogError($"Invalid pet index: {index} or pet is not purchased.");
        }
    }

    public bool IsAnyPetActive()
    {
        return activePetIndex >= 0 && activePetIndex < pets.Length;
    }


}
