using UnityEngine;
using System.Collections.Generic;

public class CameraMove : MonoBehaviour
{
    public List<Camera> targetCameras; // ������ ���� ����� ��� ��������
    public float speed = 2.0f; // �������� ��������
    private int currentCameraIndex = 0; // ������ ������� ������� ������

    // ���������� ��� SmoothDamp
    private Vector3 currentVelocity = Vector3.zero;
    private Vector3 rotationVelocity = Vector3.zero;



    void Update()
    {
        if (targetCameras == null || targetCameras.Count == 0) return;

        Camera currentTarget = targetCameras[currentCameraIndex];

        // ������� �������� � ���������� ���������
        transform.position = Vector3.SmoothDamp(
            transform.position,
            currentTarget.transform.position,
            ref currentVelocity,
            1 / speed // ����������� �������� �� ����� �����������
        );

        // ������� �������
        Vector3 currentRotation = transform.rotation.eulerAngles;
        Vector3 targetRotation = currentTarget.transform.rotation.eulerAngles;

        Vector3 smoothRotation = Vector3.SmoothDamp(
            currentRotation,
            targetRotation,
            ref rotationVelocity,
            1 / speed
        );

        transform.rotation = Quaternion.Euler(smoothRotation);

        // ������� � ��������� ������
        if (Vector3.Distance(transform.position, currentTarget.transform.position) < 0.01f)
        {
            currentCameraIndex = (currentCameraIndex + 1) % targetCameras.Count;
        }
    }
}