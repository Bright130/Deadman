using UnityEngine;
using System.Collections;
using UnityEngine.UI ;

public class clickAndPlaySound : MonoBehaviour {

	public AudioSource target ; 
	// Use this for initialization

		public void play(AudioClip which){
			target.clip = which ;
			target.enabled=true;
			
		}

	}

