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

    private bool chargeEndedThisFrame = false;

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
        if (decrementTimer <= 0) {
            batterySlider.value -= decrementAmount;
            batteryCharge = (int)batterySlider.value;
            
            decrementTimer = maxDecrementTimer;
        } else
            decrementTimer -= Time.deltaTime;

    }

}
