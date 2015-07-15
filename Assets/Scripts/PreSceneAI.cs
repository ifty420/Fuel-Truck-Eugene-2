using UnityEngine;
using System.Collections;

public class PreSceneAI : MonoBehaviour {


	public UITexture banner1;
	public UITexture banner2;
	public UITexture banner3;

	void Start () 
	{
		banner1.mainTexture = IAS_Manager.Instance.GetAdTexture(1,true);
		banner2.mainTexture = IAS_Manager.Instance.GetAdTexture(2,true);
		banner3.mainTexture = IAS_Manager.Instance.GetAdTexture(3,true);
		Time.timeScale = 0;
		if (GameManager.instanse != null)
		{
			GameManager.instanse.rull.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void onNo()
	{
		GoogleAnalytics.Instance.LogEvent("Back Screen", "No clicks");
		if (GameManager.instanse != null)
		{
			GameManager.instanse.rull.SetActive(true);
		}
		if (GameManager.isPause == false) Time.timeScale = 1;
		GameObject.Destroy(gameObject);
	}

	public void onRate()
	{
		GoogleAnalytics.Instance.LogEvent("Back Screen", "Rate clicks");
		if (GameManager.instanse != null)
		{
			GameManager.instanse.rull.SetActive(true);
		}
		if (GameManager.isPause == false) Time.timeScale = 1;
		Application.OpenURL("https://play.google.com/store/apps/details?id=com.i6.truck_parking_fuel_truck");
		GameObject.Destroy(gameObject);
	}

	public void onYes()
	{
		GoogleAnalytics.Instance.LogEvent("Back Screen", "Yes clicks");
		if (GameManager.instanse != null)
		{
			GameManager.instanse.rull.SetActive(true);
		}
		if (GameManager.isPause == false) Time.timeScale = 1;
		Application.Quit();
	}
}
