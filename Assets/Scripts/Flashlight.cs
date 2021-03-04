using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour {

    public Transform playerItemPoint;
    public GameObject lightSource;
    public GameObject pointLight;

    public Transform defaultRotation;
    
    public AudioSource soundOn;
    public AudioSource soundOff;

    FirstPersonCamera playerCamera;

    [Header("Config")]
    public int secondsOfCharge;
    int rotationSpeed = 40;

    [HideInInspector] public bool isOn = false;
    [HideInInspector] public bool hasCharge = false;
    bool rotating = false;

    Vector3 cameraRelatedPosition = new Vector3(0.544f, -0.343f, 0.817f);
    Vector3 cameraRelatedScale = new Vector3(0.081f, 0.057f, 0.081f);

    float lerpRotationSpeed = 0.2f;

    void Start() {
        playerCamera = FindObjectOfType<FirstPersonCamera>();

        // transform.SetParent(Camera.main.transform);
        // transform.localPosition = cameraRelatedPosition;
        // transform.localScale = cameraRelatedScale;
    }

    void Update() {
        HandleCharge();
        HandleInput();
        HandleRotate();

        float xAngle = playerCamera.transform.eulerAngles.x;
        if (xAngle > 90) xAngle -= 360;
        
        transform.position = new Vector3
        (
            playerItemPoint.position.x,
            playerItemPoint.position.y + -(xAngle/90),
            playerItemPoint.position.z
        );

        // // Quanto maior o angulo, +Z no playerItemPoint
        // playerItemPoint.position = new Vector3
        // (
        //     playerItemPoint.position.x,
        //     playerItemPoint.position.y,
        //     playerItemPoint.position.z + (xAngle/90)
        // );

        if (!rotating)
            transform.rotation = Quaternion.Lerp(transform.rotation, defaultRotation.transform.rotation, lerpRotationSpeed); 
    }

    void HandleInput() {
        if (Input.GetKeyDown(KeyCode.F)) {
            if (!isOn && hasCharge)
                FlashlightOn();
            else if (isOn)
                FlashlightOff();
        }
    }

    void FlashlightOn() {
        lightSource.SetActive(true);
        pointLight.SetActive(true);
        
        if (soundOn.isPlaying)
            soundOn.Stop();
    
        soundOn.Play();
        
        isOn = true;
    }  

    void FlashlightOff() {
        lightSource.SetActive(false);
        pointLight.SetActive(false);

        if (soundOff.isPlaying)
            soundOff.Stop();

        soundOff.Play();
    
        isOn = false;
    }

    void HandleRotate() {
        if (Input.GetMouseButton(1)) {
            rotating = true;
            playerCamera.isLocked = true;

            float rotX = Input.GetAxis("Mouse X") * rotationSpeed * Mathf.Deg2Rad;
            float rotY = Input.GetAxis("Mouse Y") * rotationSpeed * Mathf.Deg2Rad;

            transform.Rotate(Vector3.left, -rotX);
            transform.Rotate(Vector3.forward, rotY);
        }
        
        if (Input.GetMouseButtonUp(1)) {
            rotating = false;
            playerCamera.isLocked = false;
        }
    }

    void HandleCharge() {
        hasCharge = (BatterySlider.batteryCharge > 0);

        if (!hasCharge && isOn)
            FlashlightOff();
    }

}
