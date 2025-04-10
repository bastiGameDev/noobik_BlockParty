using UnityEngine;

public class RotateAroundY : MonoBehaviour
{
    public float rotationSpeed = 50f; // �������� ��������

    void Update()
    {
        // ������� ������ ������ ��� Y
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
