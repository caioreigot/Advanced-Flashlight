using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour {

    Item item = Item.Battery;
    BatterySlider batterySlider;

    [Range(1, 1000)]
    public int chargeAmount;

    void Awake() => Pickable.ItemPickedEvent += ChargeBattery;
    void OnDestroy() => Pickable.ItemPickedEvent -= ChargeBattery;

    void Start() {
        batterySlider = FindObjectOfType<BatterySlider>();
    }

    void ChargeBattery(Item checkItem) {
        if (!(checkItem == item)) return;
        
        batterySlider.IncreaseCharge(chargeAmount);        
    }

}
