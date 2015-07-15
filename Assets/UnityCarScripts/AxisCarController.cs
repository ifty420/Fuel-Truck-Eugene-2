//========================================================================================================================
// UnityCar 2.1 Pro Vehicle Physics - (c) Michele Di Lena
// http://www.unitypackages.net/unitycar
//
// Any product developed using this version of UnityCar requires clearly readable UnityCar logo on splash screen or credits screen.
// See README.txt for more info.
//========================================================================================================================

using UnityEngine;

public class AxisCarController : CarController {
		
	public string throttleAxis="Throttle";
	public string brakeAxis="Brake";
	public string steerAxis="Horizontal";
	public string handbrakeAxis="Handbrake";
	public string clutchAxis="Clutch";
	public string shiftUpButton="ShiftUp";
	public string shiftDownButton="ShiftDown";
	public string startEngineButton="StartEngine";

	public static float moveInput = 0;
	public static float brake = 0;
	public static float up = 0;

	public static float handBrake = 0;
		
	protected override void GetInput(out float throttleInput, 
									out float brakeInput, 
									out float steerInput, 
									out float handbrakeInput,
									out float clutchInput,
									out bool startEngineInput,
									out int targetGear){

	
		//throttleInput= Input.GetAxisRaw (throttleAxis);
		//brakeInput = Input.GetAxisRaw (brakeAxis);
		//steerInput = Input.GetAxisRaw (steerAxis);
		//handbrakeInput=Input.GetAxisRaw (handbrakeAxis);
		throttleInput= up;
		brakeInput = brake;
		steerInput = moveInput;
		handbrakeInput=handBrake;
		clutchInput =Input.GetAxisRaw (clutchAxis);
		startEngineInput=Input.GetButton (startEngineButton);
		
		
		// Gear shift
		targetGear = drivetrain.gear;
		if(Input.GetButtonDown(shiftUpButton)){
			++targetGear;
		}
		if(Input.GetButtonDown(shiftDownButton)){
			--targetGear;
		}

		if (drivetrain.shifter==true){
			if(Input.GetButton("reverse")){
				targetGear=0;
			}
			
			else if(Input.GetButton("neutral")){
				targetGear=1;
			}
			
			else if(Input.GetButton("first")){
				targetGear=2;
			}
			
			else if(Input.GetButton("second")){
				targetGear=3;
			}
			
			else if(Input.GetButton("third")){
				targetGear=4;
			}
			
			else if(Input.GetButton("fourth")){
				targetGear=5;
			}
			
			else if(Input.GetButton("fifth")){
				targetGear=6;
			}
			
			else if(Input.GetButton("sixth")){
				targetGear=7;
			}
			
			else {
				targetGear=1;
			}
		}		
	}
}
