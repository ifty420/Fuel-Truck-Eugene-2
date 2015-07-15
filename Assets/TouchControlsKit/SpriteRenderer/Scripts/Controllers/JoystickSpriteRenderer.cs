/*****************************************************
 * 													 *
 * Asset:		 Touch Controls Kit					 *
 * Script:		 JoystickSpriteRenderer.cs           *
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

namespace TouchControlsKit.SpriteRender
{
    public class JoystickSpriteRenderer : JoystickBase
    {
        public Data.ControllerDataSpriteRenderer myData = new Data.ControllerDataSpriteRenderer();
        
        public SpriteRenderer joystickSprite = null;
        public SpriteRenderer joystickBackgroundSprite = null;

        public Transform joystickTR = null;
        public Transform joystickBackgroundTR = null;


        // OnDestroy
        void OnDestroy()
        {
            TouchManagerSpriteRenderer.Obsolete();
        }

        // ControlDisable
        internal override void ControlDisable()
        {
            ShowTouchZone = false;
            joystickSprite.color = joystickBackgroundSprite.color = ElementTransparency.colorZeroAll;
        }

        // ControlAwake
        internal override void ControlAwake()
        {
            base.ControlAwake();
            SetTransparency();
            CalculationSizeAndPosition();
        }

        // CalculationSizeAndPosition
        internal override void CalculationSizeAndPosition()
        {
            myData.Anchoring();
            myData.touchzoneTransform.position = myData.basePosition;            
            joystickBackgroundTR.position = myData.touchzoneSprite.bounds.center;
            
        }

        // ShowingTouchZone
        protected override void ShowingTouchZone()
        {
            if( showTouchZone )
                myData.touchzoneSprite.color = ElementTransparency.colorHalfSprite;
            else
                myData.touchzoneSprite.color = ElementTransparency.colorZeroAll;
        }

        // SetTransparency
        private void SetTransparency()
        {
            if( isStatic || showTouchZone )
                joystickSprite.color = joystickBackgroundSprite.color = ElementTransparency.colorHalfSprite;
            else
                joystickSprite.color = joystickBackgroundSprite.color = ElementTransparency.colorZeroAll;                     
        }

        // CheckPosition
        internal override bool CheckTouchPosition( Vector2 touchPos )
        {
            return myData.CheckTouchPosition( touchPos );
        }

        // GetCurrentPosition
        protected override void GetCurrentPosition( Vector2 touchPos )
        {
            defaultPosition = currentPosition = joystickBackgroundTR.position;
            if( enableAxisX ) currentPosition.x = GuiCamera.ScreenToWorldPoint( touchPos ).x;
            if( enableAxisY ) currentPosition.y = GuiCamera.ScreenToWorldPoint( touchPos ).y;            
        }

        // CalculateBorderSize
        protected override float CalculateBorderSize()
        {
            return joystickBackgroundSprite.bounds.extents.magnitude * borderSize / 3.5f;
        }

        // UpdateJoystickPosition
        protected override void UpdateJoystickPosition()
        {
            joystickTR.position = currentPosition;
        }

        //Update Transparency And Position for Dynamic Joystick
        protected override void UpdateTransparencyAndPosition( Vector2 touchPos )
        {
            joystickSprite.color = joystickBackgroundSprite.color = ElementTransparency.colorHalfSprite;
            joystickTR.position = joystickBackgroundTR.position = GuiCamera.ScreenToWorldPoint( touchPos );
        }

        // ControlReset
        internal override void ControlReset()
        {
            base.ControlReset();
            SetTransparency();
        }
    }
}