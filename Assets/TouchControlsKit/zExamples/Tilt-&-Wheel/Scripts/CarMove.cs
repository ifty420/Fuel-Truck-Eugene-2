using UnityEngine;
using TouchControlsKit;


public class CarMove : MonoBehaviour
{
    CharacterController controller;

    // Awake
    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update
    void Update()
    {
        float horizontal = InputManager.GetAxis( "steeringWheel", "Horizontal" ) + Input.GetAxis( "Horizontal" );
        float vertical = InputManager.GetAxis( "moveJoystick", "Vertical" ) + Input.GetAxis( "Vertical" );

        if( vertical != 0f )
            transform.Rotate( 0f, horizontal, 0f );

        Vector3 movement = ( transform.forward * vertical ) * 5f;
        movement *= Time.deltaTime;
        controller.Move( movement );
    }
}
