using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthConsumable : Consumable {

    public int duration;

    public int healthRestore;

    private AudioSource musicSource;

    private AudioClip powerUpNoise;

    private void Start()
    {

        var beardman = GameObject.Find("Beard Man/MusicMaker");

        musicSource = beardman.GetComponents<AudioSource>()[0];

        var powerUpNoise = Resources.LoadAll<AudioClip>("Sound")[0];

    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        onConsume(collision.gameObject);
    }

    public  override void onConsume(GameObject consumable)
    {
        PlayerState playerstate = consumable.GetComponent<PlayerState>();

        if (playerstate != null)
        {
            musicSource.PlayOneShot(powerUpNoise);
            playerstate.TakeDamage(0 - healthRestore);
            gameObject.SetActive(false);
        }

        //gameObject.GetComponent<Animator>().SetTrigger("Consumed");
    }
}
