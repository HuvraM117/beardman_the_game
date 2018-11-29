using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitSettingsButton : MonoBehaviour {

    private Button exitButton;
    [SerializeField] private GameObject settingsPopUp;
    private SceneController sceneController;

    // Use this for initialization
    void Start()
    {
        //get scene controller
        sceneController = new SceneController();

        exitButton = GetComponent<Button>(); // <-- you get access to the button component here

        exitButton.onClick.AddListener(() => { onClick(); });  // <-- you assign a method to the button OnClick event here

    }

    void onClick()
    {
        //turn the settings pop up off
        settingsPopUp.SetActive(false);

        //unpause the game 
        sceneController.UnPauseGame();
    }

}
