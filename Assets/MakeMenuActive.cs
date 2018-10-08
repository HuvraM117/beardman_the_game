using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeMenuActive : MonoBehaviour {

	public GameObject currentMenu;
	public GameObject mainMenu;

	public void onClick(){
		currentMenu.SetActive (true);
		mainMenu.SetActive (false);
	}
}
