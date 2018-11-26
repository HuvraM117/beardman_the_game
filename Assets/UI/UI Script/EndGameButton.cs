using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameButton : MonoBehaviour {

    private Button endGameButton;
    private SceneController sceneController;

    // Use this for initialization
    void Start()
    {
        //get scene controller
        sceneController = new SceneController();

        endGameButton = GetComponent<Button>(); // <-- you get access to the button component here

        endGameButton.onClick.AddListener(() => { onClick(); });  // <-- you assign a method to the button OnClick event here

    }

    void onClick()
    {
        //unpause the game 
        sceneController.UnPauseGame();

        //load the main menu 
        sceneController.GoToMenu();
    }
}
