using UnityEngine;
using System.Collections;
using UnityEngine.UI ;
public class level : MonoBehaviour {


	public Button[] button ;
	
	public void getWhich (int income) {
		
		
		getData.writeLevel(income) ;
		getData.level = income ;
		
		foreach(Button b in button)
		{
			b.enabled=true ;
			b.image.color=Color.white ;
		}
		button[income].enabled=false ;
		button[income].image.color= Color.gray ;		
		
	}
	
	void Start(){		
		button[getData.level ].enabled=false ;
		button[getData.level].image.color= Color.gray ;
	}
	
	
	
	

}
