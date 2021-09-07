using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FPSCamera : MonoBehaviour
{

    [Header("Cam")]
    public float mouseSensitivity = 100f;
    public float xMouseSensivity { get; set; } = 50f;
    public float yMouseSensivity { get; set; } = 50f;

    public Transform playerBody;

    float xRotation = 0f;

    [Header("Raycast")]
    public Transform head;
    public float rangePlayer;
    public LayerMask layerInteractable;

    [Header("Sensivity Sliders")]
    public Slider xSlider;
    public Slider ySlider;

    [Header("Sensivity numbers")]
    public TextMeshProUGUI xSliderText;
    public TextMeshProUGUI ySliderText;

    void Start()
    {
        LoadSensivity();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        CameraFollowMouse();
        Raycast();
    }

    void LoadSensivity()
    {
        if (PlayerPrefs.GetFloat("xMouseSensivity") == 0)
        {
            xMouseSensivity = 50f;
            yMouseSensivity = 50f;
            SaveSensivity();
        }
        else
        {
            xMouseSensivity = PlayerPrefs.GetFloat("xMouseSensivity");
            yMouseSensivity = PlayerPrefs.GetFloat("yMouseSensivity");
        }

        xSlider.value = xMouseSensivity;
        ySlider.value = yMouseSensivity;
    }

    public void SaveSensivity()
    {
        PlayerPrefs.SetFloat("xMouseSensivity", xSlider.value);
        PlayerPrefs.SetFloat("yMouseSensivity", ySlider.value);
    }

    public void ActualiseSensivityText()
    {

    }

    void CameraFollowMouse()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * xMouseSensivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * yMouseSensivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    void Raycast()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward * rangePlayer);
        if (Input.GetMouseButton(0))
        { 
            if (Physics.Raycast(transform.position, transform.forward, out hit, rangePlayer, layerInteractable))
            {
            }
        }
    }

}
