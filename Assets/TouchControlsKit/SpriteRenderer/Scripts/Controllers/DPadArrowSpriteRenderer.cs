/*****************************************************
 * 													 *
 * Asset:		 Touch Controls Kit					 *
 * Script:		 DPadArrowSpriteRenderer.cs          *
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
    public class DPadArrowSpriteRenderer : DPadArrowBase
    {
        public Data.ControllerDataSpriteRenderer myData = new Data.ControllerDataSpriteRenderer();


        // OnDestroy
        void OnDestroy()
        {
            TouchManagerSpriteRenderer.Obsolete();
        }

        // DPadArrowAwake
        internal override void DPadArrowAwake()
        {
            myData.SetTouchzoneByGameObject( gameObject );
            myData.touchzoneSprite.color = ElementTransparency.colorHalfSprite;
        }

        // DPadArrowADisable
        internal override void DPadArrowADisable()
        {
            myData.touchzoneSprite.color = ElementTransparency.colorZeroAll;
        }

        // CalculationSizeAndPosition
        internal void CalculationSizeAndPosition( SpriteRenderer sprite )
        {
            float posX = sprite.bounds.center.x;
            float posY = sprite.bounds.center.y;
            float halfX = sprite.bounds.extents.x;
            float halfY = sprite.bounds.extents.y;
            //
            float myHalfX = myData.touchzoneSprite.bounds.extents.x;
            float myHalfY = myData.touchzoneSprite.bounds.extents.y;
            //
            float calcX = myData.OffsetX * halfX / 100f;
            float calcY = myData.OffsetY * halfY / 100f;

            switch( ArrowType )
            {
                case ArrowTypes.UP:
                    myData.basePosition.x = posX + calcX;
                    myData.basePosition.y = posY + halfY - myHalfY - calcY;
                    break;

                case ArrowTypes.DOWN:
                    myData.basePosition.x = posX + calcX;
                    myData.basePosition.y = posY - halfY + myHalfY + calcY;
                    break;

                case ArrowTypes.RIGHT:
                    myData.basePosition.x = posX + halfX - myHalfX - calcX;
                    myData.basePosition.y = posY + calcY;
                    break;

                case ArrowTypes.LEFT:
                    myData.basePosition.x = posX - halfX + myHalfX + calcX;
                    myData.basePosition.y = posY + calcY;
                    break;
            }

            myData.touchzoneTransform.position = myData.basePosition;
        }

        // CheckBoolPosition
        protected override bool CheckBoolPosition( Vector2 touchPos, float sizeX, float sizeY )
        {
            switch( ArrowType )
            {
                case ArrowTypes.UP:
                case ArrowTypes.DOWN:
                    if( touchPos.x < myData.touchzoneTransform.position.x + sizeX
                    && touchPos.y < myData.touchzoneSprite.bounds.max.y
                    && touchPos.x > myData.touchzoneTransform.position.x - sizeX
                    && touchPos.y > myData.touchzoneSprite.bounds.min.y )
                    {
                        return true;
                    }
                    break;                  

                case ArrowTypes.RIGHT:
                case ArrowTypes.LEFT:
                    if( touchPos.x < myData.touchzoneSprite.bounds.max.x
                    && touchPos.y < myData.touchzoneTransform.position.y + sizeY
                    && touchPos.x > myData.touchzoneSprite.bounds.min.x
                    && touchPos.y > myData.touchzoneTransform.position.y - sizeY )
                    {                       
                        return true;
                    }
                    break;               
            }
            return false;
        }
        //
    }
}

/*
Debug.DrawLine ( new Vector2( myTransform.position.x + sizeX, myData.touchzoneSprite.bounds.max.y ), 
                new Vector2( myTransform.position.x - sizeX, myData.touchzoneSprite.bounds.min.y ), 
                Color.red );

Debug.DrawLine( new Vector2( myData.touchzoneSprite.bounds.max.x, myTransform.position.y + sizeY ), 
                new Vector2( myData.touchzoneSprite.bounds.min.x, myTransform.position.y - sizeY ), 
                Color.green );
*/