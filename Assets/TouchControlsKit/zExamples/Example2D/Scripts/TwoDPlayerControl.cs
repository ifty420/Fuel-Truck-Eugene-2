using UnityEngine;
using TouchControlsKit;

namespace Examples
{
    public class TwoDPlayerControl : MonoBehaviour
    {
        public bool facingRight = true;

        public bool jump = false;

        public float moveForce = 365f;
        public float maxSpeed = 5f;
        public float jumpForce = 1000f;

        private Transform groundCheck = null;
        private bool grounded = false;
        private Animator anim = null;

        // Awake
        void Awake()
        {
            groundCheck = transform.Find( "groundCheck" );
            anim = GetComponent<Animator>();
        }

        // Update
        void Update()
        {
            grounded = Physics2D.Linecast( transform.position, groundCheck.position, 1 << LayerMask.NameToLayer( "Default" ) );

            if( InputManager.GetButtonDown( "jumpButton" ) && grounded )
            {
                jump = true;
            }           
        }

        // FixedUpdate
        void FixedUpdate()
        {
            float horizontal = InputManager.GetAxis( "DPad", "Horizontal" );
            horizontal = Mathf.Clamp( horizontal, -1f, 1f );

            anim.SetFloat( "Speed", Mathf.Abs( horizontal ) );

            if( horizontal * rigidbody2D.velocity.x < maxSpeed )
                rigidbody2D.AddForce( Vector2.right * horizontal * moveForce );

            if( Mathf.Abs( rigidbody2D.velocity.x ) > maxSpeed )
                rigidbody2D.velocity = new Vector2( Mathf.Sign( rigidbody2D.velocity.x ) * maxSpeed, rigidbody2D.velocity.y );

            if( horizontal > 0f && !facingRight )
                Flip();
            else if( horizontal < 0f && facingRight )
                Flip();

            if( jump )
            {
                anim.SetTrigger( "Jump" );
                rigidbody2D.AddForce( new Vector2( 0f, jumpForce * 1.5f ) );
                jump = false;
            }
        }

        // Flip
        void Flip()
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}
