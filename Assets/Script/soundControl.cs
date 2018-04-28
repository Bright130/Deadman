using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class soundControl : MonoBehaviour {

	public AudioSource sound ;
	public AudioClip[] except ;

	// Use this for initialization
	
	void Awake() {
		DontDestroyOnLoad(sound);



	}
	public void play(AudioClip which){
		sound.clip = which ;
		sound.enabled=true;
	
	}

	public void playSound()
	{
		sound.enabled=true ;
	}

	public void delSound(){

		AudioSource[] items = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
		foreach (AudioSource item in items) {
			DestroyImmediate(item,true) ;
		}
	}


	public void delOtherSound(){
		AudioSource[] items = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];

	

		foreach (AudioSource item in items) {

			if(item.clip!=except[0]&&item.clip!=except[1]){
				       
			DestroyImmediate(item,true) ;
			}
		
		}


	
	}

	public void delStackSound(AudioClip which){
		int countStack=0;
		AudioSource[] items = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
		
		foreach (AudioSource item in items) {
			//foreach(AudioClip which in except)
			if(item.clip==which){
				countStack++;

			}
		}



		foreach (AudioSource item in items) {
			//foreach(AudioClip which in except)
			if(item.clip==which){

				if(countStack>1)
				{

				DestroyImmediate(item,true) ;

					countStack--;
				}

			}
		}




	}




}






