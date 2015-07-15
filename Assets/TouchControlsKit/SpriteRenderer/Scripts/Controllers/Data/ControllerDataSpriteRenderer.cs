/*****************************************************
 * 													 *
 * Asset:		 Touch Controls Kit					 *
 * Script:		 ControllerDataSpriteRenderer.cs     *
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

namespace TouchControlsKit.SpriteRender.Data
{
    /// <summary>
    /// Contains second data, needed for the controller.
    /// </summary>
    [System.Serializable]
    public sealed class ControllerDataSpriteRenderer
    {
        [SerializeField]
        private ControllerAnchor anchor = ControllerAnchor.MiddleCenter;

        [SerializeField]
        private float offsetX = 0f;

        [SerializeField]
        private float offsetY = 0f;        

        public SpriteRenderer touchzoneSprite = null;
        public Transform touchzoneTransform = null;

        internal Vector2 basePosition = Vector2.zero;
        

        // Anchor
        public ControllerAnchor Anchor
        {
            get { return anchor; }
            set
            {
                if( anchor == value ) return;
                anchor = value;
                TouchManagerSpriteRenderer.Obsolete();
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
                TouchManagerSpriteRenderer.Obsolete();
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
                TouchManagerSpriteRenderer.Obsolete();
            }
        }


        // SetTouchzone
        internal void SetTouchzoneByGameObject( GameObject gameObject )
        {
            touchzoneSprite = gameObject.GetComponent<SpriteRenderer>();
            touchzoneTransform = gameObject.transform;
        }

        // Anchoring
        internal void Anchoring()
        {
            float halfHeight = GuiCamera.guiCamera.orthographicSize;
            float halfWidth = halfHeight * GuiCamera.guiCamera.aspect;

            float halfX = touchzoneSprite.bounds.extents.x;
            float halfY = touchzoneSprite.bounds.extents.y;

            float calcX = offsetX * halfWidth / 100f;
            float calcY = offsetY * halfHeight / 100f; 

            
            switch( anchor )
            {
                // Lower
                case ControllerAnchor.LowerLeft:
                    basePosition.x = GuiCamera.guiCameraTransform.position.x - halfWidth + halfX + calcX;
                    basePosition.y = GuiCamera.guiCameraTransform.position.y - halfHeight + halfY + calcY;
                    break;
                case ControllerAnchor.LowerCenter:
                    basePosition.x = GuiCamera.guiCameraTransform.position.x + calcX;
                    basePosition.y = GuiCamera.guiCameraTransform.position.y - halfHeight + halfY + calcY;
                    break;
                case ControllerAnchor.LowerRight:
                    basePosition.x = GuiCamera.guiCameraTransform.position.x + halfWidth - halfX - calcX;
                    basePosition.y = GuiCamera.guiCameraTransform.position.y - halfHeight + halfY + calcY;
                    break;

                // Middle
                case ControllerAnchor.MiddleLeft:
                    basePosition.x = GuiCamera.guiCameraTransform.position.x - halfWidth + halfX + calcX;
                    basePosition.y = GuiCamera.guiCameraTransform.position.y + calcY;
                    break;
                case ControllerAnchor.MiddleCenter:
                    basePosition.x = GuiCamera.guiCameraTransform.position.x + calcX;
                    basePosition.y = GuiCamera.guiCameraTransform.position.y + calcY;
                    break;
                case ControllerAnchor.MiddleRight:
                    basePosition.x = GuiCamera.guiCameraTransform.position.x + halfWidth - halfX - calcX;
                    basePosition.y = GuiCamera.guiCameraTransform.position.y + calcY;
                    break;

                // Upper
                case ControllerAnchor.UpperLeft:
                    basePosition.x = GuiCamera.guiCameraTransform.position.x - halfWidth + halfX + calcX;
                    basePosition.y = GuiCamera.guiCameraTransform.position.y + halfHeight - halfY - calcY;
                    break;
                case ControllerAnchor.UpperCenter:
                    basePosition.x = GuiCamera.guiCameraTransform.position.x + calcX;
                    basePosition.y = GuiCamera.guiCameraTransform.position.y + halfHeight - halfY - calcY;
                    break;
                case ControllerAnchor.UpperRight:
                    basePosition.x = GuiCamera.guiCameraTransform.position.x + halfWidth - halfX - calcX;
                    basePosition.y = GuiCamera.guiCameraTransform.position.y + halfHeight - halfY - calcY;
                    break;
            }            
        }

        // CheckPosition
        internal bool CheckTouchPosition( Vector2 touchPos )
        {
            touchPos = GuiCamera.ScreenToWorldPoint( touchPos );

            if( touchPos.x < touchzoneSprite.bounds.max.x
                && touchPos.y < touchzoneSprite.bounds.max.y
                && touchPos.x > touchzoneSprite.bounds.min.x
                && touchPos.y > touchzoneSprite.bounds.min.y )
            {
                return true;
            }
            else return false;
        }       
    }
}