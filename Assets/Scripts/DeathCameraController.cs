using UnityEngine;
using System.Collections.Generic;

public class CameraMove : MonoBehaviour
{
    public List<Camera> targetCameras; // Список всех камер для перехода
    public float speed = 2.0f; // Скорость перехода
    private int currentCameraIndex = 0; // Индекс текущей целевой камеры

    // Переменные для SmoothDamp
    private Vector3 currentVelocity = Vector3.zero;
    private Vector3 rotationVelocity = Vector3.zero;



    void Update()
    {
        if (targetCameras == null || targetCameras.Count == 0) return;

        Camera currentTarget = targetCameras[currentCameraIndex];

        // Плавное движение с постоянной скоростью
        transform.position = Vector3.SmoothDamp(
            transform.position,
            currentTarget.transform.position,
            ref currentVelocity,
            1 / speed // Преобразуем скорость во время сглаживания
        );

        // Плавный поворот
        Vector3 currentRotation = transform.rotation.eulerAngles;
        Vector3 targetRotation = currentTarget.transform.rotation.eulerAngles;

        Vector3 smoothRotation = Vector3.SmoothDamp(
            currentRotation,
            targetRotation,
            ref rotationVelocity,
            1 / speed
        );

        transform.rotation = Quaternion.Euler(smoothRotation);

        // Переход к следующей камере
        if (Vector3.Distance(transform.position, currentTarget.transform.position) < 0.01f)
        {
            currentCameraIndex = (currentCameraIndex + 1) % targetCameras.Count;
        }
    }
}