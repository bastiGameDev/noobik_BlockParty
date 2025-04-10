using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float turnSpeed = 10.0f;
    public Camera playerCamera;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component is missing on the game object " + gameObject.name);
        }
    }

    void Update()
    {
        // Получаем ввод с клавиатуры
        float moveForwardBack = Input.GetAxis("Vertical");
        float moveLeftRight = Input.GetAxis("Horizontal");

        // Перемещение персонажа
        Vector3 move = transform.right * moveLeftRight + transform.forward * moveForwardBack;
        transform.position += move * moveSpeed * Time.deltaTime;

        // Управление анимацией
        if (moveForwardBack != 0 || moveLeftRight != 0)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }
}
