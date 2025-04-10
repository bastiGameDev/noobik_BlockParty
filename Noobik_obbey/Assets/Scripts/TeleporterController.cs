using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private AudioSource soundTeleport;

    public CharacterController characterController;

    private void OnTriggerEnter(Collider other)
    {

        soundTeleport.Play();

        player.GetComponent<movement>().enabled = false;
        characterController.enabled = false;

        Vector3 finalPosition = new Vector3(4.96000004f, 3.05699992f, -84.9800034f);

        player.transform.position = finalPosition;

        player.GetComponent<movement>().enabled = true;
        characterController.enabled = true;


    }
}
