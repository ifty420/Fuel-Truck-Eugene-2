/*****************************************************
 * 													 *
 * Asset:		 Touch Controls Kit					 *
 * Script:		 SteeringWheelSpriteRenderer.cs      *
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
    [RequireComponent( typeof( SpriteRenderer ) )]
    public class SteeringWheelSpriteRenderer : SteeringWheelBase
    {
        public Data.ControllerDataSpriteRenderer myData = new Data.ControllerDataSpriteRenderer();


        // OnDestroy
        void OnDestroy()
        {
            TouchManagerSpriteRenderer.Obsolete();
        }

        // ControlDisable
        internal override void ControlDisable()
        {
            myData.touchzoneSprite.color = ElementTransparency.colorZeroAll;
        }

        // ControlAwake
        internal override void ControlAwake()
        {
            base.ControlAwake();
            myData.SetTouchzoneByGameObject( gameObject );
            myData.touchzoneSprite.color = ElementTransparency.colorHalfSprite;
            CalculationSizeAndPosition();
        }

        // CalculationSizeAndPosition
        internal override void CalculationSizeAndPosition()
        {
            myData.Anchoring();
            myData.touchzoneTransform.position = myData.basePosition;
        }

        // CheckTouchPosition
        internal override bool CheckTouchPosition( Vector2 touchPos )
        {
            return myData.CheckTouchPosition( touchPos );
        }

        // GetCurrentPosition
        protected override void GetCurrentPosition( Vector2 touchPos )
        {
            defaultPosition = currentPosition = myData.touchzoneTransform.position;
            currentPosition.x = GuiCamera.ScreenToWorldPoint( touchPos ).x;
            currentPosition.y = GuiCamera.ScreenToWorldPoint( touchPos ).y;
        }

        // UptateWheelRotation
        protected override void UptateWheelRotation()
        {
			if (GameManager.isPause == false)
			{
				base.UptateWheelRotation();
				myData.touchzoneTransform.localEulerAngles = localEulerAngles;
			}
           
        }
    }
}