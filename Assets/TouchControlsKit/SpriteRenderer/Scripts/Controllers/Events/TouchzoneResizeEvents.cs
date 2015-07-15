/*****************************************************
 * 													 *
 * Asset:		 Touch Controls Kit					 *
 * Script:		 TouchzoneResizeEvents.cs            *
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

namespace TouchControlsKit.SpriteRender
{
    [ExecuteInEditMode]
    [RequireComponent( typeof( SpriteRenderer ) )]
    public class TouchzoneResizeEvents : MonoBehaviour
    {
        private SpriteRenderer mySprite = null;
        private Vector3 center = Vector3.zero;
        private Vector3 size = Vector3.one;


        // Awake
        void Awake()
        {
            if( Application.isPlaying ) 
                Destroy( this ); 
            
            mySprite = GetComponent<SpriteRenderer>();
        }

        // Update
        void Update()
        {
            if( mySprite.bounds.center != center )
            {
                center = mySprite.bounds.center;
                TouchManagerSpriteRenderer.Obsolete();
            }

            if( mySprite.bounds.size != size )
            {
                size = mySprite.bounds.size;
                TouchManagerSpriteRenderer.Obsolete();
            }
        }
    }
}
