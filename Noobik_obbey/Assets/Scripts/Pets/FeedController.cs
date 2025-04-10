using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FeedController : MonoBehaviour
{
    [SerializeField] private EconomyController economy;
    [SerializeField] private HungerSystem hunger;

    [SerializeField] private TextMeshProUGUI textInfo;
    [SerializeField] private GameObject panelReject;

    [SerializeField] private AudioSource feedSound;
    [SerializeField] private AudioSource soundNo;


    private void OnTriggerEnter(Collider other)
    {
        textInfo.text = "Нажми Е чтобы покормить питомца";

    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (hunger.currentHunger <= 90)
            {
                feedSound.Play();
                economy.MinusBanaceMoney(20);
                hunger.currentHunger = Mathf.Min(100, hunger.currentHunger + 20);
                hunger.UpdateHungerText(); 
            }
            else
            {
                textInfo.text = "Питомец уже сыт.";
                soundNo.Play();
            }
        }
        HidePanelReject();
    }

    private void OnTriggerExit(Collider other)
    {
        textInfo.text = "";
        HidePanelReject();
    }

    private void HidePanelReject()
    {
        if (hunger.currentHunger >= 2)
        {
            panelReject.SetActive(false);
        }
    }
}
