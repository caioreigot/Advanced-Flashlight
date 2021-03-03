using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pickable : MonoBehaviour {

    public Item item;
    public static Action<Item> ItemPickedEvent;

}
