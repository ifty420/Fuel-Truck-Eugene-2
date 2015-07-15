using UnityEngine;
using System.Collections;

public class LevelPack : MonoBehaviour {

	public GameObject [] levels;
	public int counter = 0;
	// Use this for initialization

	public int time_for_level = 100;

	void Awake()
	{
		foreach(GameObject level in levels)
		{
			level.SetActive(false);
		}
	}

	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void generateNext()
	{
		if (levels.Length <= counter)
		{
			GameManager.instanse.onLevelComplete();
			gameObject.SetActive(false);
		}
		else
		{
			levels[counter].SetActive(true);
		}
		counter++;
	}
}
