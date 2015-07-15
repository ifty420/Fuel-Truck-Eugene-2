/*****************************************************
 * 													 *
 * Asset:		 Touch Controls Kit					 *
 * Script:		 ButtonGuiTexture.cs               	 *
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
    public class ButtonGuiTexture : ButtonBase
    {
        public Data.ControllerDataGuiTexture myData = new Data.ControllerDataGuiTexture();

        [SerializeField]
        private Texture2D normaltexture = null;
        public Texture2D pressedTexture = null; 


        public Texture2D normalTexture
        {
            get { return normaltexture; }
            set
            {
                if( normaltexture == value ) return;
                normaltexture = value;
                ControlAwake();
                TouchManagerGuiTexture.Obsolete();
                myData.touchzoneGUITexture.texture = normaltexture;
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
            myData.touchzoneGUITexture.color = ElementTransparency.colorZeroAll;
        }

        // ControlAwake
        internal override void ControlAwake()
        {
            base.ControlAwake();
            myData.GetTouchzoneGUITexture( gameObject );
            myData.touchzoneGUITexture.texture = normalTexture;
            CalculationSizeAndPosition();
            myData.touchzoneGUITexture.color = ElementTransparency.colorHalfGuiTexture;
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

        // ButtonDown
        protected override void ButtonDown()
        {
            myData.touchzoneGUITexture.texture = pressedTexture;
        }

        // ButtonUp
        protected override void ButtonUp()
        {
            myData.touchzoneGUITexture.texture = normalTexture;
        }
    }
}