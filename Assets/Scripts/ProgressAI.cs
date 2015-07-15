using UnityEngine;
using System.Collections;

public class ProgressAI : MonoBehaviour {

	public UILabel progress_lb;
	public UIProgressBar bak;

	public float valuePR = 0;

	public static ProgressAI instance; 

	void Awake()
	{
		instance = (ProgressAI)gameObject.GetComponent("ProgressAI");
	}

	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		bak.value = valuePR;
		progress_lb.text = Mathf.FloorToInt(valuePR*100).ToString()+"%";
	}
}
