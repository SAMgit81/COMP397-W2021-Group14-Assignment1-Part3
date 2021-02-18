using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Serialization;
using System;

public class LevelEditorWindow : EditorWindow
{
    #region Variables
    public string[] strings;
    public BuildStyle buildStyle;
    private SerializedObject so;
    private SerializedProperty[] stringsProp = new SerializedProperty[2];
    int layerHight = 0;
    Color color;

    Arrays_ScriptableObject arrays_ScriptableObject;
    #endregion

    [MenuItem("Window/Level Editor")]
    public static void ShowWindow()
    {
        GetWindow<LevelEditorWindow>("LevelEditorWindow");
    }

    //Build the Window GUI
    public void OnGUI()
    {
        if (!Application.isPlaying)
        {
            #region Check for errors
            if (so.targetObject == null)
            {
                SetScriptableObject();
            }
            #endregion

            #region Lables
            GUILayout.Space(10);
            GUILayout.Label("Set all the variables", EditorStyles.boldLabel);
            GUILayout.Space(0);
            #endregion

            #region buildStyle drop down
            buildStyle = (BuildStyle)EditorGUILayout.EnumPopup("You level build style:", buildStyle);
            #endregion

            layerHight = EditorGUILayout.IntField("Layer (Optional)", layerHight);

            #region maps and color mappings
            for (int i = 0; i < stringsProp.Length; i++)
            {
                try
                {
                    EditorGUILayout.PropertyField(stringsProp[i], true);
                }
                catch { }
            }

            so.ApplyModifiedProperties();

            #endregion

            #region Buttons
            GUIStyle myButtonStyle = new GUIStyle(GUI.skin.button);
            myButtonStyle.fontSize = 25;
            float buttonHight = 70;

            if (GUILayout.Button("Get Colors From Maps", myButtonStyle, GUILayout.Height(buttonHight)))
            {
                GetColorsToMenu();
            }
            if (GUILayout.Button("Generate Level", myButtonStyle, GUILayout.Height(buttonHight)))
            {
                GenerateLevelButtonPress();
            }
            #endregion
        }
        else
        {
            #region Lables
            GUILayout.Space(10);
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.fontSize = 40;
            style.fontStyle = FontStyle.Bold;
            GUILayout.Label("You can not use this\nin play mode", style);
            GUILayout.Space(0);
            #endregion
        }
    }

    //Get the Scriptable objects arrays
    private void OnEnable()
    {
        SetScriptableObject();
        layerHight = 0;
    }

    void SetScriptableObject()
    {
        if (arrays_ScriptableObject == null)
        {
            arrays_ScriptableObject = new Arrays_ScriptableObject();
        }
        so = new SerializedObject(arrays_ScriptableObject);
        stringsProp[0] = so.FindProperty("textures");
        stringsProp[1] = so.FindProperty("ColorMapping");
    }

    /// <summary>
    /// Create the Scene
    /// </summary>
    public void GenerateLevelButtonPress()
    {
        Arrays_ScriptableObject newScriptable = (Arrays_ScriptableObject)so.targetObject;
        newScriptable.CreateLevel(buildStyle, layerHight);
    }

    /// <summary>
    /// Adds all the colors from the images to the colors array
    /// </summary>
    public void GetColorsToMenu()
    {
        Arrays_ScriptableObject newScriptable = (Arrays_ScriptableObject)so.targetObject;
        newScriptable.GetAllColorsFromImages();
        arrays_ScriptableObject = newScriptable;
        OnEnable();
    }
}
