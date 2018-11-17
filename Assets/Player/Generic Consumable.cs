using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericConsumable : Consumable{

    public int duration;


    public override void onConsume(GameObject consumable)
    {

    }

    IEnumerator specificAction()
    {
        yield return new WaitForSeconds(duration);
    }
}
