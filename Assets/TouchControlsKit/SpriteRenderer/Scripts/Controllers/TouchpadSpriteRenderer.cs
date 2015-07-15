/*****************************************************
 * 													 *
 * Asset:		 Touch Controls Kit					 *
 * Script:		 TouchpadSpriteRenderer.cs           *
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
    public class TouchpadSpriteRenderer : TouchpadBase
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
            ShowTouchZone = false;
        }

        // ControlAwake
        internal override void ControlAwake()
        {
            base.ControlAwake();
            myData.SetTouchzoneByGameObject( gameObject );
            CalculationSizeAndPosition();
        }

        // ShowingTouchZone
        protected override void ShowingTouchZone()
        {
            if( showTouchZone )
                myData.touchzoneSprite.color = ElementTransparency.colorHalfSprite;
            else
                myData.touchzoneSprite.color = ElementTransparency.colorZeroAll;
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
    }
}