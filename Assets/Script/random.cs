using UnityEngine;
using System.Collections;

public class random : MonoBehaviour {

	// Use this for initialization
	
	public static int randomVal ;
	public static void randomNum(float to){
		
		randomVal = (int)Random.Range (0.0F, to)  ;
		
	}

}
