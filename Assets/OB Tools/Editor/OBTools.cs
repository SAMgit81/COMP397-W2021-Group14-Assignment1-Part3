/*
 * OB Tools
 * Created by Omar Balfaqih
 * https://obalfaqih.com
 * 
 */
using UnityEngine;
using UnityEditor;

public class OBTools : MonoBehaviour
{
    [MenuItem("Window/OB Tools/About", false, 11)]
    public static void AboutOBTools()
    {
        Application.OpenURL("https://obalfaqih.com/unity-tools");
    }
}
