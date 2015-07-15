using UnityEngine;
using System.Collections;

public class MenuAI : MonoBehaviour {

	public UITexture iad_banner;

	public static MenuAI instanse;

	void Awake()
	{
		instanse = (MenuAI)gameObject.GetComponent("MenuAI");
		//PlayerPrefs.DeleteAll();
		if (PlayerPrefs.GetInt("levels")==0)
		{
			PlayerPrefs.SetInt("levels",1);
			PlayerPrefs.Save();
		}
	}
	// Use this for initialization
	void Start () 
	{
		GoogleAnalytics.Instance.LogScreen("Main Menu");
		AdMob_Manager.Instance.loadInterstitial(false);
	}

	public void loadBanner()
	{
		iad_banner.mainTexture = IAS_Manager.Instance.GetAdTexture(1,false);
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void startGame()
	{
		AdMob_Manager.Instance.showInterstitial();
		//Invoke("loadLevel",1);
		loadLevel();
	}

	void loadLevel()
	{
		Application.LoadLevel("map");
	}

	public void onMoreGames()
	{
		Application.OpenURL("https://play.google.com/store/apps/developer?id=i6+Games");
	}
}
