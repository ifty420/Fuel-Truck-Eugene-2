using UnityEngine;
using System.Collections;

public class ServiceAI : MonoBehaviour {

	void Awake()
	{
		DontDestroyOnLoad(transform);
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyUp(KeyCode.Escape))
		{
			if (GameObject.Find("pre(Clone)") == null) 
			{
				GameObject pre = (GameObject)Instantiate(Resources.Load("pre"));
				if (GameManager.instanse != null)
				{
					GameManager.instanse.rull.SetActive(false);
				}
			}
			else
			{
				if (GameManager.isPause == false) Time.timeScale = 1;
				GameObject.Destroy(GameObject.Find("pre(Clone)"));
				if (GameManager.instanse != null)
				{
					GameManager.instanse.rull.SetActive(true);
				}
			}
		}
	}
}
