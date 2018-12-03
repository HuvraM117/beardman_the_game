using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {


    public void PlayGame(){
        PlayerState.hasDied = false;
        Debug.Log("player state = "+ PlayerState.hasDied);
        SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);

	}

	public void PlayBoss(){
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 3);

	}

    public void EndApplication()
    {
        Application.Quit();
    }
}
