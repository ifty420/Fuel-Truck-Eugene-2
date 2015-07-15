/*****************************************************
 * 													 *
 * Asset:		 Touch Controls Kit					 *
 * Script:		 SteeringWheelGuiTexture.cs          *
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
    public class SteeringWheelGuiTexture : SteeringWheelBase
    {
        public Data.ControllerDataGuiTexture myData = new Data.ControllerDataGuiTexture();

        private Color32 wheelColor = Color.white;
        private Matrix4x4 wheelMatrix = Matrix4x4.identity;
        private Vector2 wheelPos = Vector2.zero;
        private Rect wheelRect = new Rect( 0f, 0f, 0f, 0f );


        // OnDestroy
        void OnDestroy()
        {
            TouchManagerGuiTexture.Obsolete();
        }

        // ControlDisable
        internal override void ControlDisable()
        {
            myData.touchzoneGUITexture.color = ElementTransparency.colorZeroAll;
            this.enabled = false;
        }

        // ControlAwake
        internal override void ControlAwake()
        {
            base.ControlAwake();
            myData.GetTouchzoneGUITexture( gameObject );
            CalculationSizeAndPosition();
            SetupTransparensy();
            this.enabled = true;
        }

        // CalculationSizeAndPosition
        internal override void CalculationSizeAndPosition()
        {
            myData.Anchoring( myData.calcWidth, myData.calcHeight );
            myData.touchzoneGUITexture.pixelInset = myData.UpdateRect( myData.basePosition, myData.calcWidth, myData.calcHeight );
            SetupTransparensy();
            defaultPosition = currentPosition = myData.basePosition;
        }

        // CheckTouchPosition
        internal override bool CheckTouchPosition( Vector2 touchPos )
        {
            return myData.CheckTouchPosition( touchPos, myData.calcWidth, myData.calcHeight );
        }

        // GetCurrentPosition
        protected override void GetCurrentPosition( Vector2 touchPos )
        {
            currentPosition.x = touchPos.x - myData.calcWidth / 2f;
            currentPosition.y = touchPos.y - myData.calcHeight / 2f;
        }


        // SetupTransparensy
        private void SetupTransparensy()
        {
            if( Application.isPlaying )
                myData.touchzoneGUITexture.color = ElementTransparency.colorZeroAll;
            else
                myData.touchzoneGUITexture.color = ElementTransparency.colorHalfGuiTexture;
        }


        // OnGUI
        void OnGUI()
        {
            wheelMatrix = GUI.matrix; 
            wheelPos.x = myData.basePosition.x;
                        
            switch( myData.Anchor )
            {
                // Lower
                case ControllerAnchor.LowerLeft: 
                case ControllerAnchor.LowerCenter: 
                case ControllerAnchor.LowerRight:
                    wheelPos.y = myData.basePosition.y + ( Screen.height - myData.calcHeight - ( myData.calcY * 2f ) );
                    break;

                // Middle
                case ControllerAnchor.MiddleLeft:
                case ControllerAnchor.MiddleCenter:
                case ControllerAnchor.MiddleRight:
                    wheelPos.y = myData.basePosition.y - myData.calcY * 2f;
                    break;

                // Upper
                case ControllerAnchor.UpperLeft:
                case ControllerAnchor.UpperCenter:
                case ControllerAnchor.UpperRight:
                    wheelPos.y = myData.basePosition.y - ( Screen.height - myData.calcHeight - ( myData.calcY * 2f ) );
                    break;
            }

            wheelRect = myData.UpdateRect( wheelPos, myData.calcWidth, myData.calcHeight );

            GUIUtility.RotateAroundPivot( -localEulerAngles.z, wheelRect.center );

            wheelColor = ElementTransparency.colorHalfGuiTexture;
            wheelColor.a *= 2;
            GUI.color = wheelColor;
            GUI.DrawTexture( wheelRect, myData.touchzoneGUITexture.texture );

            GUI.matrix = wheelMatrix;
        }
    }
}