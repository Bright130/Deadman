using UnityEngine;
using System.Collections;

public class changePage : MonoBehaviour {
	public void changeScene(string scene) {
		
		Application.LoadLevel(scene);
	}
}
