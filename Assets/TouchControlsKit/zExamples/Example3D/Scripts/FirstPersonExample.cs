using UnityEngine;
using TouchControlsKit;

namespace Examples
{
    public class FirstPersonExample : MonoBehaviour
    {
        private Transform myTransform = null;
        private Transform cameraTransform = null;
        private CharacterController controller = null;
        private float rotation = 0f;


        // Use this for initialization
        void Awake()
        {
            myTransform = transform;
            cameraTransform = Camera.main.transform;
            controller = GetComponent<CharacterController>();
        }

        // Update is called once per frame
        void Update()
        {
            float moveX = InputManager.GetAxis( "Joystick", "Horizontal" );
            float moveY = InputManager.GetAxis( "Joystick", "Vertical" );
            PlayerMovement( moveX, moveY );

            float lookX = InputManager.GetAxis( "Touchpad", "Horizontal" );
            float lookY = InputManager.GetAxis( "Touchpad", "Vertical" );
            PlayerRotation( lookX, lookY );
        }

        // PlayerMovement
        private void PlayerMovement( float horizontal, float vertical )
        {
            Vector3 movement = ( myTransform.forward * vertical ) * 10f;
            movement += ( myTransform.right * horizontal ) * 10f;
            movement *= Time.deltaTime;
            movement += Physics.gravity * Time.deltaTime * 2f;
            controller.Move( movement );
        }

        // PlayerRotation
        private void PlayerRotation( float horizontal, float vertical )
        {
            myTransform.Rotate( 0f, horizontal * 12f, 0f );
            rotation += vertical * 12f;
            rotation = Mathf.Clamp( rotation, -60f, 60f );
            cameraTransform.localEulerAngles = new Vector3( -rotation, cameraTransform.localEulerAngles.y, 0f );
        }
    }
}