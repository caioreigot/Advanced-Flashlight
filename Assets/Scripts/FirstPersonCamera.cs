using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{

    public Transform characterBody;
    public Transform characterHead;

    ushort sensitivityX = 70;
    ushort sensitivityY = 70;

    float rotationX;
    float rotationY;

    float angleYMin = -90;
    float angleYMax = 90;

    public bool isLocked = false;

    // float smoothRotx = 0;
    // float smoothRoty = 0;

    // float smoothCoefx = 0.005f;
    // float smoothCoefy = 0.005f;

    void Start() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate() {
        transform.position = characterHead.position;
    }

    void Update() {
        if (!isLocked) {
            float horizontalDelta = Input.GetAxisRaw("Mouse X");
            float verticalDelta = Input.GetAxisRaw("Mouse Y");

            /*
            // Smooth Camera
            smoothRotx = Mathf.Lerp(smoothRotx, horizontalDelta, smoothCoefx);
            smoothRoty = Mathf.Lerp(smoothRoty, verticalDelta, smoothCoefy);
            */

            rotationX += horizontalDelta * Time.deltaTime * sensitivityX;
            rotationY += verticalDelta * Time.deltaTime * sensitivityY;

            rotationY = Mathf.Clamp(rotationY, angleYMin, angleYMax);

            characterBody.localEulerAngles = new Vector3(0, rotationX, 0);

            transform.eulerAngles = new Vector3(-rotationY, rotationX, 0);
        }
    }

}
