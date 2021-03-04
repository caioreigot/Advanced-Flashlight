using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SensitivitySlider : MonoBehaviour {

    public Text sensibilityText;

    [HideInInspector]
    public Slider slider;

    void Awake() {
        slider = GetComponent<Slider>();
        OnValueChanged();
    }

    void Update() {
        sensibilityText.text = slider.value.ToString("0.0").Replace(",", ".");
    }

    public void OnValueChanged() {
        FirstPersonCamera.sensitivityX = slider.value;
        FirstPersonCamera.sensitivityY = slider.value;
    }

}
