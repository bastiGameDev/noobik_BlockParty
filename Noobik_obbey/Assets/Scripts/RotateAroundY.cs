using UnityEngine;

public class RotateAroundY : MonoBehaviour
{
    public float rotationSpeed = 50f; // Скорость вращения

    void Update()
    {
        // Вращаем объект вокруг оси Y
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
