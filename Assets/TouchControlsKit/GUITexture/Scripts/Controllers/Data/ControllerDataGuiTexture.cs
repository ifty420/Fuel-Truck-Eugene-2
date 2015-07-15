/*****************************************************
 * 													 *
 * Asset:		 Touch Controls Kit					 *
 * Script:		 ControllerDataGuiTexture.cs         *
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

namespace TouchControlsKit.GuiTexture.Data
{
    /// <summary>
    /// Contains second data, needed for the controller.
    /// </summary>
    [System.Serializable]
    public sealed class ControllerDataGuiTexture
    {
        [SerializeField]
        private ControllerAnchor anchor = ControllerAnchor.MiddleCenter;

        [SerializeField]
        private float offsetX = 0f;

        [SerializeField]
        private float offsetY = 0f;

        [SerializeField]
        private float imageWidth = 8f;

        [SerializeField]
        private float imageHeight = 4.5f;
                
        public GUITexture touchzoneGUITexture = null;

        internal float calcWidth, calcHeight;
        internal float calcX, calcY;

        internal Vector2 basePosition = Vector2.zero;

        private Rect myRect = new Rect( 0f, 0f, 0f, 0f );


        // Anchor
        public ControllerAnchor Anchor
        {
            get { return anchor; }
            set
            {
                if( anchor == value ) return;
                anchor = value;
                TouchManagerGuiTexture.Obsolete();
            }
        }

        // OffsetX
        public float OffsetX
        {
            get { return offsetX; }
            set
            {
                if( offsetX == value ) return;
                offsetX = value;
                TouchManagerGuiTexture.Obsolete();
            }
        }

        // OffsetY
        public float OffsetY
        {
            get { return offsetY; }
            set
            {
                if( offsetY == value ) return;
                offsetY = value;
                TouchManagerGuiTexture.Obsolete();
            }
        }

        // ImageWidth
        public float ImageWidth
        {
            get { return imageWidth; }
            set
            {
                if( imageWidth == value ) return;
                imageWidth = value;
                TouchManagerGuiTexture.Obsolete();
            }
        }

        // ImageHeight
        public float ImageHeight
        {
            get { return imageHeight; }
            set
            {
                if( imageHeight == value ) return;
                imageHeight = value;
                TouchManagerGuiTexture.Obsolete();
            }
        }


        // GetTouchzone
        internal void GetTouchzoneGUITexture( GameObject gameObject )
        {
            touchzoneGUITexture = gameObject.GetComponent<GUITexture>();
        }

        // Anchoring
        internal void Anchoring( float width, float height )
        {
            float screenWidth = ( float )Screen.width;
            float screenHeight = ( float )Screen.height;

            calcX = offsetX * screenWidth / 100f;
            calcY = offsetY * screenHeight / 100f;

            calcWidth = imageWidth / 50f * screenWidth;
            calcHeight = imageHeight / 50f * screenWidth;

            switch( anchor )
            {
                // Lower
                case ControllerAnchor.LowerLeft:
                    basePosition.x = calcX;
                    basePosition.y = calcY;
                    break;
                case ControllerAnchor.LowerCenter:
                    basePosition.x = ( screenWidth - width ) / 2f + calcX;
                    basePosition.y = calcY;
                    break;
                case ControllerAnchor.LowerRight:
                    basePosition.x = screenWidth - width - calcX;
                    basePosition.y = calcY;
                    break;

                // Middle
                case ControllerAnchor.MiddleLeft:
                    basePosition.x = calcX;
                    basePosition.y = ( screenHeight - height ) / 2f + calcY;
                    break;
                case ControllerAnchor.MiddleCenter:
                    basePosition.x = ( screenWidth - width ) / 2f + calcX;
                    basePosition.y = ( screenHeight - height ) / 2f + calcY;
                    break;
                case ControllerAnchor.MiddleRight:
                    basePosition.x = screenWidth - width - calcX;
                    basePosition.y = ( screenHeight - height ) / 2f + calcY;
                    break;

                // Upper
                case ControllerAnchor.UpperLeft:
                    basePosition.x = calcX;
                    basePosition.y = screenHeight - height - calcY;
                    break;
                case ControllerAnchor.UpperCenter:
                    basePosition.x = ( screenWidth - width ) / 2f + calcX;
                    basePosition.y = screenHeight - height - calcY;
                    break;
                case ControllerAnchor.UpperRight:
                    basePosition.x = screenWidth - width - calcX;
                    basePosition.y = screenHeight - height - calcY;
                    break;
            }
        }

        // CheckPosition
        internal bool CheckTouchPosition( Vector2 touchPos, float width, float height )
        {
            if( touchPos.x > basePosition.x
                && touchPos.y > basePosition.y
                && touchPos.x < basePosition.x + width
                && touchPos.y < basePosition.y + height )
            {
                return true;
            }
            else return false;
        }

        // UpdateRect
        internal Rect UpdateRect( Vector2 pos, float width, float height )
        {
            myRect.x = pos.x;
            myRect.y = pos.y;
            myRect.width = width;
            myRect.height = height;
            return myRect;
        }
    }
}