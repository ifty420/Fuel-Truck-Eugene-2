/*****************************************************
 * 													 *
 * Asset:		 Touch Controls Kit					 *
 * Script:		 DPadArrowGuiTextureEditor.cs        *
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
    [CustomEditor( typeof( DPadArrowGuiTexture ) )]
    public class DPadArrowGuiTextureEditor : Editor
    {
        private DPadArrowGuiTexture myTarget = null;


        // OnEnable
        void OnEnable()
        {
            myTarget = ( DPadArrowGuiTexture )target;
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
                minOffsetX = -15f;
            }
            else if( myTarget.ArrowType == DPadArrowBase.ArrowTypes.LEFT || myTarget.ArrowType == DPadArrowBase.ArrowTypes.RIGHT )
            {
                minOffsetY = -15f;
            }

            GUILayout.BeginHorizontal();
            GUILayout.Label( "Offset X", GUILayout.Width( size ) );
            myTarget.myData.OffsetX = EditorGUILayout.Slider( myTarget.myData.OffsetX, minOffsetX, 15f );
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label( "Offset Y", GUILayout.Width( size ) );
            myTarget.myData.OffsetY = EditorGUILayout.Slider( myTarget.myData.OffsetY, minOffsetY, 15f );
            GUILayout.EndHorizontal();

            GUILayout.Space( 5 );

            GUILayout.BeginHorizontal();
            GUILayout.Label( "Arrow Width", GUILayout.Width( size ) );
            myTarget.myData.ImageWidth = EditorGUILayout.Slider( myTarget.myData.ImageWidth, 1f, 10f );
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label( "Arrow Height", GUILayout.Width( size ) );
            myTarget.myData.ImageHeight = EditorGUILayout.Slider( myTarget.myData.ImageHeight, 1f, 10f );
            GUILayout.EndHorizontal();

            GUILayout.Space( 5 );
            GUILayout.EndVertical();

            GUILayout.Space( 5 );

            GUILayout.BeginVertical( "Box" );
            GUILayout.Label( "Textures", StyleHelper.LabelStyle() );
            GUILayout.Space( 5 );

            const int txsize = 140;

            GUILayout.BeginHorizontal();
            GUILayout.Label( "            Normal", GUILayout.Width( txsize ) );
            GUILayout.Label( "          Pressed", GUILayout.Width( txsize ) );
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            myTarget.normalTexture = EditorGUILayout.ObjectField( myTarget.normalTexture, typeof( Texture2D ), false, GUILayout.Width( txsize ), GUILayout.Height( txsize ) ) as Texture2D;
            myTarget.pressedTexture = EditorGUILayout.ObjectField( myTarget.pressedTexture, typeof( Texture2D ), false, GUILayout.Width( txsize ), GUILayout.Height( txsize ) ) as Texture2D;
            GUILayout.EndHorizontal();

            GUILayout.EndVertical();
        }
    }
}