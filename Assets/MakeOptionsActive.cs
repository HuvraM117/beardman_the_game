using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeOptionsActive : MonoBehaviour {

	public GameObject optionsMenu;
	public GameObject mainMenu;

	public void onClick(){
		optionsMenu.SetActive (true);
		mainMenu.SetActive (false);
	}
}
