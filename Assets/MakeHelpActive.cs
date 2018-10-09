using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeHelpActive : MonoBehaviour {

	public GameObject helpMenu;
	public GameObject mainMenu;

	public void onClick(){
		helpMenu.SetActive (true);
		mainMenu.SetActive (false);
	}
}
