/*****************************************************
 * 													 *
 * Asset:		 Touch Controls Kit					 *
 * Script:		 TouchManagerGuiTexture.cs           *
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

namespace TouchControlsKit.GuiTexture
{
    [ExecuteInEditMode]
    public class TouchManagerGuiTexture : TouchManagerBase
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
    }
}