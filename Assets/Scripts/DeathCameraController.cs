using UnityEngine;
using System.Collections.Generic;


/*
This script was created to make cinematic transitions between cameras
It went something like this. But I have no idea how to do it properly, so I used a bicycle.
It works like this: The main camera moves towards the second camera to take its position.
Then it moves towards the third camera, and so on in a circle.


 */

public class CameraMove : MonoBehaviour
{
    public List<Camera> targetCameras;  // List of all cameras to go to
    public float speed = 2.0f;          // Transition speed
    private int currentCameraIndex = 0; // Index of the current target cell

    // Variables for SmoothDamp
    private Vector3 currentVelocity = Vector3.zero;
    private Vector3 rotationVelocity = Vector3.zero;



    void Update()
    {

        // Choose which camera to move to
        Camera currentTarget = targetCameras[currentCameraIndex];

        // Smooth movement at a constant speed
        transform.position = Vector3.SmoothDamp(
            transform.position,
            currentTarget.transform.position,
            ref currentVelocity,
            1 / speed // Convert the velocity during smoothing
        );

        // Smooth turn
        Vector3 currentRotation = transform.rotation.eulerAngles;
        Vector3 targetRotation = currentTarget.transform.rotation.eulerAngles;

        // Smooth rotation using SmoothDamp
        Vector3 smoothRotation = Vector3.SmoothDamp(
            currentRotation,
            targetRotation,
            ref rotationVelocity,
            1 / speed
        );

        // Apply smooth rotation to the camera
        transform.rotation = Quaternion.Euler(smoothRotation);

        // Move to the next camera
        if (Vector3.Distance(transform.position, currentTarget.transform.position) < 0.01f)
        {
            currentCameraIndex = (currentCameraIndex + 1) % targetCameras.Count;
        }
    }
}