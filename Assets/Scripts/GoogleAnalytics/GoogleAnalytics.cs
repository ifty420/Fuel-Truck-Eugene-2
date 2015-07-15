using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GoogleAnalytics : MonoBehaviour
{
	
	public string AppName = "App Name Here";
	public string PropertyID = "UA-000000000";
	public string BundleID = "com.i6.AppNameHere";
	public string AppVersion = "1.00";
	
	public static GoogleAnalytics Instance;
	
	private string screenResolution;
	private string clientID;
	
	void Awake()
	{
		if(!Instance)
			Instance = this;
	}
	
	void Start()
	{
		// Get the device resolution
		screenResolution = Screen.width + "x" + Screen.height;
		
		// Get a unique identifier for the device http://docs.unity3d.com/Documentation/ScriptReference/SystemInfo-deviceUniqueIdentifier.html
		clientID = WWW.EscapeURL(SystemInfo.deviceUniqueIdentifier);
		
		// HTMLEscape our variables so it doesn't break the URL request
		AppName = WWW.EscapeURL(AppName);
		PropertyID = WWW.EscapeURL(PropertyID);
		BundleID = WWW.EscapeURL(BundleID);
		AppVersion = WWW.EscapeURL(AppVersion);
		
		// Always log the initial Analytics start as "Start"
		LogScreen("Start");
	}
	
	public void LogScreen(string title)
	{
		// Get the htmlchars escaped title of the screen so it doesn't break the URL request
		title = WWW.EscapeURL(title);
		
		// URL which will be pinged to log the requested screen and include details about the user
		var url = "http://www.google-analytics.com/collect?v=1&ul=6en-us&t=appview&sr="+screenResolution+"&an="+AppName+"&tid="+PropertyID+"&aid="+BundleID+"&cid="+clientID+"&_u=.sB&av="+AppVersion+"&_v=ma1b3&cd="+title+"&qt=2500&z=185";
		
		// Process the URL
		StartCoroutine(Process(new WWW(url)));
	}
	
	public void LogEvent(string titleCat, string titleAction)	
	{
		// Get the htmlchars escaped category and action of the event so it doesn't break the URL request
		titleCat = WWW.EscapeURL(titleCat);
		titleAction = WWW.EscapeURL(titleAction);
		
		// URL which will be pinged to log the event and include details about the user
		var url = "http://www.google-analytics.com/collect?v=1&ul=en-us&t=event&sr="+screenResolution+"&an="+AppName+"&tid="+PropertyID+"&aid="+BundleID+"&cid="+clientID+"&_u=.sB&av="+AppVersion+"&_v=ma1b3&ec="+titleCat+"&ea="+titleAction+"&qt=2500&z=185";
		
		// Process the URL
		StartCoroutine(Process(new WWW(url)));
	}
	
	public void LogError(string description, bool isFatal)
	{
		// Get the htmlchars escaped description so it doesn't break the URL request
		description = WWW.EscapeURL(description);
		
		int fatal = (isFatal ? 1 : 0);
		
		// URL which will be pinged to log the requested screen and include details about the user
		var url = "http://www.google-analytics.com/collect?v=1&ul=en-us&t=exception&sr="+screenResolution+"&an="+AppName+"&tid="+PropertyID+"&aid="+BundleID+"&cid="+clientID+"&exd="+description+"&exf="+fatal+"&qt=2500&z=185";
		
		// Process the URL
		StartCoroutine(Process(new WWW(url)));
	}
	
	private IEnumerator Process(WWW www)
	{
		// Wait for the URL to be processed
		yield return www;
		
		// Cleanup the request data
		www.Dispose();
	}
	
}