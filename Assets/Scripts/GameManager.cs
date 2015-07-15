using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {


	public static GameManager instanse;

	public static bool isPause = false;

	public LevelPack [] levelPacks;

	public static int currenLevel = 7;

	public int time = 0;

	public int [] times_for_level;

	private LevelPack CurrentLevelPack;

	public GameObject Truck;

	public GameObject levelComplete;
	public GameObject levelFailed;
	public Vector3 [] truckPositions;
	public Vector3 [] truckAngles;

	public GameObject progress;

	public GameObject pause;

	public UISprite pauseBtnBack;

	public UISprite resumeBtnBack;

	public UISprite restartBtnBack;

	public UISprite menuBtnBack;

	public UISprite cameraBtnBack;

	public UILabel time_label;

	private int currentCamera = 1;

	public CarCameras car_camera;

	public UILabel bar_label;

	public GameObject rull;

	public GameObject loader;

	public UITexture banner1;
	public UITexture banner2;

	void Awake()
	{
		instanse = (GameManager)gameObject.GetComponent("GameManager");
		Truck.transform.position = truckPositions[currenLevel-1];
		Truck.transform.eulerAngles = truckAngles[currenLevel-1];
		isPause = false;
	}
	// Use this for initialization
	void Start () 
	{
		GoogleAnalytics.Instance.LogScreen("Level "+currenLevel.ToString());

		loader.SetActive(false);
		levelComplete.SetActive(false);
		levelFailed.SetActive(false);
		foreach(LevelPack l in levelPacks)
		{
			l.gameObject.SetActive(false);
		}
		CurrentLevelPack = levelPacks[currenLevel-1];
		CurrentLevelPack.gameObject.SetActive(true);
		checkLevel();
		pause.SetActive(false);
		//AdMob_Manager.Instance.hideBanner();

		time = times_for_level[currenLevel-1];
		tickerTick();
		AdMob_Manager.Instance.loadInterstitial(false);
	}


	void tickerTick()
	{
		time--;
		if (time < 0)
		{
			time = 0;
			onLevelFailed();
		}
		else
		{
			Invoke("tickerTick",1);
		}
		string t = FloatToTime(float.Parse(time.ToString()),"#00:00");
		time_label.text = t;
		Debug.Log(t);
	}

	void onLevelFailed()
	{
		GoogleAnalytics.Instance.LogScreen("Level "+currenLevel.ToString()+ " failed");
		banner2.mainTexture = IAS_Manager.Instance.GetAdTexture(4,false);
		rull.SetActive(false);
		Time.timeScale = 0;
		levelFailed.SetActive(true);
		AdMob_Manager.Instance.showInterstitial();
	}

	private void switchCamera()
	{
		currentCamera++;
		if (currentCamera > 3) currentCamera = 1;

		if (currentCamera == 1)
		{
			car_camera.distance = 14.6f;
			car_camera.height = 12f;
			car_camera.yawAngle = 0;
			car_camera.pitchAngle = -8.6f;
		}
		if (currentCamera == 2)
		{
			car_camera.distance = 14.6f;
			car_camera.height = 12f;
			car_camera.yawAngle = -180;
			car_camera.pitchAngle = -8.6f;
		}
		if (currentCamera == 3)
		{
			car_camera.distance = 4.05f;
			car_camera.height = 6.44f;
			car_camera.yawAngle = 0;
			car_camera.pitchAngle = -1.49f;
		}
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void onCamera()
	{
		cameraBtnBack.spriteName = "camera_pressed";
	}

	public void onCameraPressed()
	{
		cameraBtnBack.spriteName = "camera_na";
		switchCamera();
	}

	public void onPause()
	{
		isPause = true;
		//AdMob_Manager.Instance.showBanner();
		pauseBtnBack.spriteName = "pause_na";
		pause.SetActive(true);
		Time.timeScale = 0;
	}




	public void onPausePressed()
	{
		pauseBtnBack.spriteName = "pause_pressed";

	}

	public void onResume()
	{
		resumeBtnBack.spriteName = "resume_Pressed";
	}

	public void onResumePressed()
	{
		isPause = false;
		resumeBtnBack.spriteName = "resume_na";
		Time.timeScale = 1;
		pause.SetActive(false);
		//AdMob_Manager.Instance.hideBanner();
	}

	public void onMap()
	{
		Time.timeScale = 1;
		Application.LoadLevel("map");
	}

	public void onMenu()
	{
		menuBtnBack.spriteName = "mainmenu_pressed";
	}

	public void onMenuPressed()
	{
		menuBtnBack.spriteName = "mainmenu_na";
		Time.timeScale = 1;
		Application.LoadLevel("menu");
	}

	public void onRestart2()
	{
		progress.SetActive(false);
		levelComplete.SetActive(false);
		levelFailed.SetActive(false);
		loader.SetActive(true);
		Time.timeScale = 1;
		Application.LoadLevel("game");
	}

	public void onRestart()
	{
		restartBtnBack.spriteName = "Restart_Pressed";
	}

	public void onRestartPressed()
	{
		progress.SetActive(false);
		levelComplete.SetActive(false);
		levelFailed.SetActive(false);
		loader.SetActive(true);
		restartBtnBack.spriteName = "Restart_na";
		Time.timeScale = 1;
		Application.LoadLevel("game");
	}

	public void onNext()
	{
		progress.SetActive(false);
		levelComplete.SetActive(false);
		levelFailed.SetActive(false);
		loader.SetActive(true);
		Time.timeScale = 1;
		currenLevel++;
		if (currenLevel > 20) currenLevel = 20;
		Application.LoadLevel("game");
	}

	public void checkLevel()
	{
		CurrentLevelPack.generateNext();

	}

	public void onLevelComplete()
	{
		GoogleAnalytics.Instance.LogScreen("Level "+currenLevel.ToString()+ " complete");
		banner1.mainTexture = IAS_Manager.Instance.GetAdTexture(3,false);
		rull.SetActive(false);
		int lvl = PlayerPrefs.GetInt("levels");
		if (lvl == currenLevel)
		{
			lvl++;
			PlayerPrefs.SetInt("levels",lvl);
			PlayerPrefs.Save();
		}
		CancelInvoke();
		Debug.Log("onLevelComplete");
		levelComplete.SetActive(true);
		Time.timeScale = 0;
		AdMob_Manager.Instance.showInterstitial();
		AdMob_Manager.Instance.showBanner();
	}

	public static string FloatToTime (float toConvert, string format){
		switch (format){
		case "00.0":
			return string.Format("{0:00}:{1:0}", 
			                     Mathf.Floor(toConvert) % 60,//seconds
			                     Mathf.Floor((toConvert*10) % 10));//miliseconds
			break;
		case "#0.0":
			return string.Format("{0:#0}:{1:0}", 
			                     Mathf.Floor(toConvert) % 60,//seconds
			                     Mathf.Floor((toConvert*10) % 10));//miliseconds
			break;
		case "00.00":
			return string.Format("{0:00}:{1:00}", 
			                     Mathf.Floor(toConvert) % 60,//seconds
			                     Mathf.Floor((toConvert*100) % 100));//miliseconds
			break;
		case "00.000":
			return string.Format("{0:00}:{1:000}", 
			                     Mathf.Floor(toConvert) % 60,//seconds
			                     Mathf.Floor((toConvert*1000) % 1000));//miliseconds
			break;
		case "#00.000":
			return string.Format("{0:#00}:{1:000}", 
			                     Mathf.Floor(toConvert) % 60,//seconds
			                     Mathf.Floor((toConvert*1000) % 1000));//miliseconds
			break;
		case "#0:00":
			return string.Format("{0:#0}:{1:00}",
			                     Mathf.Floor(toConvert / 60),//minutes
			                     Mathf.Floor(toConvert) % 60);//seconds
			break;
		case "#00:00":
			return string.Format("{0:#00}:{1:00}", 
			                     Mathf.Floor(toConvert / 60),//minutes
			                     Mathf.Floor(toConvert) % 60);//seconds
			break;
		case "0:00.0":
			return string.Format("{0:0}:{1:00}.{2:0}",
			                     Mathf.Floor(toConvert / 60),//minutes
			                     Mathf.Floor(toConvert) % 60,//seconds
			                     Mathf.Floor((toConvert*10) % 10));//miliseconds
			break;
		case "#0:00.0":
			return string.Format("{0:#0}:{1:00}.{2:0}",
			                     Mathf.Floor(toConvert / 60),//minutes
			                     Mathf.Floor(toConvert) % 60,//seconds
			                     Mathf.Floor((toConvert*10) % 10));//miliseconds
			break;
		case "0:00.00":
			return string.Format("{0:0}:{1:00}.{2:00}",
			                     Mathf.Floor(toConvert / 60),//minutes
			                     Mathf.Floor(toConvert) % 60,//seconds
			                     Mathf.Floor((toConvert*100) % 100));//miliseconds
			break;
		case "#0:00.00":
			return string.Format("{0:#0}:{1:00}.{2:00}",
			                     Mathf.Floor(toConvert / 60),//minutes
			                     Mathf.Floor(toConvert) % 60,//seconds
			                     Mathf.Floor((toConvert*100) % 100));//miliseconds
			break;
		case "0:00.000":
			return string.Format("{0:0}:{1:00}.{2:000}",
			                     Mathf.Floor(toConvert / 60),//minutes
			                     Mathf.Floor(toConvert) % 60,//seconds
			                     Mathf.Floor((toConvert*1000) % 1000));//miliseconds
			break;
		case "#0:00.000":
			return string.Format("{0:#0}:{1:00}.{2:000}",
			                     Mathf.Floor(toConvert / 60),//minutes
			                     Mathf.Floor(toConvert) % 60,//seconds
			                     Mathf.Floor((toConvert*1000) % 1000));//miliseconds
			break;
		}
		return "error";
	}

}
