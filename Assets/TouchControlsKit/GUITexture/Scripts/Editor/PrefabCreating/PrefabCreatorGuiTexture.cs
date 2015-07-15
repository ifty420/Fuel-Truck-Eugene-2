/*****************************************************
 * 													 *
 * Asset:		 Touch Controls Kit					 *
 * Script:		 PrefabCreatorGuiTexture.cs          *
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
using TouchControlsKit.Utils;

namespace TouchControlsKit.GuiTexture.Inspector
{
    public sealed class PrefabCreatorGuiTexture : Editor
    {
        // 
        private const string mainGOName = "_TouchManagerGuiTexture";
        private const string menuAbbrev = "GameObject/Create Other/Touch Controls Kit/GuiTexture/";
        private const string nameAbbrev = "TouchControlsKit";

        //
        private static GameObject tckGUIobj = null;

        private static GameObject Button = null;

        private static GameObject JoystickMain, JoystickBackgr, Joystick;

        private static GameObject Touchpad = null;

        private static GameObject DpadMain, DpadArrowUP, DpadArrowDOWN, DpadArrowLEFT, DpadArrowRIGHT;

        private static GameObject SteeringWheel = null;


        // CreateTouchManager 
        [MenuItem( menuAbbrev + "Touch Manager" )]
        private static void CreateTouchManager()
        {
            if( FindObjectOfType<TouchManagerGuiTexture>() && !tckGUIobj ) tckGUIobj = FindObjectOfType<TouchManagerGuiTexture>().gameObject;
            
            if( tckGUIobj ) 
                return;

            tckGUIobj = new GameObject( mainGOName, typeof( TouchManagerGuiTexture ) );
            tckGUIobj.isStatic = true;
        }

        [MenuItem( menuAbbrev + "Touch Manager", true )]
        private static bool ValidateCreateTouchManager()
        {
            return !FindObjectOfType<TouchManagerGuiTexture>();
        }


        // CreateButton [MenuItem]
        [MenuItem( menuAbbrev + "Button" )]
        private static void CreateButton()
        {
            if( !tckGUIobj ) 
                CreateTouchManager();

            SetupController<ButtonGuiTexture>( ref Button, tckGUIobj.transform, "Button" + tckGUIobj.GetComponentsInChildren<ButtonGuiTexture>().Length.ToString(), Vector3.zero );

            ButtonGuiTexture btnTemp = Button.GetComponent<ButtonGuiTexture>();

            btnTemp.normalTexture = Resources.LoadAssetAtPath<Texture2D>( "Assets/" + nameAbbrev + "/Base/Resources/Sprites/ButtonNormal.png" );
            btnTemp.pressedTexture = Resources.LoadAssetAtPath<Texture2D>( "Assets/" + nameAbbrev + "/Base/Resources/Sprites/ButtonPressed.png" );

            btnTemp.MyName = Button.name;

            btnTemp.myData.ImageWidth = 3.5f;
            btnTemp.myData.ImageHeight = 3.5f;

            btnTemp.myData.OffsetX = Random.Range( -35f, 35f );
            btnTemp.myData.OffsetY = Random.Range( -35f, 35f );

            TouchManagerGuiTexture.Obsolete();
        }

        // CreateJoystick [MenuItem]
        [MenuItem( menuAbbrev + "Joystick" )]
        private static void CreateJoystick()
        {
            if( !tckGUIobj ) 
                CreateTouchManager();

            SetupController<JoystickGuiTexture>( ref JoystickMain, tckGUIobj.transform, "Joystick" + tckGUIobj.GetComponentsInChildren<JoystickGuiTexture>().Length.ToString(), Vector3.zero );

            JoystickGuiTexture joyTemp = JoystickMain.GetComponent<JoystickGuiTexture>();

            SetupController<GUITexture>( ref Joystick, JoystickMain.transform, "Joystick", Vector3.forward );
            SetupController<GUITexture>( ref JoystickBackgr, JoystickMain.transform, "JoystickBack", Vector3.zero );
            
            joyTemp.joystickGUITexture = Joystick.GetComponent<GUITexture>();
            joyTemp.backgroundGUITexture = JoystickBackgr.GetComponent<GUITexture>();
            joyTemp.myData.touchzoneGUITexture = JoystickMain.GetComponent<GUITexture>();

            joyTemp.joystickGUITexture.texture = Resources.LoadAssetAtPath<Texture2D>( "Assets/" + nameAbbrev + "/Base/Resources/Sprites/Joystick.png" );
            joyTemp.backgroundGUITexture.texture = Resources.LoadAssetAtPath<Texture2D>( "Assets/" + nameAbbrev + "/Base/Resources/Sprites/JoystickBack.png" );
            joyTemp.myData.touchzoneGUITexture.texture = Resources.LoadAssetAtPath<Texture2D>( "Assets/" + nameAbbrev + "/Base/Resources/Sprites/Touchzone.png" );

            joyTemp.joystickGUITexture.color = ElementTransparency.colorHalfGuiTexture;
            joyTemp.backgroundGUITexture.color = ElementTransparency.colorHalfGuiTexture;
            joyTemp.myData.touchzoneGUITexture.color = ElementTransparency.colorHalfGuiTexture;            

            joyTemp.MyName = JoystickMain.name;

            joyTemp.myData.ImageWidth = 19f;
            joyTemp.myData.ImageHeight = 12.5f;

            joyTemp.myData.OffsetX = Random.Range( -35f, 35f );
            joyTemp.myData.OffsetY = Random.Range( -35f, 35f );  

            TouchManagerGuiTexture.Obsolete();
        }
                
        // CreateTouchpad [MenuItem]
        [MenuItem( menuAbbrev + "Touchpad" )]
        private static void CreateTouchpad()
        {
            if( !tckGUIobj ) 
                CreateTouchManager();

            SetupController<TouchpadGuiTexture>( ref Touchpad, tckGUIobj.transform, "Touchpad" + tckGUIobj.GetComponentsInChildren<TouchpadGuiTexture>().Length.ToString(), Vector3.zero );

            TouchpadGuiTexture tpTemp = Touchpad.GetComponent<TouchpadGuiTexture>();
            tpTemp.myData.touchzoneGUITexture = Touchpad.GetComponent<GUITexture>();
            tpTemp.myData.touchzoneGUITexture.color = ElementTransparency.colorHalfGuiTexture;
            tpTemp.myData.touchzoneGUITexture.texture = Resources.LoadAssetAtPath<Texture2D>( "Assets/" + nameAbbrev + "/Base/Resources/Sprites/Touchzone.png" );
            tpTemp.MyName = Touchpad.name;
            tpTemp.myData.ImageWidth = 19f;
            tpTemp.myData.ImageHeight = 12.5f;
            tpTemp.myData.OffsetX = Random.Range( -35f, 35f );
            tpTemp.myData.OffsetY = Random.Range( -35f, 35f );  

            TouchManagerGuiTexture.Obsolete();
        }

        // CreateDPad [MenuItem]
        [MenuItem( menuAbbrev + "DPad" )]
        private static void CreateDPad()
        {
            if( !tckGUIobj ) 
                CreateTouchManager();

            SetupController<DPadGuiTexture>( ref DpadMain, tckGUIobj.transform, "DPad" + tckGUIobj.GetComponentsInChildren<DPadGuiTexture>().Length.ToString(), Vector3.zero );

            DPadGuiTexture dpadTemp = DpadMain.GetComponent<DPadGuiTexture>();
            dpadTemp.myData.touchzoneGUITexture = dpadTemp.GetComponent<GUITexture>();
            dpadTemp.myData.touchzoneGUITexture.color = ElementTransparency.colorHalfGuiTexture;
            dpadTemp.myData.touchzoneGUITexture.texture = Resources.LoadAssetAtPath<Texture2D>( "Assets/" + nameAbbrev + "/Base/Resources/Sprites/Touchzone.png" );
            dpadTemp.MyName = DpadMain.name;
            dpadTemp.myData.ImageWidth = 12f;
            dpadTemp.myData.ImageHeight = 12f;
            dpadTemp.myData.OffsetX = Random.Range( -35f, 35f );
            dpadTemp.myData.OffsetY = Random.Range( -35f, 35f );

            DPadArrowGuiTexture tempArrow = null;
            //
            SetupController<DPadArrowGuiTexture>( ref DpadArrowUP, DpadMain.transform, "ArrowUP", Vector3.zero );
            tempArrow = DpadArrowUP.GetComponent<DPadArrowGuiTexture>();
            tempArrow.ArrowType = DPadArrowBase.ArrowTypes.UP;
            tempArrow.normalTexture = Resources.LoadAssetAtPath<Texture2D>( "Assets/" + nameAbbrev + "/Base/Resources/Textures/ArrowUpNormal.png" );
            tempArrow.pressedTexture = Resources.LoadAssetAtPath<Texture2D>( "Assets/" + nameAbbrev + "/Base/Resources/Textures/ArrowUpPressed.png" );
            tempArrow.myData.ImageWidth = 4f;
            tempArrow.myData.ImageHeight = 4f;
            //
            SetupController<DPadArrowGuiTexture>( ref DpadArrowDOWN, DpadMain.transform, "ArrowDOWN", Vector3.zero );
            tempArrow = DpadArrowDOWN.GetComponent<DPadArrowGuiTexture>();
            tempArrow.ArrowType = DPadArrowBase.ArrowTypes.DOWN;
            tempArrow.normalTexture = Resources.LoadAssetAtPath<Texture2D>( "Assets/" + nameAbbrev + "/Base/Resources/Textures/ArrowDownNormal.png" );
            tempArrow.pressedTexture = Resources.LoadAssetAtPath<Texture2D>( "Assets/" + nameAbbrev + "/Base/Resources/Textures/ArrowDownPressed.png" );
            tempArrow.myData.ImageWidth = 4f;
            tempArrow.myData.ImageHeight = 4f;
            //
            SetupController<DPadArrowGuiTexture>( ref DpadArrowLEFT, DpadMain.transform, "ArrowLEFT", Vector3.zero );
            tempArrow = DpadArrowLEFT.GetComponent<DPadArrowGuiTexture>();
            tempArrow.ArrowType = DPadArrowBase.ArrowTypes.LEFT;
            tempArrow.normalTexture = Resources.LoadAssetAtPath<Texture2D>( "Assets/" + nameAbbrev + "/Base/Resources/Textures/ArrowLeftNormal.png" );
            tempArrow.pressedTexture = Resources.LoadAssetAtPath<Texture2D>( "Assets/" + nameAbbrev + "/Base/Resources/Textures/ArrowLeftPressed.png" );
            tempArrow.myData.ImageWidth = 4f;
            tempArrow.myData.ImageHeight = 4f;
            //
            SetupController<DPadArrowGuiTexture>( ref DpadArrowRIGHT, DpadMain.transform, "ArrowRIGHT", Vector3.zero );
            tempArrow = DpadArrowRIGHT.GetComponent<DPadArrowGuiTexture>();
            tempArrow.ArrowType = DPadArrowBase.ArrowTypes.RIGHT;
            tempArrow.normalTexture = Resources.LoadAssetAtPath<Texture2D>( "Assets/" + nameAbbrev + "/Base/Resources/Textures/ArrowRightNormal.png" );
            tempArrow.pressedTexture = Resources.LoadAssetAtPath<Texture2D>( "Assets/" + nameAbbrev + "/Base/Resources/Textures/ArrowRightPressed.png" );
            tempArrow.myData.ImageWidth = 4f;
            tempArrow.myData.ImageHeight = 4f;
            
            TouchManagerGuiTexture.Obsolete();
        }

        // CreateSteeringWheel [MenuItem]
        [MenuItem( menuAbbrev + "Steering Wheel" )]
        private static void CreateSteeringWheel()
        {
            if( !tckGUIobj ) 
                CreateTouchManager();

            SetupController<SteeringWheelGuiTexture>( ref SteeringWheel, tckGUIobj.transform, "SteeringWheel" + tckGUIobj.GetComponentsInChildren<SteeringWheelGuiTexture>().Length.ToString(), Vector3.zero );

            SteeringWheelGuiTexture swTemp = SteeringWheel.GetComponent<SteeringWheelGuiTexture>();
            swTemp.myData.touchzoneGUITexture = SteeringWheel.GetComponent<GUITexture>();
            swTemp.myData.touchzoneGUITexture.color = ElementTransparency.colorHalfGuiTexture;
            swTemp.myData.touchzoneGUITexture.texture = Resources.LoadAssetAtPath<Texture2D>( "Assets/" + nameAbbrev + "/Base/Resources/Sprites/SteeringWheel.png" );
            swTemp.MyName = SteeringWheel.name;
            swTemp.myData.ImageWidth = 10f;
            swTemp.myData.ImageHeight = 10f;
            swTemp.myData.OffsetX = Random.Range( -35f, 35f );
            swTemp.myData.OffsetY = Random.Range( -35f, 35f );

            TouchManagerGuiTexture.Obsolete();
        }


        // SetupController<Generic>
        private static void SetupController<TComp>(
            ref GameObject myGO, Transform myParent, string myName, Vector3 vec )
            where TComp : Component
        {
            myGO = new GameObject( myName );
            myGO.transform.localScale = Vector3.zero;
            myGO.transform.localPosition = vec;
            myGO.transform.parent = myParent;
            myGO.isStatic = true;
            myGO.AddComponent<TComp>();
        }
    }
}