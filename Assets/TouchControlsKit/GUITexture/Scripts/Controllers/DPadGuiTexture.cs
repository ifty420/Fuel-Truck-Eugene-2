/*****************************************************
 * 													 *
 * Asset:		 Touch Controls Kit					 *
 * Script:		 DPadGuiTexture.cs               	 *
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
    public class DPadGuiTexture : DPadBase
    {
        public Data.ControllerDataGuiTexture myData = new Data.ControllerDataGuiTexture();
                
        private DPadArrowGuiTexture[] myArrowsGuiTexture = null;

        private Vector2 centerPosition = Vector2.zero;


        // OnDestroy
        void OnDestroy()
        {
            TouchManagerGuiTexture.Obsolete();
        }

        // ControlDisable
        internal override void ControlDisable()
        {
            ShowTouchZone = false;
            for( int cnt = 0; cnt < myArrowsGuiTexture.Length; cnt++ )
            {
                myArrowsGuiTexture[ cnt ].DPadArrowADisable();
            }
        }

        // ControlAwake
        internal override void ControlAwake()
        {
            base.ControlAwake();
            myData.GetTouchzoneGUITexture( gameObject );
            myArrowsGuiTexture = GetComponentsInChildren<DPadArrowGuiTexture>();
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

            centerPosition.x = myData.basePosition.x + myData.calcWidth / 2f;
            centerPosition.y = myData.basePosition.y + myData.calcHeight / 2f;

            defaultPosition = centerPosition;
            currentPosition = centerPosition;

            sizeX = myData.calcWidth / 2f;
            sizeY = myData.calcHeight / 2f;

            for( int cnt = 0; cnt < myArrowsGuiTexture.Length; cnt++ )
            {
                myArrowsGuiTexture[ cnt ].DPadArrowAwake();
                myArrowsGuiTexture[ cnt ].CalculationSizeAndPosition( centerPosition, sizeX, sizeY );
            }
        }

        // CheckTouchPosition
        internal override bool CheckTouchPosition( Vector2 touchPos )
        {
            return myData.CheckTouchPosition( touchPos, myData.calcWidth, myData.calcHeight );
        }

        // GetCurrentPosition
        protected override void GetCurrentPosition( Vector2 touchPos )
        {
            if( enableAxisX ) currentPosition.x = touchPos.x;
            if( enableAxisY ) currentPosition.y = touchPos.y;

        }

        // CalculateBorderSize
        protected override void CalculateBorderSize( out float calcX, out float calcY )
        {
            calcX = myData.calcWidth / 2.5f;
            calcY = myData.calcHeight / 2.5f;
        }

        // ArrowDown
        protected override void ArrowDown( int index )
        {
            myArrowsGuiTexture[ index ].SetPressedTexture();
        }

        // ArrowUp
        protected override void ArrowUp( int index )
        {
            myArrowsGuiTexture[ index ].SetNormalTexture();
        }
    }
}

/*
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