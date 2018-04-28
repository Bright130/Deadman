using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.IO;
using UnityEngine.UI ;

public class capture : MonoBehaviour 
{
	public WebCamTexture mCamera ;
	public Color[] data;
	public Color[] cropPic;
	public Color[] cropPicNew;
	public Color[] head ;
	public Texture2D crop ;
	public RawImage sourceScreen ;
	public 	WebCamDevice[] devices ;
	byte[] bytes ;
	public static int height ;
	public static int width;

	// Use this for initialization
	void Start ()
	{

	devices = WebCamTexture.devices;

		mCamera = new WebCamTexture (devices[getData.switchCam].name,640, 480);
		//GetComponent<Renderer>().material.mainTexture = mCamera ;
		if(mCamera.height<mCamera.width)
		{
			width =  mCamera.width;
			height = mCamera.height;
		}
		else
		{
			height =  mCamera.width;
			width = mCamera.height;
		}


		if(!devices[getData.switchCam].isFrontFacing)
		{
			sourceScreen.transform.Rotate(0,180,0);
		}






		mCamera.Play ();
		data = new Color[height * height];
		cropPic = new Color[height * height] ;
		
		cropPicNew  = new Color[height * height] ;
		unpack();
		
		
	}
	
	
	
	void FixedUpdate()
	{
		GetComponent<RawImage>().texture = mCamera;
	}
	// Update is called once per frame
	public void trig(){	
		
		if(mCamera.height<mCamera.width)
		{
			width =  mCamera.width;
			height = mCamera.height;
		}
		else
		{
			height =  mCamera.width;
			width = mCamera.height;
		}

		Texture2D destTex = new Texture2D(height ,height);
		data=mCamera.GetPixels((width-height)/2,0,height,height);

		if(devices[getData.switchCam].isFrontFacing)
		{
			Rotate();
			flipY();
		}
		else{
			Rotate();
			Rotate();
			Rotate();

		}
		destTex.SetPixels(data);
		destTex.Apply();
		Debug.Log (Application.persistentDataPath.ToString());
		System.IO.File.WriteAllBytes(
			Application.persistentDataPath+"/Custom_head.jpg",
			destTex.EncodeToJPG()
			);
		mCamera.Stop();

		
		cropImage();
	}



	void unpack(){
		if(mCamera.height<mCamera.width)
		{
			width =  mCamera.width;
			height = mCamera.height;
		}
		else
		{
			height =  mCamera.width;
			width = mCamera.height;
		}
		width = height ;
		cropPic = crop.GetPixels(0, 0, crop.width, crop.height);
		
		
		Texture2D textureCrop = new Texture2D(crop.width, crop.height);
		textureCrop.SetPixels(cropPic);
		//	textureCrop.Resize(mCamera.height,mCamera.width);
		textureCrop.Apply();
		
		Texture2D textureNewCrop = ScaleTexture(textureCrop,height,width) ;
		System.IO.File.WriteAllBytes(
			Application.persistentDataPath+"/Crop_head.png",
			textureNewCrop.EncodeToPNG()
			);
		
	}
	
	
	void flipY(){
		if(mCamera.height<mCamera.width)
		{
			width =  mCamera.width;
			height = mCamera.height;
		}
		else
		{
			height =  mCamera.width;
			width = mCamera.height;
		}
		width = height ;
		Color temp;
		int i;
		int j;
		for(i=0;i<height;i++)
		{
			for(j=0;j<width/2;j++)
			{
				temp=data[(width*i)+j] ;
				data[width*i+j] =data[width*(i+1)-j-1];
				data[width*(i+1)-j-1]=temp;
			}
		}
	}
	
	void Rotate(){
		if(mCamera.height<mCamera.width)
		{
			width =  mCamera.width;
			height = mCamera.height;
		}
		else
		{
			height =  mCamera.width;
			width = mCamera.height;
		}
		width = height ;
		Color[] temp = new Color[height*width];
		int i;
		int j;
		long  count=0 ;
		for(i=0;i<height;i++)
		{
			for(j=0;j<width;j++)
			{
				//Debug.Log(count+"     "+(mCamera.height*j+mCamera.width-1-i)) ;
				temp[height*j+(height-1-i)] = data[count];
				count++;
			}
		}
		data=temp ;
	}
	
	private Texture2D ScaleTexture(Texture2D source,int targetWidth,int targetHeight) {
		Texture2D result=new Texture2D(targetWidth,targetHeight,source.format,true);
		Color[] rpixels=result.GetPixels(0);
		float incX=((float)1/source.width)*((float)source.width/targetWidth);
		float incY=((float)1/source.height)*((float)source.height/targetHeight);
		for(int px=0; px<rpixels.Length; px++) {
			rpixels[px] = source.GetPixelBilinear(incX*((float)px%targetWidth),
			                                      incY*((float)Mathf.Floor(px/targetWidth)));
		}
		result.SetPixels(rpixels,0);
		result.Apply();
		return result;
	}
	
	void cropImage()
	{
		
		if(mCamera.height<mCamera.width)
		{
			width =  mCamera.width;
			height = mCamera.height;
		}
		else
		{
			height =  mCamera.width;
			width = mCamera.height;
		}
		width=height ;
		
		head = new Color[height * height] ;
		
		bytes = File.ReadAllBytes(Application.persistentDataPath + "/Crop_head.png");
		Texture2D texture = new Texture2D(width, height);
		texture.LoadImage(bytes);
		cropPicNew = texture.GetPixels(0,0,width,height) ;

		int i=0;
		Debug.Log(data.Length);
		Debug.Log(cropPicNew.Length);
		Debug.Log(head.Length);
		for(i=0;i<data.Length;i++)
		{
			if(cropPicNew[i]==Color.black)
			{
				head[i] = data[i] ;
			}
			else 
			{
				head[i]=Color.clear ;
			}
		}
		
		Texture2D textureCrop = new Texture2D(width,height);
		textureCrop.SetPixels(head);
		textureCrop.Apply();
		
		textureCrop = ScaleTexture(textureCrop,720,720) ; 
		System.IO.File.WriteAllBytes(
			Application.persistentDataPath+"/temp.jpg",
			textureCrop.EncodeToPNG()
			);
	}

	public void switchIt(){


			if(getData.switchCam<devices.Length-1)
			{
				getData.switchCam++;
			}
			else 
			{
				getData.switchCam=0;
			}
		
		
		mCamera.Stop();
	
	}

	public void stopCam()
	{
		mCamera.Stop();
	}

}
