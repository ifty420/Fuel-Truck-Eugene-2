/***********************************************************
 * 														   *
 * Asset:		 Touch Controls Kit	        		       *
 * Script:		 PrefabCreatorSpriteRenderer.cs            *
 * 														   *
 * Copyright(c): Victor Klepikov						   *
 * Support: 	 http://bit.ly/vk-Support				   *
 * 														   *
 * mySite:       http://vkdemos.ucoz.org				   *
 * myAssets:     http://bit.ly/VictorKlepikovUnityAssets   *
 * myTwitter:	 http://twitter.com/VictorKlepikov		   *
 * myFacebook:	 http://www.facebook.com/vikle4 		   *
 * 														   *
 ***********************************************************/


using UnityEngine;
using UnityEditor;
using TouchControlsKit.Utils;

namespace TouchControlsKit.SpriteRender.Inspector
{
    public sealed class PrefabCreatorSpriteRenderer : Editor
    {
        // 
        private const string mainGOName = "_TouchManagerSpriteRenderer";
        private const string menuAbbrev = "GameObject/Create Other/Touch Controls Kit/SpriteRenderer/";
        private const string nameAbbrev = "TouchControlsKit";

        //
        private static GameObject tckGUIobj = null;

        private static GameObject Button = null;

        private static GameObject JoystickMain, JoystickBackgr, Joystick, JoystickTouchzone;

        private static GameObject Touchpad = null;

        private static GameObject DpadMain, DpadArrowUP, DpadArrowDOWN, DpadArrowLEFT, DpadArrowRIGHT, DpadTouchzone;

        private static GameObject SteeringWheel = null;


        // CreateTouchManager [MenuItem]
        [MenuItem( menuAbbrev + "Touch Manager" )]
        private static void CreateTouchManager()
        {
            if( FindObjectOfType<TouchManagerSpriteRenderer>() && !tckGUIobj ) tckGUIobj = FindObjectOfType<TouchManagerSpriteRenderer>().gameObject;
            
            if( tckGUIobj ) 
                return;

            tckGUIobj = new GameObject( mainGOName, typeof( TouchManagerSpriteRenderer ) );
            GuiCamera.CreateCamera( tckGUIobj.transform, 1, 10f );
        }

        [MenuItem( menuAbbrev + "Touch Manager", true )]
        private static bool ValidateCreateTouchManager()
        {
            return !FindObjectOfType<TouchManagerSpriteRenderer>();
        }


        // CreateButton [MenuItem]
        [MenuItem( menuAbbrev + "Button" )]
        private static void CreateButton()
        {
            if( !tckGUIobj ) 
                CreateTouchManager();

            SetupController<ButtonSpriteRenderer>( ref Button, tckGUIobj.transform, "Button" + FindObjectsOfType<ButtonSpriteRenderer>().Length.ToString() );
            ButtonSpriteRenderer btnTemp = Button.GetComponent<ButtonSpriteRenderer>();
            btnTemp.normalSprite = Resources.LoadAssetAtPath<Sprite>( "Assets/" + nameAbbrev + "/Base/Resources/Sprites/ButtonNormal.png" );
            btnTemp.pressedSprite = Resources.LoadAssetAtPath<Sprite>( "Assets/" + nameAbbrev + "/Base/Resources/Sprites/ButtonPressed.png" );
            btnTemp.myData.touchzoneTransform = Button.transform;
            btnTemp.MyName = Button.name;
            btnTemp.myData.OffsetX = Random.Range( -50f, 50f );
            btnTemp.myData.OffsetY = Random.Range( -50f, 50f );

            TouchManagerSpriteRenderer.Obsolete();
        }

        // CreateJoystick [MenuItem]
        [MenuItem( menuAbbrev + "Joystick" )]
        private static void CreateJoystick()
        {
            if( !tckGUIobj ) 
                CreateTouchManager();

            SetupController<JoystickSpriteRenderer>( ref JoystickMain, tckGUIobj.transform, "Joystick" + FindObjectsOfType<JoystickSpriteRenderer>().Length.ToString() );

            SetupController<SpriteRenderer>( ref JoystickBackgr, JoystickMain.transform, "JoystickBackgr" );
            SetupController<SpriteRenderer>( ref Joystick, JoystickBackgr.transform, "Joystick" );
            SetupController<TouchzoneResizeEvents>( ref JoystickTouchzone, JoystickMain.transform, "touchzone" );

            JoystickSpriteRenderer joyTemp = JoystickMain.GetComponent<JoystickSpriteRenderer>();

            joyTemp.joystickBackgroundSprite = JoystickBackgr.GetComponent<SpriteRenderer>();
            joyTemp.joystickBackgroundSprite.sortingOrder = 1;
            joyTemp.joystickBackgroundTR = JoystickBackgr.transform;

            joyTemp.joystickSprite = Joystick.GetComponent<SpriteRenderer>();
            joyTemp.joystickSprite.sortingOrder = 2;
            joyTemp.joystickTR = Joystick.transform;

            joyTemp.myData.touchzoneSprite = JoystickTouchzone.GetComponent<SpriteRenderer>();
            joyTemp.myData.touchzoneTransform = JoystickTouchzone.transform;

            joyTemp.joystickBackgroundSprite.sprite = Resources.LoadAssetAtPath<Sprite>( "Assets/" + nameAbbrev + "/Base/Resources/Sprites/JoystickBack.png" );
            joyTemp.joystickSprite.sprite = Resources.LoadAssetAtPath<Sprite>( "Assets/" + nameAbbrev + "/Base/Resources/Sprites/Joystick.png" );
            joyTemp.myData.touchzoneSprite.sprite = Resources.LoadAssetAtPath<Sprite>( "Assets/" + nameAbbrev + "/Base/Resources/Sprites/Touchzone.png" );

            joyTemp.MyName = JoystickMain.name;

            joyTemp.myData.OffsetX = Random.Range( -50f, 50f );
            joyTemp.myData.OffsetY = Random.Range( -50f, 50f );

            joyTemp.myData.touchzoneTransform.localScale = new Vector3( 0.5f, 0.6f, 1f );
            
            TouchManagerSpriteRenderer.Obsolete();
        }

        // CreateTouchpad[MenuItem]
        [MenuItem( menuAbbrev + "Touchpad" )]
        private static void CreateTouchpad()
        {
            if( !tckGUIobj ) 
                CreateTouchManager();

            SetupController<TouchpadSpriteRenderer>( ref Touchpad, tckGUIobj.transform, "Touchpad" + FindObjectsOfType<TouchpadSpriteRenderer>().Length.ToString() );
            TouchpadSpriteRenderer tpTemp = Touchpad.GetComponent<TouchpadSpriteRenderer>();
            tpTemp.myData.touchzoneSprite = Touchpad.GetComponent<SpriteRenderer>();
            tpTemp.myData.touchzoneSprite.sprite = Resources.LoadAssetAtPath<Sprite>( "Assets/" + nameAbbrev + "/Base/Resources/Sprites/Touchzone.png" );
            tpTemp.MyName = Touchpad.name;
            tpTemp.myData.touchzoneTransform = Touchpad.transform;
            tpTemp.myData.OffsetX = Random.Range( -50f, 50f );
            tpTemp.myData.OffsetY = Random.Range( -50f, 50f );
            tpTemp.myData.touchzoneTransform.localScale = new Vector3( 1f, 0.7f, 1f );

            TouchManagerSpriteRenderer.Obsolete();
        }

        // CreateDPad [MenuItem]
        [MenuItem( menuAbbrev + "DPad" )]
        private static void CreateDPad()
        {
            if( !tckGUIobj ) 
                CreateTouchManager();

            SetupController<DPadSpriteRenderer>( ref DpadMain, tckGUIobj.transform, "DPad" + FindObjectsOfType<DPadSpriteRenderer>().Length.ToString() );
            DPadSpriteRenderer dpadTemp = DpadMain.GetComponent<DPadSpriteRenderer>();

            DPadArrowSpriteRenderer tempArrow = null;
            //
            SetupController<DPadArrowSpriteRenderer>( ref DpadArrowUP, DpadMain.transform, "ArrowUP" );
            tempArrow = DpadArrowUP.GetComponent<DPadArrowSpriteRenderer>();
            tempArrow.ArrowType = DPadArrowBase.ArrowTypes.UP;
            DpadArrowUP.transform.rotation = Quaternion.Euler( 0f, 0f, 90f );
            //
            SetupController<DPadArrowSpriteRenderer>( ref DpadArrowDOWN, DpadMain.transform, "ArrowDOWN" );
            tempArrow = DpadArrowDOWN.GetComponent<DPadArrowSpriteRenderer>();
            tempArrow.ArrowType = DPadArrowBase.ArrowTypes.DOWN;
            DpadArrowDOWN.transform.rotation = Quaternion.Euler( 0f, 0f, 270f );
            //
            SetupController<DPadArrowSpriteRenderer>( ref DpadArrowLEFT, DpadMain.transform, "ArrowLEFT" );
            tempArrow = DpadArrowLEFT.GetComponent<DPadArrowSpriteRenderer>();
            tempArrow.ArrowType = DPadArrowBase.ArrowTypes.LEFT;
            DpadArrowLEFT.transform.rotation = Quaternion.Euler( 0f, 0f, 180f );
            //
            SetupController<DPadArrowSpriteRenderer>( ref DpadArrowRIGHT, DpadMain.transform, "ArrowRIGHT" );
            tempArrow = DpadArrowRIGHT.GetComponent<DPadArrowSpriteRenderer>();
            tempArrow.ArrowType = DPadArrowBase.ArrowTypes.RIGHT;
            //
            SetupController<TouchzoneResizeEvents>( ref DpadTouchzone, DpadMain.transform, "touchzone" );

            dpadTemp.myData.touchzoneSprite = DpadTouchzone.GetComponent<SpriteRenderer>();
            dpadTemp.myData.touchzoneTransform = DpadTouchzone.transform;

            dpadTemp.myData.touchzoneSprite.sprite = Resources.LoadAssetAtPath<Sprite>( "Assets/" + nameAbbrev + "/Base/Resources/Sprites/Touchzone.png" );
            dpadTemp.normalSprite = Resources.LoadAssetAtPath<Sprite>( "Assets/" + nameAbbrev + "/Base/Resources/Sprites/ArrowNormal.png" );
            dpadTemp.pressedSprite = Resources.LoadAssetAtPath<Sprite>( "Assets/" + nameAbbrev + "/Base/Resources/Sprites/ArrowPressed.png" );

            dpadTemp.MyName = DpadMain.name; 

            dpadTemp.myData.OffsetX = Random.Range( -50f, 50f );
            dpadTemp.myData.OffsetY = Random.Range( -50f, 50f );
            
            TouchManagerSpriteRenderer.Obsolete();
        }

        // CreateSteeringWheel[MenuItem]
        [MenuItem( menuAbbrev + "Steering Wheel" )]
        private static void CreateSteeringWheel()
        {
            if( !tckGUIobj ) 
                CreateTouchManager();

            SetupController<SteeringWheelSpriteRenderer>( ref SteeringWheel, tckGUIobj.transform, "SteeringWheel" + FindObjectsOfType<SteeringWheelSpriteRenderer>().Length.ToString() );
            SteeringWheelSpriteRenderer swTemp = SteeringWheel.GetComponent<SteeringWheelSpriteRenderer>();
            swTemp.myData.touchzoneSprite = SteeringWheel.GetComponent<SpriteRenderer>();
            swTemp.myData.touchzoneSprite.sprite = Resources.LoadAssetAtPath<Sprite>( "Assets/" + nameAbbrev + "/Base/Resources/Sprites/SteeringWheel.png" );
            swTemp.MyName = SteeringWheel.name;
            swTemp.myData.touchzoneTransform = SteeringWheel.transform;
            swTemp.myData.OffsetX = Random.Range( -50f, 50f );
            swTemp.myData.OffsetY = Random.Range( -50f, 50f );
            swTemp.myData.touchzoneTransform.localScale = new Vector3( 2.45f, 2.45f, 1f );

            TouchManagerSpriteRenderer.Obsolete();
        }
        

        // SetupController<Generic>
        private static void SetupController<TComp>(
            ref GameObject myGO, Transform myParent, string myName )
            where TComp : Component
        {
            myGO = new GameObject( myName );
            myGO.AddComponent<TComp>();
            myGO.transform.parent = myParent;
            myGO.transform.localScale = Vector3.one;
            myGO.transform.localPosition = Vector3.zero;                       
        }
    }
}