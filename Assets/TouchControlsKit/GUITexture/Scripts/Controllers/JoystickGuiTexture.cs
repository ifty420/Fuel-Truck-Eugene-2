/*****************************************************
 * 													 *
 * Asset:		 Touch Controls Kit					 *
 * Script:		 JoystickGuiTexture.cs               *
 * 													 *
 * Copyright(c): Victor Klepikov					 *
 * Support: 	 http://bit.ly/vk-Support			 *
 * 													 *
 * mySite:       http://vkdemos.ucoz.org			 *
 * myAssets:     http://u3d.as/5Fb                   *
 * myTwitter:	 http://twitter.com/VictorKlepikov	 *
 * myFacebook:	 http://www.facebook.com/vikle4 	 *
 * 													 *
 ****************************************************/


using UnityEngine;
using TouchControlsKit.Utils;

namespace TouchControlsKit.GuiTexture
{
    [RequireComponent( typeof( GUITexture ) )]
    public class JoystickGuiTexture : JoystickBase
    {
        public Data.ControllerDataGuiTexture myData = new Data.ControllerDataGuiTexture();

        public GUITexture joystickGUITexture = null;
        public GUITexture backgroundGUITexture = null;

        [SerializeField]
        private float joystickSize = 4.6f;

        private Vector2 calculatedJoystickPosition = Vector2.zero;
        private float calculatedJoystickSize = 0f;


        // JoystickSize
        public float JoystickSize
        {
            get { return joystickSize; }
            set
            {
                if( joystickSize == value ) return;
                joystickSize = value;
                TouchManagerGuiTexture.Obsolete();
            }
        }


        // OnDestroy
        void OnDestroy()
        {
            TouchManagerGuiTexture.Obsolete();
        }

        // ControlDisable
        internal override void ControlDisable()
        {
            ShowTouchZone = false;
            joystickGUITexture.color = backgroundGUITexture.color = ElementTransparency.colorZeroAll;
        }

        // ControlAwake
        internal override void ControlAwake()
        {
            base.ControlAwake();
            myData.GetTouchzoneGUITexture( gameObject );
            CalculationSizeAndPosition();
            SetTransparency();
        }        

        // ShowingTouchZone
        protected override void ShowingTouchZone()
        {
            if( showTouchZone )
                myData.touchzoneGUITexture.color = ElementTransparency.colorHalfGuiTexture;
            else
                myData.touchzoneGUITexture.color = ElementTransparency.colorZeroAll;
        }

        // SetTransparency
        private void SetTransparency()
        {
            if( isStatic || showTouchZone )
                joystickGUITexture.color = backgroundGUITexture.color = ElementTransparency.colorHalfGuiTexture;            
            else
                joystickGUITexture.color = backgroundGUITexture.color = ElementTransparency.colorZeroAll;             
        }

        // CalculationSizeAndPosition
        internal override void CalculationSizeAndPosition()
        {
            calculatedJoystickSize = joystickSize / 50f * Screen.width;

            myData.Anchoring( myData.calcWidth, myData.calcHeight );
            myData.touchzoneGUITexture.pixelInset = myData.UpdateRect( myData.basePosition, myData.calcWidth, myData.calcHeight );

            calculatedJoystickPosition.x = myData.basePosition.x + myData.calcWidth / 2f - calculatedJoystickSize / 2f;
            calculatedJoystickPosition.y = myData.basePosition.y + myData.calcHeight / 2f - calculatedJoystickSize / 2f;
            joystickGUITexture.pixelInset = myData.UpdateRect( calculatedJoystickPosition, calculatedJoystickSize, calculatedJoystickSize );
            backgroundGUITexture.pixelInset = myData.UpdateRect( calculatedJoystickPosition, calculatedJoystickSize, calculatedJoystickSize );

            defaultPosition = currentPosition = calculatedJoystickPosition;
        }

        // CheckPosition
        internal override bool CheckTouchPosition( Vector2 touchPos )
        {
            return myData.CheckTouchPosition( touchPos, myData.calcWidth, myData.calcHeight );
        }

        // GetCurrentPosition
        protected override void GetCurrentPosition( Vector2 touchPos )
        {
            if( enableAxisX ) currentPosition.x = touchPos.x - calculatedJoystickSize / 2f;
            if( enableAxisY ) currentPosition.y = touchPos.y - calculatedJoystickSize / 2f;
        }

        // CalculateBorderSize
        protected override float CalculateBorderSize()
        {
            return calculatedJoystickSize * borderSize / 5f;
        }

        // UpdateJoystickPosition
        protected override void UpdateJoystickPosition()
        {
            joystickGUITexture.pixelInset = myData.UpdateRect( currentPosition, calculatedJoystickSize, calculatedJoystickSize );
        }

        // Update Transparency And Position for Dynamic Joystick
        protected override void UpdateTransparencyAndPosition( Vector2 touchPos )
        {
            joystickGUITexture.color = backgroundGUITexture.color = ElementTransparency.colorHalfGuiTexture;
            defaultPosition.x = touchPos.x - calculatedJoystickSize / 2f;
            defaultPosition.y = touchPos.y - calculatedJoystickSize / 2f;
            currentPosition = defaultPosition;
            joystickGUITexture.pixelInset = backgroundGUITexture.pixelInset = myData.UpdateRect( currentPosition, calculatedJoystickSize, calculatedJoystickSize ); 
        }

        // ControlReset
        internal override void ControlReset()
        {
            base.ControlReset();
            SetTransparency();
        }
    }
}