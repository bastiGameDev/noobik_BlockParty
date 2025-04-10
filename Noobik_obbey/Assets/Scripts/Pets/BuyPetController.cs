using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuyPetController : MonoBehaviour
{
    [SerializeField] private int indexPet;

    [SerializeField] private string NamePet;
    [SerializeField] private int pricePet;
    [SerializeField] GameObject panelPetShop;
    [SerializeField] TextMeshProUGUI textNamePet;
    [SerializeField] TextMeshProUGUI textPrice;

    [SerializeField] private GameObject panelRejection;
    [SerializeField] private TextMeshProUGUI textRejection;

    [SerializeField] private GameObject alreadyBuyed;

    public EconomyController economy;
    public PetManager petManager;
    public AudioSource petSound;
    public AudioSource soundNo;

    private void Start()
    {       
    }

    private void OnTriggerStay(Collider other)
    {
        textNamePet.text = NamePet;
        textPrice.text = pricePet.ToString();

        if (!petManager.IsPetPurchased(indexPet))
        {
            panelPetShop.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (economy.GetBanaceMoney() >= pricePet)
                {
                    //Здесь нада какойнгибдь звук надристать покупки

                    economy.MinusBanaceMoney(pricePet);

                    petManager.PurchasePet(indexPet);
                    petManager.ActivatePet(indexPet);
                }
                else
                {
                    panelRejection.SetActive(true);
                    soundNo.Play();
                    textRejection.text = "Недостаточно денег";
                }
            }
        }
        else
        {
            alreadyBuyed.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Y))
            {
                petSound.Play();
                petManager.ActivatePet(indexPet);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        alreadyBuyed.SetActive(false);
        panelRejection.SetActive(false);
        panelPetShop.SetActive(false);
    }
}
