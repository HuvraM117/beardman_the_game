using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsButton : MonoBehaviour {

    private Button settingsButton;
    [SerializeField] private GameObject settingsPopUp;
    private SceneController sceneController;

	// Use this for initialization
	void Start () {
        //get scene controller
        sceneController = new SceneController();

        settingsButton = GetComponent<Button>(); // <-- you get access to the button component here

        settingsButton.onClick.AddListener(() => { onClick(); });  // <-- you assign a method to the button OnClick event here
        
    }
	
    void onClick()
    {
        //pause the game 
        sceneController.PauseGame();

        //turn the settings pop up on 
        settingsPopUp.SetActive(true);

    }
}
