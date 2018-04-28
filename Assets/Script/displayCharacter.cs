using UnityEngine;
using System.Collections;
using UnityEngine.UI ;

public class displayCharacter : MonoBehaviour {

	public Image show ;
	public Sprite stickman ;
	public Sprite maleCustom ;
	public Image headCustom ;
	
	// Use this for initialization

	
	void Start () {

		getData.Load();
		if (getData.characterCurrent == 0) {
			headCustom.enabled=false ;
			show.sprite = stickman ;
		}
		else if (getData.characterCurrent == 1) {
			headCustom.enabled=true ;
			show.sprite = maleCustom ;
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
