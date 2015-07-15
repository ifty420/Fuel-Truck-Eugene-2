/*****************************************************
 * 													 *
 * Asset:		 Touch Controls Kit					 *
 * Script:		 TouchpadGuiTexture.cs               *
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
    public class TouchpadGuiTexture : TouchpadBase
    {
        public Data.ControllerDataGuiTexture myData = new Data.ControllerDataGuiTexture();


        // OnDestroy
        void OnDestroy()
        {
            TouchManagerGuiTexture.Obsolete();
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
            myData.GetTouchzoneGUITexture( gameObject );
            CalculationSizeAndPosition();
        }        

        // ShowingTouchZone
        protected override void ShowingTouchZone()
        {
            if( showTouchZone )
                myData.touchzoneGUITexture.color = ElementTransparency.colorHalfGuiTexture;
            else
                myData.touchzoneGUITexture.color = ElementTransparency.colorZeroAll;
        }

        // CalculationSizeAndPosition
        internal override void CalculationSizeAndPosition()
        {
            myData.Anchoring( myData.calcWidth, myData.calcHeight );
            myData.touchzoneGUITexture.pixelInset = myData.UpdateRect( myData.basePosition, myData.calcWidth, myData.calcHeight );
        }

        // CheckTouchPosition
        internal override bool CheckTouchPosition( Vector2 touchPos )
        {
            return myData.CheckTouchPosition( touchPos, myData.calcWidth, myData.calcHeight );
        }
    }
}