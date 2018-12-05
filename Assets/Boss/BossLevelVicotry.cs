using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BossLevelVicotry : MonoBehaviour {
	private GameObject boss;
	// Use this for initialization
	void Start () {
		boss = GameObject.Find ("Barber");
		if(boss!=null)
			Debug.Log("BOSS IS ALIVE");
	}
	
	// Update is called once per frame
	void Update () {
		if (boss == null)
			StartCoroutine (Wait ()); 
		
	}

	IEnumerator Wait(){
		yield return new WaitForSeconds (2f);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
