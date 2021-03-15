using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFaceCam : MonoBehaviour {

    Camera cameraMain;

    void Start() => cameraMain = Camera.main;

    void Update() {
        transform.LookAt
        (
            transform.position + cameraMain.transform.rotation * Vector3.forward, 
            cameraMain.transform.rotation * Vector3.up
        );
    }

}
