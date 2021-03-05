using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FOVSlider : MonoBehaviour {

    Camera playerCamera;
    public Text fovText;

    [HideInInspector]
    public Slider slider;

    void Awake() {
        slider = GetComponent<Slider>();
        playerCamera = Camera.main;
        OnValueChanged();
    }

    void Update() {
        fovText.text = slider.value.ToString("0");
    }

    public void OnValueChanged() {
        playerCamera.fieldOfView = (int)slider.value;
    }

}
