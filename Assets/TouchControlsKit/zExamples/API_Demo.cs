using UnityEngine;
using TouchControlsKit;

public class API_Demo : MonoBehaviour
{
    public bool windowsEnabled = false;

    private int screenWidth = 0;
    private int screenHeight = 0;

    private Rect hideBtnSize = new Rect( 0f, 0f, 0f, 0f );
    private Rect disBtnSize = new Rect( 0f, 0f, 0f, 0f );

    private bool showingTouchzones = true; 


    // Update is called once per frame
    void Update()
    {
        if( screenWidth != Screen.width || screenHeight != Screen.height )
        {
            screenWidth = Screen.width;
            screenHeight = Screen.height;

            hideBtnSize.x = screenWidth - ( screenWidth / 100f * 57.5f );
            hideBtnSize.y = 5f;
            hideBtnSize.width = screenWidth / 100f * 15.25f;
            hideBtnSize.height = screenHeight / 14f;

            disBtnSize.x = screenWidth - ( screenWidth / 100f * 57.5f );
            disBtnSize.y = hideBtnSize.height + 12f;
            disBtnSize.width = screenWidth / 100f * 15.25f;
            disBtnSize.height = screenHeight / 14f;
        }
    }

    // OnGUI
    void OnGUI()
    {
        if( GUI.Button( hideBtnSize, "Show / Hide \nTouch Zones" ) )
        {
            if( TouchManagerBase.Instance.enabled )
            {
                showingTouchzones = !showingTouchzones;
                InputManager.ShowingTouchZone( showingTouchzones );
            }
        }
        
        if( GUI.Button( disBtnSize, "Enable / Disable \nControllers" ) )
        {
            showingTouchzones = false;
            TouchManagerBase.Instance.SetActive( !TouchManagerBase.Instance.enabled );
        }

        // Left Window
        if( windowsEnabled )
        {
            GUILayout.BeginArea( new Rect( 5f, 5f, screenWidth / 2.5f, screenHeight / 2f ) );
            GUILayout.BeginVertical( "Box" );

            SetGuiStyle( "<b>Joystick</b>" );

            Axes( "Joystick" );
            Sens( "Joystick" );

            GUILayout.EndVertical();
            GUILayout.EndArea();
        }

        // Right Window
        if( windowsEnabled )
        {
            GUILayout.BeginArea( new Rect( screenWidth - ( screenWidth / 100f * 40.4f ), 5f, screenWidth / 2.5f, screenHeight / 2f ) );
            GUILayout.BeginVertical( "Box" );

            SetGuiStyle( "<b>Touchpad</b>" );

            Axes( "Touchpad" );
            Sens( "Touchpad" );
            
            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
    }  

    
    // Sens
    private void Sens( string ctrlName )
    {
        float sensitivity = InputManager.GetSensitivity( ctrlName );
        sensitivity = customSlider( "Sensitivity", sensitivity, 1f, 10f );
        InputManager.SetSensitivity( ctrlName, sensitivity );
    }
    
    // Axes
    private void Axes( string ctrlName )
    {
        GUILayout.BeginHorizontal();

        bool enableX = InputManager.GetAxisEnable( ctrlName, "Horizontal" );
        GUILayout.BeginHorizontal();
        GUILayout.Label( "Enable X Axis", GUILayout.Width( 115 ) );
        enableX = GUILayout.Toggle( enableX, string.Empty );
        GUILayout.EndHorizontal();
        InputManager.SetAxisEnable( ctrlName, "Horizontal", enableX );

        if( enableX )
        {
            bool inverseX = InputManager.GetAxisInverse( ctrlName, "Horizontal" );
            GUILayout.BeginHorizontal();
            GUILayout.Label( "Inverse X", GUILayout.Width( 60 ) );
            inverseX = GUILayout.Toggle( inverseX, string.Empty );
            GUILayout.EndHorizontal();
            InputManager.SetAxisInverse( ctrlName, "Horizontal", inverseX );
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        bool enableY = InputManager.GetAxisEnable( ctrlName, "Vertical" );
        GUILayout.BeginHorizontal();
        GUILayout.Label( "Enable Y Axis", GUILayout.Width( 115 ) );
        enableY = GUILayout.Toggle( enableY, string.Empty );
        GUILayout.EndHorizontal();
        InputManager.SetAxisEnable( ctrlName, "Vertical", enableY );

        if( enableY )
        {
            bool inverseY = InputManager.GetAxisInverse( ctrlName, "Vertical" );
            GUILayout.BeginHorizontal();
            GUILayout.Label( "Inverse Y", GUILayout.Width( 60 ) );
            inverseY = GUILayout.Toggle( inverseY, string.Empty );
            GUILayout.EndHorizontal();
            InputManager.SetAxisInverse( ctrlName, "Vertical", inverseY );
        }
        GUILayout.EndHorizontal();
    }


    // SetGuiStyle
    private void SetGuiStyle( string labelName )
    {
        GUIStyle style = GUI.skin.GetStyle( "Label" );
        style.richText = true;
        style.alignment = TextAnchor.UpperCenter;
        style.normal.textColor = Color.red;
        GUILayout.Label( labelName, style );
        style.richText = false;
        style.alignment = TextAnchor.UpperLeft;
        style.normal.textColor = Color.white;
    }

    // customSlider
    private float customSlider( string label, float currentValue, float minValue, float maxValue )
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label( label, GUILayout.Width( 115 ) );
        currentValue = GUILayout.HorizontalSlider( currentValue, minValue, maxValue );
        GUILayout.Space( 10 );
        GUILayout.Label( string.Format( "{0:F2}", currentValue ), GUILayout.MaxWidth( 50 ) );
        GUILayout.EndHorizontal();
        return currentValue;
    }
}