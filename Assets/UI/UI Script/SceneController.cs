using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneController : MonoBehaviour {
	public GameObject Endoflevel;
	public GameObject Player;
	public GameObject Backgroud;
	public GameObject MiddleLayerBackgroud;
    public GameObject SettingsPopUp;
	public Vector3 MiddleLayerBackgroudOffset;
	public float moveRatio;
	public Vector3 backgroundOffset;
    private Camera BossLevelCamera;
    private Camera normalCamera;
	private float startOfLevel;
	private float progress;
	public int Range=10;

	// Use this for initialization
	void Start () {
        //BossLevelCamera = this.gameObject.GetComponents<Camera>()[0];
        //normalCamera = Player.GetComponents<Camera>()[0];
		startOfLevel=Player.transform.position.x;
        SettingsPopUp.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        var sceneName = SceneManager.GetActiveScene().name;


        var levelComplete = LevelComplete();
	//	Backgroud.transform.position = Player.transform.position+backgroundOffset+new Vector3(startOfLeel);

		scrollBackgroud ();
        if (sceneName.Equals("LeadUpToBoss"))
        { 
            if (LevelComplete())
            {
                SceneManager.LoadScene(3);
            }
        }else if (LevelComplete())
        {
            SceneManager.LoadScene(0);
        }
        
	}
    
    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1;
    }


    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }


	bool LevelComplete(){

        return (Vector3.Distance (Player.transform.position, Endoflevel.transform.position) < Range);
        
    }

	void scrollBackgroud(){
		progress = (Player.transform.position.x-startOfLevel) * moveRatio;

		Backgroud.transform.position = Player.transform.position+backgroundOffset+new Vector3(-progress,0,0);
		MiddleLayerBackgroud.transform.position = new Vector3 (Player.transform.position.x + MiddleLayerBackgroudOffset.x - progress * 10, MiddleLayerBackgroud.transform.position.y, MiddleLayerBackgroud.transform.position.z);
	}

    
}
