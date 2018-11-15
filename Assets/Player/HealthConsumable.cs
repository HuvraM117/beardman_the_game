using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthConsumable : Consumable {

    public int duration;

    public int healthRestore;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        onConsume(collision.gameObject);
    }

    public  override void onConsume(GameObject consumable)
    {
        PlayerState playerstate = consumable.GetComponent<PlayerState>();

        if (playerstate != null)
        {
            //TODO : play the power-up noise 
            playerstate.TakeDamage(0 - healthRestore);
            gameObject.SetActive(false);
        }

        //gameObject.GetComponent<Animator>().SetTrigger("Consumed");
    }
}
