using UnityEngine;
using System.Collections;
using System.Data; 
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Text ;
using Mono.Data.SqliteClient;
using System.IO;

public class game : MonoBehaviour {

	public static List<string> word = new List<string>();
	public static  List<string> hint = new List<string>();
	public static  List<string> type = new List<string>();
	float randomVal ;
	int len;
	int count ;
	int life;
	public static string p ;
	int bCorrect ;
	public static int score ;
	string quiz ;
	public static string answer ;
	public Text showQuiz ; 
	public Text showHint ;
	public Text showScore ;
	public Text showHighscore ;
	public Text showType ;
	int databaseCount ;
	private string connection;
	private IDbConnection dbcon;
	private IDbCommand dbcmd;
	private IDataReader reader;
	private StringBuilder builder;
	//public int version ;
	//static int currentVersion ;
	public  Sprite[] giyoStick;
	public  Sprite[] bombStick;
	public  Sprite[] waterStick;
	public  Sprite[] giyoMaleCustom;
	public  Sprite[] waterMaleCustom;
	public  Sprite[] bombMaleCustom;

	public Image headCustom ;
	public Image pic ;
	public static float[,,] transImage = new float[3,4,5]{{ {-22.0f,42.2f,0f,75.8f,75.8f} , {-50.85f,4.4f,0f,42.3f,42.3f},{-39f,-46.7f,0f,42.3f,42.3f},{-9f,-45.5f,0f,47.8f,47.8f}}    ,    {{-23.2f,38.6f,0f,68.22f,68.22f},{-12.8f,41.7f,0f,63.0f,63.0f},{-14.6f,40.4f,0f,54.576f,54.576f},{-15.9f,43.3f,0f,54.576f,54.576f}}                         , {{-20.8f,42.2f,0f,68.22f,68.22f},{-11.4f,48.4f,0f,63.0f,63.0f},{5.4f,24.9f,0f,29.61f,29.61f},{-15.2f,30.4f,0f,43.2f,43.2f}} }; 


	// Use this for initialization
	void Start () {
		//if(score==0)
		importDatabase ();
		getData.Load();
		life = 4;
		checkPic();
		random.randomNum ( databaseCount+0.0F);// not 8

		answer = word [random.randomVal];
		Debug.Log("-->"+random.randomVal+"\\"+random.randomVal+"\\"+databaseCount+"\\"+p) ;
		answer=answer.ToLower() ;
		if(answer.Length>14)
		{
			showQuiz.fontSize = 20;
		}
		else if(answer.Length>10)
		{
			showQuiz.fontSize = 30;
		}

	

 			Debug.Log( "Vocabulary= "+word[random.randomVal] +"  Hint   ="+hint[random.randomVal]  +"  Type   ="+type[random.randomVal] );
		creatUnderscore (word [random.randomVal]);
		showType.text ="Type : " + type [random.randomVal];
		showScore.text ="Score : " + score as String  ;
		if(hint [random.randomVal].Length>60)
		{
			showHint.fontSize = 14;
		}
		showHint.text ="HINT : "+ hint [random.randomVal];

		switch(getData.level)
		{
		case 0 : showHighscore.text ="HIGH SCORE : "+getData.highscoreEasy;
			break ;
		case 1 : showHighscore.text ="HIGH SCORE : "+getData.highscoreNormal;
			break ;
		case 2 : showHighscore.text ="HIGH SCORE : "+getData.highscoreHard;
			break ;
		}
	}

	// Update is called once per frame
	public void disableButton(Button which)
	{
		which.enabled = false;
		which.image.color= Color.gray ;
	}


	public void checkPic(){
		if(life>0){
		if(getData.characterCurrent==0&&getData.waytodie==0)
		{
				headCustom.enabled=false;
			pic.sprite = giyoStick[life-1] ;
	
		}
			else if(getData.characterCurrent==0&&getData.waytodie==1)
			{
				headCustom.enabled=false;
				pic.sprite = bombStick[life-1] ;
				
			}
			else if(getData.characterCurrent==0&&getData.waytodie==2)
			{
				headCustom.enabled=false;
				pic.sprite = waterStick[life-1] ;
				
			}
			else if(getData.characterCurrent==1&&getData.waytodie==0)
			{
				headCustom.enabled=true;
				setHeadPos();
				pic.sprite = giyoMaleCustom[life-1] ;
				
			}
			else if(getData.characterCurrent==1&&getData.waytodie==1)
			{
				headCustom.enabled=true;
				setHeadPos();
				pic.sprite = bombMaleCustom[life-1] ;
				
			}
			else if(getData.characterCurrent==1&&getData.waytodie==2)
			{
				headCustom.enabled=true;
				setHeadPos();
				pic.sprite = waterMaleCustom[life-1] ;
				if(life==2)
				{
					pic.transform.localScale=new Vector3(1.625f,1.625f,1f) ;
					pic.transform.localPosition= new Vector3(-15,6.25f,1);
				}
				if(life==1)
				{
					pic.transform.localScale=new Vector3(1.3f,1.3f,1) ;
				}
			}
		}
		
	}
	void setHeadPos(){
		if(getData.characterCurrent==1&&getData.waytodie==0)
		{
			headCustom.transform.localPosition =new Vector3(transImage[0,4-life,0],transImage[0,4-life,1],transImage[0,4-life,2]);
			headCustom.transform.localScale =new Vector3(transImage[0,4-life,3],transImage[0,4-life,4],1);


		}
		else if(getData.characterCurrent==1&&getData.waytodie==1)
		{
			headCustom.transform.localPosition =new Vector3(transImage[1,4-life,0],transImage[1,4-life,1],transImage[1,4-life,2]);
			headCustom.transform.localScale =new Vector3(transImage[1,4-life,3],transImage[1,4-life,4],1);
		}
		else if(getData.characterCurrent==1&&getData.waytodie==2)
		{
			headCustom.transform.localPosition =new Vector3(transImage[2,4-life,0],transImage[2,4-life,1],transImage[2,4-life,2]);
			headCustom.transform.localScale =new Vector3(transImage[2,4-life,3],transImage[2,4-life,4],1);
		}

	}

	public void check(string want){
//		Debug.Log (want [0]);
		bCorrect = 0;
		StringBuilder temp = new StringBuilder(quiz);
		for (int i=0; i<answer.Length; i++) 
		{
			if(temp[i*2]=='_')
			{
			 if (want[0] == answer[i]) 
				{
				{temp[i*2]= want[0];
					count++;
						bCorrect=1;
						if(count==answer.Length)
						{
							score++;
							showScore.text ="Score : " + score as String  ;
							Application.LoadLevel("game") ;
						}
					}
				}
			}
		}
		if(bCorrect==0){
			life--;
			//do anime
		/*	if(life==3){
				if(hint [random.randomVal].Length>60)
				{
					showHint.fontSize = 14;
				}
				showHint.text ="HINT : " + hint [random.randomVal]+Environment.NewLine;
			}*/
			if(life==0)
			{
				AudioSource[] items = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
				foreach (AudioSource item in items) {
					DestroyImmediate(item,true) ;
				}
				Application.LoadLevel("gameover") ;
			}
			checkPic();
		}
		quiz = temp.ToString();
		showQuiz.text = quiz ;
	}

	void creatUnderscore(string buildWord)
	{
		len = buildWord.Length;
		StringBuilder temp = new StringBuilder(buildWord);
		for (int i = 0; i < len; i++)
		{
		if(temp[i]==' ')
			{
				quiz+="  " ;
				count++;
			}
			else
			{quiz += "_ ";
			}

		}
		showQuiz.text = quiz ;
	}



	void importDatabase()
	{


		switch(getData.level)
		{
		case 0 : p="databaseEasy.db" ;
			break ;
		case 1 : p="databaseNormal.db";
			break ;
		case 2 : p="databaseHard.db";
			break ;			
		}

		Debug.Log("Call to OpenDB:" + p);
		// check if file exists in Application.persistentDataPath
		string filepath = Application.persistentDataPath + "/" + p;
		Debug.Log (Application.persistentDataPath) ;
	//	if(!File.Exists(filepath))//||version!=currentVersion)
	//	{
			//currentVersion=version;
			Debug.LogWarning("File \"" + filepath + "\" does not exist. Attempting to create from \"" +
			Application.dataPath + "!/assets/" + p);
			// if it doesn't ->
			// open StreamingAssets directory and load the db -> 
			WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/" + p);
			while(!loadDB.isDone) {}
			// then save to Application.persistentDataPath
			File.WriteAllBytes(filepath, loadDB.bytes);
	//}		
		//open db connection
		connection = "URI=file:" + filepath;
		Debug.Log("Stablishing connection to: " + connection);
		dbcon = new SqliteConnection(connection);
		dbcon.Open();
		dbcmd = dbcon.CreateCommand();
		string sqlQuery = "SELECT Vocabulary,Hint,Type " + "FROM Vocabulary";
		//string sqlQuery = "SELECT word,hint,type " + "FROM vocab";
		dbcmd.CommandText = sqlQuery;
		reader = dbcmd.ExecuteReader();
		databaseCount=0;
		while (reader.Read())
		{
			Debug.Log("wer") ;
			databaseCount++ ;
			word.Add ( reader.GetString(0));
			hint.Add ( reader.GetString(1));
			type.Add ( reader.GetString(2));
		}
		reader.Close();
		reader = null;
		dbcmd.Dispose();
		dbcmd = null;
		dbcon.Close();
		dbcon = null;
	  }
	}

