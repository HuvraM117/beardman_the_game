using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Consumable : MonoBehaviour{
    abstract public void onConsume(GameObject consumable);
}
