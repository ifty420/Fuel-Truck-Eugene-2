using UnityEngine;
using TouchControlsKit;

public class BallMove : MonoBehaviour
{
    private Rigidbody myRigidbody = null;
    private Vector3 tilt = Vector3.zero;
    private Transform myTransform = null;
    private Transform cameraTransform = null;


    // Awake
    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myRigidbody.maxAngularVelocity = 25f;
        myTransform = GetComponent<Transform>();
        cameraTransform = Camera.main.GetComponent<Transform>();
    }

    // Update
    void Update()
    {
        tilt.x = Tilt.forwardAxis + InputManager.GetAxis( "dPad", "Horizontal" );
        tilt.z = -Tilt.sidewaysAxis + InputManager.GetAxis( "dPad", "Vertical" );

        cameraTransform.position = new Vector3( myTransform.position.x, cameraTransform.position.y, myTransform.position.z - 5f );

    }

    // FixedUpdate
    void FixedUpdate()
    {
        myRigidbody.AddForce( tilt * 5f * Time.fixedDeltaTime, ForceMode.Impulse );
    }
}
