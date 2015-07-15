/*****************************************************
 * 													 *
 * Asset:		 Touch Controls Kit					 *
 * Script:		 DPadSpriteRenderer.cs               *
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
    public class DPadSpriteRenderer : DPadBase
    {
        public Data.ControllerDataSpriteRenderer myData = new Data.ControllerDataSpriteRenderer();

        [SerializeField]
        private Sprite normalsprite = null;
        public Sprite pressedSprite = null;

        private DPadArrowSpriteRenderer[] myArrowsSpriteRenderer = null;

        public Sprite normalSprite
        {
            get { return normalsprite; }
            set
            {
                if( normalsprite == value ) return;
                normalsprite = value;
                ControlAwake();
                TouchManagerSpriteRenderer.Obsolete();
            }
        }

        
        // OnDestroy
        void OnDestroy()
        {
            TouchManagerSpriteRenderer.Obsolete();
        }

        // ControlDisable
        internal override void ControlDisable()
        {
            ShowTouchZone = false;
            for( int cnt = 0; cnt < myArrowsSpriteRenderer.Length; cnt++ )
            {
                myArrowsSpriteRenderer[ cnt ].DPadArrowADisable();
            }
        }

        // ControlAwake
        internal override void ControlAwake()
        {
            base.ControlAwake();
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

            myArrowsSpriteRenderer = GetComponentsInChildren<DPadArrowSpriteRenderer>();
            for( int cnt = 0; cnt < myArrowsSpriteRenderer.Length; cnt++ )
            {
                myArrowsSpriteRenderer[ cnt ].DPadArrowAwake();
                myArrowsSpriteRenderer[ cnt ].CalculationSizeAndPosition( myData.touchzoneSprite );
                ArrowUp( cnt );
            }
        }

        // CheckPosition
        internal override bool CheckTouchPosition( Vector2 touchPos )
        {
            return myData.CheckTouchPosition( touchPos );
        }

        // UpdatePosition
        protected override void GetCurrentPosition( Vector2 touchPos )
        {
            if( enableAxisX ) currentPosition.x = GuiCamera.ScreenToWorldPoint( touchPos ).x;
            if( enableAxisY ) currentPosition.y = GuiCamera.ScreenToWorldPoint( touchPos ).y;

            sizeX = myData.touchzoneSprite.bounds.extents.x;
            sizeY = myData.touchzoneSprite.bounds.extents.y;
            defaultPosition = myData.touchzoneSprite.bounds.center;
        }

        // CalculateBorderSize
        protected override void CalculateBorderSize( out float calcX, out float calcY )
        {
            calcX = myData.touchzoneSprite.bounds.extents.x / 1.25f;
            calcY = myData.touchzoneSprite.bounds.extents.y / 1.25f;
        }

        // ArrowDown
        protected override void ArrowDown( int index )
        {
            if( myArrowsSpriteRenderer[ index ].myData.touchzoneSprite.sprite != pressedSprite )
                myArrowsSpriteRenderer[ index ].myData.touchzoneSprite.sprite = pressedSprite;
        }

        // ArrowUp
        protected override void ArrowUp( int index )
        {
            if( myArrowsSpriteRenderer[ index ].myData.touchzoneSprite.sprite != normalSprite )
                myArrowsSpriteRenderer[ index ].myData.touchzoneSprite.sprite = normalSprite;
        }
    }
}