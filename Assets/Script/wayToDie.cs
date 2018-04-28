using UnityEngine;
using System.Collections;
using UnityEngine.UI ;

public class wayToDie : MonoBehaviour {
	
	public Button[] button ;
	
	public void getWhich(int income) {
		
		
		getData.writeWayToDie(income) ;
		getData.waytodie= income ;
		
		foreach(Button b in button)
		{
			b.enabled=true ;
			b.image.color=Color.white ;
		}
		button[income].enabled=false ;
		button[income].image.color= Color.gray ;
		
		
		
		
	}
	
	void Start(){
		
		button[getData.waytodie ].enabled=false ;
		button[getData.waytodie].image.color= Color.gray ;
	}
	
	
	
	
}
