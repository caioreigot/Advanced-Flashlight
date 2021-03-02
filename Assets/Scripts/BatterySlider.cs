using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatterySlider : MonoBehaviour {

    private float decrementAmount;

    public static int batteryCharge;
    
    private float maxDecrementTimer;
    private float decrementTimer;

    private Flashlight playerFlashlight;
    private Slider batterySlider;

    void Start() {
        playerFlashlight = FindObjectOfType<Flashlight>();
        batterySlider = GetComponent<Slider>();

        batterySlider.value = batteryCharge;

        // Temp
        batterySlider.value = 1000;

        maxDecrementTimer = 0.5f;
        decrementTimer = maxDecrementTimer;

        decrementAmount = (1000f * maxDecrementTimer) / playerFlashlight.secondsOfCharge;
    }

    void Update() {
        DecrementCharge();
        CheckCharge();
    }

    void DecrementCharge() {
        if (!playerFlashlight.isOn) return;

        if (decrementTimer <= 0) {
            batterySlider.value -= decrementAmount;
            
            decrementTimer = maxDecrementTimer;
        } else
            decrementTimer -= Time.deltaTime;
    }

    void CheckCharge() {
        batteryCharge = (int)batterySlider.value;
        playerFlashlight.hasCharge = (batteryCharge > 0);
    }

}
