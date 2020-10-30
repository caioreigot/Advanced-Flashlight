using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{

    public GameObject lightSource;
    public Transform defaultRotation;
    public AudioSource soundOn;
    public AudioSource soundOff;

    private bool isOn = false;
    private bool rotating = false;

    private int rotationSpeed = 35;
    
    void Update() {
        HandleInput();
        HandleRotate();
    }

    void HandleInput() {
        if (Input.GetKeyDown(KeyCode.F)) {
            if (!isOn)
                FlashlightOn();
            else 
                FlashlightOff();
        }
    }

    void FlashlightOn() {
        lightSource.SetActive(true);
        soundOn.Play();
        isOn = true;
    }  

    void FlashlightOff() {
        lightSource.SetActive(false);
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
            transform.rotation = Quaternion.Lerp(transform.rotation, defaultRotation.transform.rotation, 0.1f);
            
            if (transform.rotation == defaultRotation.transform.rotation || rotating)
                yield break;
            
            yield return new WaitForSeconds(0.02f);
        }
    }

}
