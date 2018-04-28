using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.IO;
using UnityEngine.UI ;

public class customHeadDisplay : MonoBehaviour {
	public Image show ;
	byte[] bytes;
	Sprite sprite ;
	public bool showTemp ;


	// Use this for initialization
	void Start () {
		getData.Load();
		if(showTemp==false){
		try
		{    
			bytes = File.ReadAllBytes(Application.persistentDataPath + "/head.jpg");
			Texture2D texture = new Texture2D(720,720);
			texture.LoadImage(bytes);
			sprite = Sprite.Create(texture, new Rect(0,0,  720, 720), new Vector2(0.5f,0.0f));
			show.sprite=sprite ;
		}
		catch{
				show.enabled=false;
		}
		}
		else
		{
			readTemp ();
		}
		// if (getData.characterCurrent == 1) {

		//}

	}
	public  Texture2D readTemp(){

		try
		{    
			bytes = File.ReadAllBytes(Application.persistentDataPath + "/temp.jpg");
			Texture2D texture = new Texture2D(720,720);
			texture.LoadImage(bytes);
			sprite = Sprite.Create(texture, new Rect(0,0,  720, 720), new Vector2(0.5f,0.0f));
			show.sprite=sprite ;
			return texture ;
		}
		catch{
			Debug.Log("sdfsdf");
			return null;
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
	
	public void save(){
		try
		{    
			bytes = File.ReadAllBytes(Application.persistentDataPath + "/temp.jpg");
			Texture2D texture = new Texture2D(720,720);
			texture.LoadImage(bytes);
			System.IO.File.WriteAllBytes(
				Application.persistentDataPath+"/head.jpg",
				texture.EncodeToPNG()
				);
			
			
		}
		catch{
			Debug.Log("sdfsdf");
			
		}
		
	}


}
