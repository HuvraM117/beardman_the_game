using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeVolume : MonoBehaviour {

    public Slider musicBar;
    public Slider soundFXBar;

    private AudioSource musicSource;
    private AudioSource soundFXSource;

    // Use this for initialization
    void Start () {
        musicBar.maxValue = 1f;
        musicBar.minValue = 0f;

        soundFXBar.maxValue = 1f;
        soundFXBar.minValue = 0f;


        //need to get the audioSource for the background 
        var music = GameObject.Find("Beard Man/BackgroundMusicSource");
        musicSource = music.GetComponents<AudioSource>()[0];

        //need to get the audioSource for the sound effects 
        var soundFX = GameObject.Find("Beard Man/MusicMaker");
        soundFXSource = soundFX.GetComponents<AudioSource>()[0];

        musicBar.value = musicSource.volume;
        soundFXBar.value = soundFXSource.volume;
    }
	
	// Update is called once per frame
	void Update () {

        //to change the volume, just use musicSource.volume 
        musicSource.volume = musicBar.value;
        soundFXSource.volume = soundFXBar.value;

	}


}
