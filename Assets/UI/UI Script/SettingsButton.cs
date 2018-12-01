using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsButton : MonoBehaviour {

    private Button settingsButton;
    [SerializeField] private GameObject settingsPopUp;
    [SerializeField] private Sprite exitButtonSprite;
    private Sprite settingsButtonSprite;
    private SceneController sceneController;
    private Image settingButtonImage;

    // Use this for initialization
    void Start () {
        //get scene controller
        sceneController = new SceneController();

        settingsButton = GetComponent<Button>(); // <-- you get access to the button component here

        settingButtonImage = GetComponent<Image>();

        settingsButton.onClick.AddListener(() => { onClick(); });  // <-- you assign a method to the button OnClick event here

        settingsButtonSprite = settingButtonImage.sprite;
    }
	
    void onClick()
    {

        if (settingsPopUp.active)
        {
            //turn the settings pop up off 
            settingsPopUp.SetActive(false);

            //unpause the game 
            sceneController.UnPauseGame();

            //settingsButton
            settingButtonImage.sprite = settingsButtonSprite;
        }
        else
        {
            //pause the game 
            sceneController.PauseGame();

            //turn the settings pop up on 
            settingsPopUp.SetActive(true);

            settingButtonImage.sprite = exitButtonSprite;
        }

    }
}
