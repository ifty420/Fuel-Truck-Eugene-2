/*****************************************************
 * 													 *
 * Asset:		 Touch Controls Kit					 *
 * Script:		 JoystickSpriteRendererEditor.cs     *
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
    [CustomEditor( typeof( JoystickSpriteRenderer ) )]
    public class JoystickSpriteRendererEditor : Editor
    {
        private JoystickSpriteRenderer myTarget = null;
        private static string[] modNames = { "Dynamic", "Static" };


        // OnEnable
        void OnEnable()
        {
            myTarget = ( JoystickSpriteRenderer )target;

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
            const int size = 115;

            GUILayout.BeginVertical( "Box" );
            GUILayout.Label( "Parameters", StyleHelper.LabelStyle() );
            GUILayout.Space( 5 );

            GUILayout.BeginHorizontal();
            GUILayout.Label( "Anchor", GUILayout.Width( size ) );
            myTarget.myData.Anchor = ( ControllerAnchor )EditorGUILayout.EnumPopup( myTarget.myData.Anchor );
            GUILayout.EndHorizontal();

            GUILayout.Space( 5 );

            GUILayout.BeginHorizontal();
            GUILayout.Label( "Mode", GUILayout.Width( size ) );
            myTarget.IsStatic = System.Convert.ToBoolean( GUILayout.Toolbar( System.Convert.ToInt32( myTarget.IsStatic ), modNames, GUILayout.Height( 20 ) ) );
            GUILayout.EndHorizontal();

            GUILayout.Space( 5 );

            GUILayout.BeginHorizontal();
            GUILayout.Label( "Sensitivity", GUILayout.Width( size ) );
            myTarget.sensitivity = EditorGUILayout.Slider( myTarget.sensitivity, 1f, 10f );
            GUILayout.EndHorizontal();

            GUILayout.Space( 5 );

            float minOffsetX = 0f;
            float minOffsetY = 0f;

            if( myTarget.myData.Anchor == ControllerAnchor.LowerCenter || myTarget.myData.Anchor == ControllerAnchor.UpperCenter ) minOffsetX = -50f;
            else if( myTarget.myData.Anchor == ControllerAnchor.MiddleCenter )
            {
                minOffsetX = -50f;
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

            GUILayout.BeginHorizontal();
            GUILayout.Label( "Border Size", GUILayout.Width( size ) );
            myTarget.borderSize = EditorGUILayout.Slider( myTarget.borderSize, 1f, 9f );
            GUILayout.EndHorizontal();

            GUILayout.Space( 5 );

            if( myTarget.IsStatic )
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label( "Smooth Return", GUILayout.Width( size ) );
                myTarget.smoothReturn = EditorGUILayout.Toggle( myTarget.smoothReturn );
                GUILayout.EndHorizontal();

                if( myTarget.smoothReturn )
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Space( 15 );
                    GUILayout.Label( "Smooth Factor", GUILayout.Width( size ) );
                    myTarget.smoothFactor = EditorGUILayout.Slider( myTarget.smoothFactor, 1f, 20f );
                    GUILayout.EndHorizontal();
                }
            }

            GUILayout.Space( 5 );

            GUILayout.BeginHorizontal();
            GUILayout.Label( "Show TouchZone", GUILayout.Width( size ) );
            myTarget.ShowTouchZone = EditorGUILayout.Toggle( myTarget.ShowTouchZone );
            GUILayout.EndHorizontal();

            if( myTarget.ShowTouchZone )
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space( 15 );
                GUILayout.Label( "TouchZone Sprite", GUILayout.Width( size ) );
                myTarget.myData.touchzoneSprite.sprite = EditorGUILayout.ObjectField( myTarget.myData.touchzoneSprite.sprite, typeof( Sprite ), false ) as Sprite;
                GUILayout.EndHorizontal();                
            }

            GUILayout.Space( 5 );

            GUILayout.BeginHorizontal();
            GUILayout.Label( "Joystick Name", GUILayout.Width( size ) );
            myTarget.MyName = EditorGUILayout.TextField( myTarget.MyName );
            GUILayout.EndHorizontal();

            GUILayout.Space( 5 );

            GUILayout.BeginHorizontal();
            GUILayout.Label( "         Joystick" );
            GUILayout.Label( "         Background" );
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            myTarget.joystickSprite.sprite = EditorGUILayout.ObjectField( myTarget.joystickSprite.sprite, typeof( Sprite ), false ) as Sprite;
            myTarget.joystickBackgroundSprite.sprite = EditorGUILayout.ObjectField( myTarget.joystickBackgroundSprite.sprite, typeof( Sprite ), false ) as Sprite;
            GUILayout.EndHorizontal();

            GUILayout.Space( 5 );
            GUILayout.EndVertical();

            GUILayout.Space( 5 );
            AxesHelper.ShowAxes( size );
            GUILayout.Space( 5 );
            EventsHelper.ShowEvents( size ); 
        }
    }
}