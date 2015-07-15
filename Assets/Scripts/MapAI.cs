using UnityEngine;
using System.Collections;

public class MapAI : MonoBehaviour {

	public UISprite [] backs;
	public UISprite [] lockeds;
	public UILabel [] number;
	// Use this for initialization
	public GameObject loader;

	public static MapAI instance;

	public UITexture banner;

	void Awake()
	{
		instance = (MapAI)gameObject.GetComponent("MapAI");
	}

	void Start () 
	{
		Vector2 asp = GetAspectRatio(Screen.width,Screen.height);

		/*if (asp.x == 3 && asp.x == 2)
		{
			banner.width = 116;
		}
		else if (asp.x == 16 && asp.x == 9)
		{
			banner.width = 157;
		}*/
		if (Screen.width == 480 && Screen.height == 320)
		{
			banner.width = 116;
		}
		else if (Screen.width == 800 && Screen.height == 480)
		{
			banner.width = 185;
		}
		else if (Screen.width == 854 && Screen.height == 480)
		{
			banner.width = 232;
		}
		else if (Screen.width == 1024 && Screen.height == 600)
		{
			banner.width = 202;
		}
		else if (Screen.width == 1280 && Screen.height == 800)
		{
			banner.width = 156;
		}
		else
		{
			banner.width = 185;
		}

		GoogleAnalytics.Instance.LogScreen("Level Select");
		loader.SetActive(false);
		loadBanner();
		AdMob_Manager.Instance.showInterstitial();
	}

	void loadBanner()
	{
		banner.mainTexture = IAS_Manager.Instance.GetAdTexture(2,false);
	}

	public Vector2 GetAspectRatio(int x, int y){
		float f = (float)x / (float)y;
		int i = 0;
		while(true){
			i++;
			if(System.Math.Round(f * i, 2) == Mathf.RoundToInt(f * i))
				break;
		}
		return new Vector2((float)System.Math.Round(f * i, 2), i);
	}
	// Update is called once per frame
	void Update () {
	
	}

	public void onLevel1()
	{
		loader.SetActive(true);
		GameManager.currenLevel = 1;
		Application.LoadLevel("game");
	}

	public void onLevel2()
	{
		loader.SetActive(true);
		GameManager.currenLevel = 2;
		Application.LoadLevel("game");
	}

	public void onLevel3()
	{
		loader.SetActive(true);
		GameManager.currenLevel = 3;
		Application.LoadLevel("game");
	}

	public void onLevel4()
	{
		loader.SetActive(true);
		GameManager.currenLevel = 4;
		Application.LoadLevel("game");
	}


}
