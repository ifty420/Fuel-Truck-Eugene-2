/*****************************************************
 * 													 *
 * Asset:		 Touch Controls Kit					 *
 * Script:		 DPadArrowGuiTexture.cs              *
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
    public class DPadArrowGuiTexture : DPadArrowBase
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
                DPadArrowAwake();
                TouchManagerGuiTexture.Obsolete();
                myData.touchzoneGUITexture.texture = normaltexture;
            }
        }

        // OnDestroy
        void OnDestroy()
        {
            TouchManagerGuiTexture.Obsolete();
        }

        // DPadArrowADisable
        internal override void DPadArrowADisable()
        {
            myData.touchzoneGUITexture.color = ElementTransparency.colorZeroAll;
        }

        // DPadArrowAwake
        internal override void DPadArrowAwake()
        {
            myData.GetTouchzoneGUITexture( gameObject );
            myData.touchzoneGUITexture.color = ElementTransparency.colorHalfGuiTexture;
        }

        // CalculationSizeAndPosition
        internal void CalculationSizeAndPosition( Vector2 pos, float sizeX, float sizeY )
        {
            int screenWidth = Screen.width;

            myData.calcWidth = myData.ImageWidth / 50f * screenWidth;
            myData.calcHeight = myData.ImageHeight / 50f * screenWidth;

            float calcX = myData.OffsetX * screenWidth / 100f;
            float calcY = myData.OffsetY * screenWidth / 100f;

            switch( ArrowType )
            {
                case ArrowTypes.UP:
                    myData.basePosition.x = pos.x - myData.calcWidth / 2f + calcX;
                    myData.basePosition.y = pos.y + sizeY - myData.calcHeight - calcY;
                    break;

                case ArrowTypes.DOWN:
                    myData.basePosition.x = pos.x - myData.calcWidth / 2f + calcX;
                    myData.basePosition.y = pos.y - sizeY + calcY;
                    break;

                case ArrowTypes.RIGHT:
                    myData.basePosition.x = pos.x + sizeX - myData.calcWidth - calcX;
                    myData.basePosition.y = pos.y - myData.calcHeight / 2f + calcY;
                    break;

                case ArrowTypes.LEFT:
                    myData.basePosition.x = pos.x - sizeX + calcX;
                    myData.basePosition.y = pos.y - myData.calcHeight / 2f + calcY;
                    break;
            }                        
            myData.touchzoneGUITexture.pixelInset = myData.UpdateRect( myData.basePosition, myData.calcWidth, myData.calcHeight );
        }

        // CheckBoolPosition
        protected override bool CheckBoolPosition( Vector2 touchPos, float sizeX, float sizeY )
        {
            switch( ArrowType )
            {
                case ArrowTypes.UP:
                case ArrowTypes.DOWN:
                    if( touchPos.x < myData.basePosition.x + myData.calcWidth + sizeX
                    && touchPos.y < myData.basePosition.y + myData.calcHeight
                    && touchPos.x > myData.basePosition.x - sizeX 
                    && touchPos.y > myData.basePosition.y )
                    {
                        return true;
                    }
                    
                    break;

                case ArrowTypes.RIGHT:
                case ArrowTypes.LEFT:
                    if( touchPos.x < myData.basePosition.x + myData.calcWidth
                    && touchPos.y < myData.basePosition.y + myData.calcHeight + sizeY
                    && touchPos.x > myData.basePosition.x
                    && touchPos.y > myData.basePosition.y - sizeY )
                    {
                        return true;
                    }
                    
                    break;
            }
            return false;
        }

        // SetPressedTexture
        internal void SetPressedTexture()
        {
            if( myData.touchzoneGUITexture.texture != pressedTexture )
                myData.touchzoneGUITexture.texture = pressedTexture;
        }

        // SetNormalTexture
        internal void SetNormalTexture()
        {
            if( myData.touchzoneGUITexture.texture != normalTexture )
                myData.touchzoneGUITexture.texture = normalTexture;
        }
    }
}

/*
Debug.DrawLine( new Vector2( myData.basePosition.x - sizeX, myData.basePosition.y ),
                new Vector2( myData.basePosition.x + myData.calcWidth + sizeX, myData.basePosition.y + myData.calcHeight ),
                Color.green );


Debug.DrawLine( new Vector2( myData.basePosition.x, myData.basePosition.y - sizeY ),
                new Vector2( myData.basePosition.x + myData.calcWidth, myData.basePosition.y + myData.calcHeight + sizeY ),
                Color.red );

void Update()
        {
            Debug.DrawLine( myData.basePosition, new Vector2( myData.basePosition.x + myData.calcWidth, myData.basePosition.y ) );
            Debug.DrawLine( myData.basePosition, new Vector2( myData.basePosition.x, myData.basePosition.y + myData.calcHeight ) );
            Debug.DrawLine( new Vector2( myData.basePosition.x + myData.calcWidth, myData.basePosition.y + myData.calcHeight ),
                            new Vector2( myData.basePosition.x, myData.basePosition.y + myData.calcHeight ) );
            Debug.DrawLine( new Vector2( myData.basePosition.x + myData.calcWidth, myData.basePosition.y + myData.calcHeight ),
                            new Vector2( myData.basePosition.x + myData.calcWidth, myData.basePosition.y ) );
        }
*/