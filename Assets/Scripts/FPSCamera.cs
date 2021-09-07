using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCamera : MonoBehaviour
{

    [Header("Cam")]
    public float mouseSensitivity = 100f;

    public Transform playerBody;

    float xRotation = 0f;


    [Header("Raycast")]
    public Transform head;
    public float rangePlayer;
    public LayerMask layerInteractable;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        CameraFollowMouse();
        Raycast();
    }

    void CameraFollowMouse()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    void Raycast()
    {
        RaycastHit hit;
        Debug.DrawRay(head.position, transform.forward * rangePlayer);

        if (Physics.Raycast(head.position, transform.forward, out hit, rangePlayer, layerInteractable))
        {

        }
    }

}
