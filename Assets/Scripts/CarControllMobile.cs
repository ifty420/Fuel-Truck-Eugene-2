using UnityEngine;
using System.Collections;

public class CarControllMobile : MonoBehaviour {

	public Transform wheel;

	private float lastRot = 0;

	public static float allRot = 0;

	public UISprite upBack;

	public UISprite breakBack;

	private bool isMoved = false;
	

	void Start () 
	{
		allRot = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isMoved)
		{
			AxisCarController.handBrake = 0;
		}
		else
		{
			AxisCarController.handBrake = 1;
		}

		if (allRot < -720) allRot = -720;
		if (allRot > 720) allRot = 720;
		Debug.Log(allRot);

		if (allRot > 0)
		{
			AxisCarController.moveInput = allRot/720f/0.65f;
		}
		else
		{
			AxisCarController.moveInput = allRot/720f/0.65f;
		}

	}

	public void onLeftDown()
	{
		AxisCarController.moveInput = -1;
	}

	public void onLeftUp()
	{
		AxisCarController.moveInput = 0;
	}

	public void onRightDown()
	{
		AxisCarController.moveInput = 1;
	}

	public void onRightUp()
	{
		AxisCarController.moveInput = 0;
	}

	public void onBackDown()
	{
		isMoved = true;
		breakBack.spriteName = "brake_pressed";
		AxisCarController.brake = 1;
	}

	public void onBackUp()
	{
		isMoved = true;
		breakBack.spriteName = "brake_na";
		AxisCarController.brake = 0;
	}

	public void onForwardDown()
	{
		isMoved = true;
		upBack.spriteName = "acselerate_Pressed";
		AxisCarController.up = 1;
	}

	public void onForwardUp()
	{
		isMoved = true;
		upBack.spriteName = "acselerate_na";
		AxisCarController.up = 0;
	}
}
