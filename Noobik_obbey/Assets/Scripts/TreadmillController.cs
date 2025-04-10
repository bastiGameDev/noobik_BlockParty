using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class TreadmillController : MonoBehaviour
{
    
    public CharacterController characterController;
    public AudioSource forcePlusSound;

    [SerializeField] private GameObject player;
    [SerializeField] private Animator animator;

    [SerializeField] private AudioSource soundRun;
    [SerializeField] private AudioSource soundTredmill;

    [SerializeField] private EconomyController economy;

    [SerializeField] private RectTransform animUnit;
    [SerializeField] private TextMeshProUGUI countForceForAnimationText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 initialPosition = new Vector3(13.9700003f, 2.05699992f, -58.5299988f);
            player.transform.position = initialPosition;
            player.transform.rotation = Quaternion.identity;

            soundRun.Stop();

            player.GetComponent<movement>().enabled = false;
            characterController.enabled = false;

            animator.SetBool("isRunningOnTreadmill", true);

            StartCoroutine(RunTreadmill());
        }

    }

    private IEnumerator RunTreadmill()
    {
        soundTredmill.Play();
        yield return new WaitForSeconds(3f);
        soundTredmill.Stop();

        StartCoroutine(AnimationUnit());

        animator.SetBool("isRunningOnTreadmill", false);

        Vector3 finalPosition = new Vector3(4.96000004f, 3.05699992f, -84.9800034f);
        player.transform.position = finalPosition;

        player.GetComponent<movement>().enabled = true;
        characterController.enabled = true;        
    }

    private IEnumerator AnimationUnit()
    {
        countForceForAnimationText.text = "+2";

        animUnit.gameObject.SetActive(true);

        animUnit.DOAnchorPos(new Vector2(-171f, 486.25f), 2.0f).SetEase(Ease.InQuint);

        yield return new WaitForSeconds(2.1f);

        forcePlusSound.Play();
        economy.PlusBanaceForce(2);

        animUnit.gameObject.SetActive(false);

        animUnit.DOAnchorPos(new Vector2(-87f, -340f), 0.2f);   
    }
}
