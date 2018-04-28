using UnityEngine;
using System.Collections;
using UnityEngine.UI ;

public class character : MonoBehaviour {

	public Button[] button ;

	public void getWho (int income) {
	

			getData.writeCharacter(income) ;
			getData.characterCurrent = income ;
		
		foreach(Button b in button)
		{
			b.enabled=true ;
			b.image.color=Color.white ;
		}
		button[income].enabled=false ;
		button[income].image.color= Color.gray ;




	}

	void Start(){

		button[getData.characterCurrent ].enabled=false ;
		button[getData.characterCurrent].image.color= Color.gray ;
	}



	
	

}
