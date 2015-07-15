using UnityEngine;

[AddComponentMenu("NGUI/Examples/Spin With Mouse")]
public class SpinWithMouse : MonoBehaviour
{
	public Transform target;
	public float speed = 1f;
	Vector3 currentLoc;
	Transform mTrans;
	Vector3 startPoint;

	void Start ()
	{
		mTrans = transform;
	}

	void OnPress()
	{
		//Get the distance of x,y,z from the center 
		currentLoc = Input.mousePosition - new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
	}

	void OnDrag (Vector2 delta)
	{
		Vector3 newLoc = Input.mousePosition; 
		//Get the distance of x,y,z from the center 
		float angle = Vector3.Angle(currentLoc,newLoc); 
		mTrans.transform.Rotate(Vector3.forward, angle); 
		Debug.Log("Wheel location " + mTrans.localPosition); 
		Debug.Log("Clicked location " + currentLoc); 
		Debug.Log("Dragged location " + newLoc);
	}
}