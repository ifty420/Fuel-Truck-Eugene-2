using UnityEngine;
using System.Collections;

public class MapBtnAI : MonoBehaviour {

	public UISprite back;
	public UISprite locked;
	public UILabel numbers;

	private int currentLevel;

	private int thisLevel;

	private bool isLocked = false;

	// Use this for initialization
	void Start ()
	{
		currentLevel = PlayerPrefs.GetInt("levels");
		thisLevel = int.Parse(numbers.text);
		if (thisLevel > currentLevel)
		{
			isLocked = true;
			numbers.gameObject.SetActive(false);
		}
		else
		{
			isLocked = false;
			locked.gameObject.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void onPlayLevelPressed()
	{
		back.spriteName = "levelna";
		if (isLocked)
		{

		}
		else
		{
			MapAI.instance.loader.SetActive(true);
			GameManager.currenLevel = int.Parse(numbers.text);
			Application.LoadLevel("game");
		}
	}

	public void onPlayLevel()
	{
		if (!isLocked)
		{
			back.spriteName = "levelpressed";
		}
		else
		{
			back.spriteName = "levelpressed_WHEN_loked";
		}
	}
}
