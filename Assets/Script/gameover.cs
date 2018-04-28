using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.IO;
using UnityEngine.UI ;

public class gameover : MonoBehaviour {
	public Text showscore ;
	public Sprite giyoStick ;
	public Sprite giyoMaleCustom ;
	public Sprite bombStick ;
	public Sprite waterMaleCustom ;
	public  Sprite waterStick;
	public  Sprite bombMaleCustom;
	public Image pic ;
	public Image headCustom;
	int highscore;
	// Use this for initialization
	void Start () {
		getData.Load();
		if(game.answer.Length>14)
		{
			showscore.fontSize = 20;
		}
		else if(game.answer.Length>10)
		{
			showscore.fontSize = 30;
		}

		showscore.text = "GAME OVER !"+ Environment.NewLine + Environment.NewLine +"ANSWER : "+game.answer.ToUpper() + Environment.NewLine +"YOUR SCORE : "+game.score.ToString() ;
		if(getData.characterCurrent==0&&getData.waytodie==0)
		{
			headCustom.enabled=false;
			pic.sprite=giyoStick ;
		}
		else if(getData.characterCurrent==0&&getData.waytodie==1)
		{
			headCustom.enabled=false;
			pic.sprite=bombStick ;
		}
		else if(getData.characterCurrent==0&&getData.waytodie==2)
		{
			headCustom.enabled=false;
			pic.sprite=waterStick ;
		}
		else if(getData.characterCurrent==1&&getData.waytodie==0)
		{
			headCustom.enabled=true;

			pic.sprite = giyoMaleCustom ;
			
			headCustom.transform.localPosition =new Vector3(4f,8.5f,0);
			headCustom.transform.localScale =new Vector3(1.887f,1.887f,1);
			
		}
		else if(getData.characterCurrent==1&&getData.waytodie==1)
		{
			headCustom.enabled=true;
			
			pic.sprite = bombMaleCustom ;
			
			headCustom.transform.localPosition =new Vector3(18.2f,-27.2f,0);
			headCustom.transform.localScale =new Vector3(1.2f,1.2f,1);
			
		}
		else if(getData.characterCurrent==1&&getData.waytodie==2)
		{
			headCustom.enabled=true;

			pic.sprite = waterMaleCustom ;

			headCustom.transform.localPosition =new Vector3(-55.1f,-30.0f,0);
			headCustom.transform.localScale =new Vector3(0.561f,0.561f,1);
			headCustom.transform.Rotate(0,0,90) ;
		}


		switch(getData.level)
		{
		case 0 :if (game.score > getData.highscoreEasy) {
				getData.highscoreEasy = game.score ;
				getData.writeHighscoreEasy(game.score) ;
			}
			break ;
		case 1 : if (game.score > getData.highscoreNormal) {
				getData.highscoreNormal = game.score ;
				getData.writeHighscoreNormal(game.score) ;
			}
			break ;
		case 2 : if (game.score > getData.highscoreHard) {
				getData.highscoreHard = game.score ;
				getData.writeHighscoreHard(game.score) ;
			}
			break ;
			
		}







		game.score =0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
