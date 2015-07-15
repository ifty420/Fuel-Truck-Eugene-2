/*****************************************************
 * 													 *
 * Asset:		 Touch Controls Kit					 *
 * Script:		 ButtonSpriteRenderer.cs             *
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
    public class ButtonSpriteRenderer : ButtonBase
    {
        public Data.ControllerDataSpriteRenderer myData = new Data.ControllerDataSpriteRenderer();

        [SerializeField]
        private Sprite normalsprite = null;
        public Sprite pressedSprite = null;

        public Sprite normalSprite
        {
            get { return normalsprite; }
            set
            {
                if( normalsprite == value ) return;
                normalsprite = value;
                ControlAwake();
                TouchManagerSpriteRenderer.Obsolete();
                myData.touchzoneSprite.sprite = normalsprite;
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

        // ButtonDown
        protected override void ButtonDown()
        {
            myData.touchzoneSprite.sprite = pressedSprite;
        }

        // ButtonUp
        protected override void ButtonUp()
        {
            myData.touchzoneSprite.sprite = normalSprite;
        }
    }
}