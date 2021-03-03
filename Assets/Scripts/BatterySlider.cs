using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatterySlider : MonoBehaviour {

    float decrementAmount;

    public static int batteryCharge;
    
    float maxDecrementTimer;
    float decrementTimer;

    Flashlight playerFlashlight;
    Slider batterySlider;

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
        DecrementChargeTimer();
        CheckCharge();
    }

    void DecrementChargeTimer() {
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

    public void IncreaseCharge(int amount) {
        batterySlider.value = Mathf.Clamp(batterySlider.value + amount, 0, 1000);
        batteryCharge = (int)batterySlider.value;
    }

}
