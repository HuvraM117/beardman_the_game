using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneController : MonoBehaviour {
	public GameObject Endoflevel;
	public GameObject Player;
	public GameObject Backgroud;
	public GameObject MiddleLayerBackgroud;
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
        startOfLevel =Player.transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
        var sceneName = SceneManager.GetActiveScene().name;


        var levelComplete = LevelComplete();
	//	Backgroud.transform.position = Player.transform.position+backgroundOffset+new Vector3(startOfLeel);

		scrollBackgroud ();
 
		if (LevelComplete())
        {

			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
		if(MiddleLayerBackgroud!=null)
			MiddleLayerBackgroud.transform.position = new Vector3 (Player.transform.position.x + MiddleLayerBackgroudOffset.x - progress * 10, Player.transform.position.y, MiddleLayerBackgroud.transform.position.z);

	}

    
}
