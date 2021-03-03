using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SensibilitySlider : MonoBehaviour {

    public Text sensibilityText;
    Slider slider;

    void Start() {
        slider = GetComponent<Slider>();
        slider.value = FirstPersonCamera.sensitivityX;
    }

    void Update() {
        sensibilityText.text = slider.value.ToString("0.0").Replace(",", ".");
    }

    public void OnValueChanged() {
        FirstPersonCamera.sensitivityX = slider.value;
        FirstPersonCamera.sensitivityY = slider.value;
    }

}
