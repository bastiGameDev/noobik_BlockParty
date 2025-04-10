using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClimbingController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource soundRun;
    public float climbHeight = 1.0f; // Высота подъема за одно нажатие
    public float baseClimbSpeed = 5f; // Базовая скорость подъема (юниты в секунду)
    public float speedIncreasePerUnit = 0.5f; // Увеличение скорости на каждую единицу высоты

    public CharacterController characterController;
    public AudioSource moneyPlusSound;

    [SerializeField] private EconomyController economy;

    [SerializeField] private RectTransform animUnit;
    [SerializeField] private TextMeshProUGUI countForceForAnimationText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 initialPosition = new Vector3(3.96000004f, 2.05699992f, -44.7260017f);
            player.transform.position = initialPosition;
            player.transform.rotation = Quaternion.identity;

            soundRun.Stop();

            player.GetComponent<movement>().enabled = false;
            characterController.enabled = false;

            animator.SetBool("isClimbing", true);

            StartCoroutine(Climb());
        }
    }

    private IEnumerator Climb()
    {
        Vector3 startPosition = player.transform.position;
        float force = economy.GetBanaceForce();
        float maxForce = 386f;

        // Ограничиваем силу до максимальной возможной силы
        if (force > maxForce)
        {
            force = maxForce;
        }

        Vector3 targetPosition = startPosition + Vector3.up * force;

        float distance = Vector3.Distance(startPosition, targetPosition);
        float climbSpeed = baseClimbSpeed + distance * speedIncreasePerUnit;

        while (player.transform.position.y < targetPosition.y)
        {
            player.transform.position += Vector3.up * climbSpeed * Time.deltaTime;
            yield return null;
        }

        player.transform.position = targetPosition;

        animator.SetBool("isClimbing", false);

        if (economy.GetBanaceForce() >= maxForce)
        {
            Vector3 finalPosition = new Vector3(5.09000015f, 399.209991f, -35.0200005f);
            player.transform.position = finalPosition;
        }
        else
        {
            Vector3 finalPosition = new Vector3(4.96000004f, 3.05699992f, -84.9800034f);
            player.transform.position = finalPosition;
        }

        StartCoroutine(AnimationUnit());

        player.GetComponent<movement>().enabled = true;
        characterController.enabled = true;
    }

    private IEnumerator AnimationUnit()
    {
        countForceForAnimationText.text = ("+" + (economy.GetBanaceForce() * 2).ToString());

        animUnit.gameObject.SetActive(true);

        animUnit.DOAnchorPos(new Vector2(-841f, 489f), 2.0f).SetEase(Ease.InQuint);
        //Vector3(-841,489,0)
        yield return new WaitForSeconds(2.1f);

        economy.PlusBanaceMoney(economy.GetBanaceForce() * 2);
        moneyPlusSound.Play();

        animUnit.gameObject.SetActive(false);

        animUnit.DOAnchorPos(new Vector2(-87f, -340f), 0.2f);
    }
}
