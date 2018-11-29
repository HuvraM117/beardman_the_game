using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuVolume : MonoBehaviour {

    private AudioSource musicSource;

    [SerializeField] private Slider musicBar;


	// Use this for initialization
	void Start () {

        musicSource = GetComponent<AudioSource>();

        musicBar.value = musicSource.volume;
    }
	
	// Update is called once per frame
	void Update () {
        musicSource.volume = musicBar.value;
    }
}
