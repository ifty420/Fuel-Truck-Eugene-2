/*****************************************************
 * 													 *
 * Asset:		 Touch Controls Kit					 *
 * Script:		 TouchManagerSpriteRenderer.cs       *
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
    [ExecuteInEditMode]
    public class TouchManagerSpriteRenderer : TouchManagerBase
    {
        private static bool outdated = false;

        private static int screenWidth = 0;
        private static int screenHeight = 0;


        /// <summary>
        /// Starts a single update for outdated values.
        /// </summary>
        public static void Obsolete()
        {
            outdated = true;
        }

        // Use this for initialization
        void Awake()
        {
            TouchManagerSetup();
        }

        // Update is called once per frame
        void Update()
        {
            if( screenWidth != Screen.width || screenHeight != Screen.height )
            {
                screenWidth = Screen.width;
                screenHeight = Screen.height;
                outdated = true;
            }

            if( outdated )
            {
                TouchManagerSetup();

                for( int cnt = 0; cnt < controllersCount; cnt++ )
                {
                    controllers[ cnt ].CalculationSizeAndPosition();
                }
                
                outdated = false;
            }

            // isPlaying
            if( Application.isPlaying )
                FinalUpdate( Input.touchCount );
        }

#if UNITY_EDITOR
        // OnDrawGizmos
        public bool drawGizmos = true;        
        //
        void OnDrawGizmos()
        {
            if( !drawGizmos ) return;

            float halfHeight = GuiCamera.guiCamera.orthographicSize;
            float halfWidth = halfHeight * GuiCamera.guiCamera.aspect;

            Vector2 cubeSize = Vector2.one * 1.75f;

            float halfX = cubeSize.x / 2f;
            float halfY = cubeSize.y / 2f;

            // UpperLeft
            Gizmos.DrawWireCube( new Vector2( GuiCamera.guiCameraTransform.position.x - halfWidth + halfX, GuiCamera.guiCameraTransform.position.y + halfHeight - halfY ),
                cubeSize );

            // UpperCenter
            Gizmos.DrawWireCube( new Vector2( GuiCamera.guiCameraTransform.position.x, GuiCamera.guiCameraTransform.position.y + halfHeight - halfY ),
                cubeSize );

            // UpperRight
            Gizmos.DrawWireCube( new Vector2( GuiCamera.guiCameraTransform.position.x + halfWidth - halfX, GuiCamera.guiCameraTransform.position.y + halfHeight - halfY ), 
                cubeSize );

            // MiddleLeft
            Gizmos.DrawWireCube( new Vector2( GuiCamera.guiCameraTransform.position.x - halfWidth + halfX, GuiCamera.guiCameraTransform.position.y ), 
                cubeSize );

            // MiddleCenter
            Gizmos.DrawWireCube( new Vector2( GuiCamera.guiCameraTransform.position.x, GuiCamera.guiCameraTransform.position.y ), 
                cubeSize );

            // MiddleRight
            Gizmos.DrawWireCube( new Vector2( GuiCamera.guiCameraTransform.position.x + halfWidth - halfX, GuiCamera.guiCameraTransform.position.y ), 
                cubeSize );

            // LowerLeft
            Gizmos.DrawWireCube( new Vector2( GuiCamera.guiCameraTransform.position.x - halfWidth + halfX, GuiCamera.guiCameraTransform.position.y - halfHeight + halfY ), 
                cubeSize );

            // LowerCenter
            Gizmos.DrawWireCube( new Vector2( GuiCamera.guiCameraTransform.position.x, GuiCamera.guiCameraTransform.position.y - halfHeight + halfY ), 
                cubeSize );

            // LowerRight
            Gizmos.DrawWireCube( new Vector2( GuiCamera.guiCameraTransform.position.x + halfWidth - halfX, GuiCamera.guiCameraTransform.position.y - halfHeight + halfY ), 
                cubeSize );


            // LowerLeft to UpperLeft
            Debug.DrawLine( new Vector2( GuiCamera.guiCameraTransform.position.x - halfWidth, GuiCamera.guiCameraTransform.position.y - halfHeight ),   //LowerLeft
                            new Vector2( GuiCamera.guiCameraTransform.position.x - halfWidth, GuiCamera.guiCameraTransform.position.y + halfHeight ) ); //UpperLeft

            // LowerLeft to LowerRight
            Debug.DrawLine( new Vector2( GuiCamera.guiCameraTransform.position.x - halfWidth, GuiCamera.guiCameraTransform.position.y - halfHeight ),   //LowerLeft
                            new Vector2( GuiCamera.guiCameraTransform.position.x + halfWidth, GuiCamera.guiCameraTransform.position.y - halfHeight ) ); //LowerRight

            // UpperLeft to UpperRight
            Debug.DrawLine( new Vector2( GuiCamera.guiCameraTransform.position.x - halfWidth, GuiCamera.guiCameraTransform.position.y + halfHeight ),   //UpperLeft
                            new Vector2( GuiCamera.guiCameraTransform.position.x + halfWidth, GuiCamera.guiCameraTransform.position.y + halfHeight ) ); //UpperRight

            // UpperRight to LowerRight
            Debug.DrawLine( new Vector2( GuiCamera.guiCameraTransform.position.x + halfWidth, GuiCamera.guiCameraTransform.position.y + halfHeight ),   //UpperRight
                            new Vector2( GuiCamera.guiCameraTransform.position.x + halfWidth, GuiCamera.guiCameraTransform.position.y - halfHeight ) ); //LowerRight
        }
#endif
    }
}