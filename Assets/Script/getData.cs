using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.IO;
using UnityEngine.UI ;

public class getData : MonoBehaviour {

	public static int highscoreEasy ;
	public static int highscoreNormal ;
	public static int highscoreHard ;
	public static int level ;
	public static int DBversion ;
	public static int characterCurrent ;
	public static int waytodie ;
	public static int switchCam ;
	public Text showHighscore ;
	static int i;
	public AudioSource soundgetStart;
	public AudioClip audiogetStart ;

	public static bool isOpen=false ;
	//string path = Application.dataPath+"/data.txt"; //iOS is read only
	// Use this for initialization
	void Start()
	{
		Load();
		//string path = Application.dataPath+"/data.txt"; //iOS is read only

		showHighscore.text = "Highscore Easy: "+ highscoreEasy+ Environment.NewLine+ "Highscore Normal: "+ highscoreNormal+ Environment.NewLine+ "Highscore Hard: "+ highscoreHard;
		//Debug.Log (path);
		//string createText = "SHITTTT" + Environment.NewLine+"FUCK U";
		//File.WriteAllText(path, createText);

		if(!isOpen)
		{
		soundgetStart.clip = audiogetStart ;
		soundgetStart.Play();
			isOpen=true ;
		}
		else
		{
			AudioSource[] items = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
			
			foreach (AudioSource item in items) {
				//foreach(AudioClip which in except)
				if(item.clip==null){
					DestroyImmediate(item ,true)  ;
				}
			}
		}
	}



	public static void writeHighscoreEasy(int data)
	{
		string path = Application.persistentDataPath + "/data.txt";
		Load ();
		string createText = data.ToString() + Environment.NewLine+ highscoreNormal.ToString() + Environment.NewLine+ highscoreHard.ToString()+ Environment.NewLine+ level.ToString()+ Environment.NewLine+ characterCurrent.ToString()+ Environment.NewLine+waytodie.ToString()+ Environment.NewLine+"highscoreE-H level  character waytodie";
		File.WriteAllText(path, createText);
	}

	public static void writeHighscoreNormal(int data)
	{
		string path = Application.persistentDataPath + "/data.txt";
		Load ();
		string createText = highscoreEasy.ToString() + Environment.NewLine+ data.ToString() + Environment.NewLine+  highscoreHard.ToString()+ Environment.NewLine+ level.ToString()+ Environment.NewLine+ characterCurrent.ToString()+ Environment.NewLine+waytodie.ToString()+ Environment.NewLine+"highscoreE-H level  character waytodie";
		File.WriteAllText(path, createText);
	}


	public static void writeHighscoreHard(int data)
	{
		string path = Application.persistentDataPath + "/data.txt";
		Load ();
		string createText = highscoreEasy.ToString() + Environment.NewLine+ highscoreNormal.ToString() + Environment.NewLine+   data.ToString()+ Environment.NewLine+ level.ToString()+ Environment.NewLine+ characterCurrent.ToString()+ Environment.NewLine+waytodie.ToString()+ Environment.NewLine+"highscoreE-H level  character waytodie";
		File.WriteAllText(path, createText);
	}

	public static void writeLevel(int data)
	{
		string path = Application.persistentDataPath + "/data.txt";
		Load ();
		string createText = highscoreEasy.ToString() + Environment.NewLine+ highscoreNormal.ToString() + Environment.NewLine+ highscoreHard.ToString()+ Environment.NewLine+  data.ToString()+ Environment.NewLine+ characterCurrent.ToString()+ Environment.NewLine+waytodie.ToString()+ Environment.NewLine+"highscoreE-H level  character waytodie";
		File.WriteAllText(path, createText);
	}

	public static void writeCharacter(int data)
	{
		string path = Application.persistentDataPath + "/data.txt";
		Load ();
		string createText = highscoreEasy.ToString() + Environment.NewLine+ highscoreNormal.ToString() + Environment.NewLine+ highscoreHard.ToString()+ Environment.NewLine+ level.ToString()+ Environment.NewLine+  data.ToString()+ Environment.NewLine+waytodie.ToString()+ Environment.NewLine+"highscoreE-H level  character waytodie";
		File.WriteAllText(path, createText);
	}
	
	public static void writeWayToDie(int data)
	{
		string path = Application.persistentDataPath + "/data.txt";
		Load ();
		string createText = highscoreEasy.ToString() + Environment.NewLine+ highscoreNormal.ToString() + Environment.NewLine+ highscoreHard.ToString()+ Environment.NewLine+ level.ToString()+ Environment.NewLine+ characterCurrent.ToString()+ Environment.NewLine+ data.ToString()+ Environment.NewLine+"highscoreE-H level  character waytodie";
		File.WriteAllText(path, createText);
	}

	public static void Load()
	{
		// Handle any problems that might arise when reading the text
		string[] createNewText = {"0","0","0","0","0","0","0"};
		string path = Application.persistentDataPath + "/data.txt";
		try{
			
			string[] readText = File.ReadAllLines(path);
			i=0;
			foreach (string s in readText)
			{
				if(i==0){
					
					
					Int32.TryParse(s, out highscoreEasy);
					
					Debug.Log(highscoreEasy) ;
					
				}
				else if(i==1){
					
					
					Int32.TryParse(s, out highscoreNormal);
					
					Debug.Log(highscoreNormal) ;
					
				}
				else if(i==2){
					
					
					Int32.TryParse(s, out highscoreHard);
					
					Debug.Log(highscoreHard) ;
					
				}
				else if(i==3){
					
					
					Int32.TryParse(s, out level);
					
					Debug.Log(highscoreHard) ;
					
				}
				else if(i==4){
					
					
					Int32.TryParse(s, out characterCurrent);
					
					Debug.Log(characterCurrent) ;
					
				}
				else if(i==5){
					
					
					Int32.TryParse(s, out waytodie);
					
					Debug.Log(waytodie) ;
					
				}
				else if(i==6){
					
					
					Int32.TryParse(s, out DBversion);
					
					Debug.Log(DBversion) ;
					
				}
				

				i++;
				
				//Debug.Log(s) ;
				
			}
			//File.WriteAllLines(path, createText);
		}
		
		catch
		{
			
			File.WriteAllLines(path, createNewText);
			Application.LoadLevel("getStart") ;
		}
		
		
		

	}

}
