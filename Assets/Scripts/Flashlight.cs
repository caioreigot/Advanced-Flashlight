using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour {

    public GameObject lightSource;
    public GameObject pointLight;
    public Transform defaultRotation;
    public AudioSource soundOn;
    public AudioSource soundOff;

    [Header("Setup")]
    public int secondsOfCharge;

    [HideInInspector]
    public bool isOn = false;
    
    [HideInInspector]
    public bool hasCharge = false;
    
    private bool rotating = false;

    private int rotationSpeed = 40;

    void Update() {
        HandleCharge();
        HandleInput();
        HandleRotate();
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
            FindObjectOfType<FirstPersonCamera>().isLocked = true;

            float rotX = Input.GetAxis("Mouse X") * rotationSpeed * Mathf.Deg2Rad;
            float rotY = Input.GetAxis("Mouse Y") * rotationSpeed * Mathf.Deg2Rad;

            transform.Rotate(Vector3.left, -rotX);
            transform.Rotate(Vector3.forward, rotY);
        }
        
        if (Input.GetMouseButtonUp(1)) {
            rotating = false;
            FindObjectOfType<FirstPersonCamera>().isLocked = false;
            StartCoroutine(ResetRotation());
        }
    }

    IEnumerator ResetRotation() {
        while (true) {
            transform.rotation = Quaternion.Lerp(transform.rotation, defaultRotation.transform.rotation, 0.05f);
            
            if (transform.rotation == defaultRotation.transform.rotation || rotating)
                yield break;
            
            yield return new WaitForSeconds(0.01f);
        }
    }

    void HandleCharge() {
        hasCharge = (BatterySlider.batteryCharge > 0);

        if (!hasCharge && isOn)
            FlashlightOff();
    }

}
