/*****************************************************
 * 													 *
 * Asset:		 Touch Controls Kit					 *
 * Script:		 DPadArrowSpriteRendererEditor.cs    *
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
using UnityEditor;
using TouchControlsKit.Inspector;

namespace TouchControlsKit.SpriteRender.Inspector
{
    [CustomEditor( typeof( DPadArrowSpriteRenderer ) )]
    public class DPadArrowSpriteRendererEditor : Editor
    {
        private DPadArrowSpriteRenderer myTarget = null;


        // OnEnable
        void OnEnable()
        {
            myTarget = ( DPadArrowSpriteRenderer )target;
        }

        // OnInspectorGUI
        public override void OnInspectorGUI()
        {
            // BEGIN
            GUILayout.BeginVertical( "Box", GUILayout.Width( 300 ) );
            GUILayout.Space( 5 );
            //

            ShowParameters();
            if( GUI.changed ) EditorUtility.SetDirty( myTarget );

            // END
            GUILayout.Space( 5 );
            GUILayout.EndVertical();
            //
        }

        // ShowParameters
        private void ShowParameters()
        {
            const int size = 115;

            GUILayout.BeginVertical( "Box" );
            GUILayout.Label( "Parameters", StyleHelper.LabelStyle() );
            GUILayout.Space( 5 );

            GUILayout.BeginHorizontal();
            GUILayout.Label( "Arrow Type", GUILayout.Width( size ) );
            myTarget.ArrowType = ( DPadArrowBase.ArrowTypes )EditorGUILayout.EnumPopup( myTarget.ArrowType );
            GUILayout.EndHorizontal();

            GUILayout.Space( 5 );

            float minOffsetX = 0f;
            float minOffsetY = 0f;

            if( myTarget.ArrowType == DPadArrowBase.ArrowTypes.UP || myTarget.ArrowType == DPadArrowBase.ArrowTypes.DOWN )
            {
                minOffsetX = -50f;
            }
            else if( myTarget.ArrowType == DPadArrowBase.ArrowTypes.LEFT || myTarget.ArrowType == DPadArrowBase.ArrowTypes.RIGHT )
            {
                minOffsetY = -50f;
            }

            GUILayout.BeginHorizontal();
            GUILayout.Label( "Offset X", GUILayout.Width( size ) );
            myTarget.myData.OffsetX = EditorGUILayout.Slider( myTarget.myData.OffsetX, minOffsetX, 50f );
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label( "Offset Y", GUILayout.Width( size ) );
            myTarget.myData.OffsetY = EditorGUILayout.Slider( myTarget.myData.OffsetY, minOffsetY, 50f );
            GUILayout.EndHorizontal();

            GUILayout.Space( 5 );
            GUILayout.EndVertical();
        }
    }
}