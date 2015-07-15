/*****************************************************
 * 													 *
 * Asset:		 Touch Controls Kit					 *
 * Script:		 TouchpadGuiTextureEditor.cs         *
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

namespace TouchControlsKit.GuiTexture.Inspector
{
    [CustomEditor( typeof( TouchpadGuiTexture ) )]
    public class TouchpadGuiTextureEditor : Editor
    {
        private TouchpadGuiTexture myTarget = null;


        // OnEnable
        void OnEnable()
        {
            myTarget = ( TouchpadGuiTexture )target;

            AxesHelper.HelperSetup( myTarget );
            EventsHelper.HelperSetup( myTarget );
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
            GUILayout.BeginVertical( "Box" );
            GUILayout.Label( "Parameters", StyleHelper.LabelStyle() );
            GUILayout.Space( 5 );

            const int size = 115;

            GUILayout.BeginHorizontal();
            GUILayout.Label( "Anchor", GUILayout.Width( size ) );
            myTarget.myData.Anchor = ( ControllerAnchor )EditorGUILayout.EnumPopup( myTarget.myData.Anchor );
            GUILayout.EndHorizontal();

            GUILayout.Space( 5 );

            GUILayout.BeginHorizontal();
            GUILayout.Label( "Sensitivity", GUILayout.Width( size ) );
            myTarget.sensitivity = EditorGUILayout.Slider( myTarget.sensitivity, 1f, 10f );
            GUILayout.EndHorizontal();

            GUILayout.Space( 5 );

            float minOffsetX = 0f;
            float minOffsetY = 0f;
            if( myTarget.myData.Anchor == ControllerAnchor.LowerCenter || myTarget.myData.Anchor == ControllerAnchor.UpperCenter ) minOffsetX = -35f;
            else if( myTarget.myData.Anchor == ControllerAnchor.MiddleCenter )
            {
                minOffsetX = -35f;
                minOffsetY = -35f;
            }

            GUILayout.BeginHorizontal();
            GUILayout.Label( "Offset X", GUILayout.Width( size ) );
            myTarget.myData.OffsetX = EditorGUILayout.Slider( myTarget.myData.OffsetX, minOffsetX, 35f );
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label( "Offset Y", GUILayout.Width( size ) );
            myTarget.myData.OffsetY = EditorGUILayout.Slider( myTarget.myData.OffsetY, minOffsetY, 35f );
            GUILayout.EndHorizontal();

            GUILayout.Space( 5 );

            GUILayout.BeginHorizontal();
            GUILayout.Label( "Show TouchZone", GUILayout.Width( size ) );
            myTarget.ShowTouchZone = EditorGUILayout.Toggle( myTarget.ShowTouchZone );
            GUILayout.EndHorizontal();

            if( myTarget.ShowTouchZone )
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space( 15 );
                GUILayout.Label( "TouchZone Width", GUILayout.Width( size ) );
                myTarget.myData.ImageWidth = EditorGUILayout.Slider( myTarget.myData.ImageWidth, 5f, 50f );
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space( 15 );
                GUILayout.Label( "TouchZone Height", GUILayout.Width( size ) );
                myTarget.myData.ImageHeight = EditorGUILayout.Slider( myTarget.myData.ImageHeight, 5f, 50f );
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space( 15 );
                GUILayout.Label( "TouchZone Texture", GUILayout.Width( size ) );
                myTarget.myData.touchzoneGUITexture.texture = EditorGUILayout.ObjectField( myTarget.myData.touchzoneGUITexture.texture, typeof( Texture2D ), false ) as Texture2D;
                GUILayout.EndHorizontal();
            }
            GUILayout.Space( 5 );

            GUILayout.BeginHorizontal();
            GUILayout.Label( "Touchpad Name", GUILayout.Width( size ) );
            myTarget.MyName = EditorGUILayout.TextField( myTarget.MyName );
            GUILayout.EndHorizontal();            

            GUILayout.Space( 5 );
            GUILayout.EndVertical();
            
            //
            GUILayout.Space( 5 );
            AxesHelper.ShowAxes( size );
            GUILayout.Space( 5 );
            EventsHelper.ShowEvents( size );
            //
        }
    }
}