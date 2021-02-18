using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Makes this script run without using play mode
[ExecuteInEditMode]
public class InEditorLevelGenerator : MonoBehaviour
{
    #region Variables
    [Tooltip("How should the maps lay out")]
    public static BuildStyle buildStyle;
    [Tooltip("Every pixle is a block")]
    public static Texture2D[] maps;
    [Tooltip("Dictionary to tell the program out to build the scene")]
    public static List<ColorToPrefab> colorMappings;
    public static int layerHight = 0;
    public static GameObject parent;
    static GameObject child;
    #endregion

    /// <summary>
    /// Generating All the Object to the Scene
    /// </summary>
    /// <param name="mapsArr">Textures 2D of the layout of the scene</param>
    /// <param name="colorMappingsArr">Dictionary of objects and colors</param>
    /// <param name="style">How to buid the scene</param>
    public static void GenerateLevel(Texture2D[] mapsArr, List<ColorToPrefab> colorMappingsArr, BuildStyle style, int layerhight)
    {
        //Set all the object to the script
        maps = mapsArr;
        colorMappings = colorMappingsArr;
        buildStyle = style;
        parent = new GameObject();
        parent.name = "Level";
        layerHight = layerhight;

        //run on all the maps and generate objects to each map
        for (int m = 0; m < maps.Length; m++)
        {
            GenerateMap(maps[m], m);
        }
        SetMainCameraToPlayer();
    }

    /// <summary>
    /// Generat a map to the scene
    /// </summary>
    /// <param name="map">The map to generate</param>
    /// <param name="z">The map Z possition to put in the objects transform</param>
    public static void GenerateMap(Texture2D map, int z)
    {
        for (int i = 0; i < map.width; i++)
        {
            for (int j = 0; j < map.height; j++)
            {
                GenerateTile(new Vector3(i, j, z), map.GetPixel(i, j));
            }
        }
    }

    /// <summary>
    /// Generate a tille in the right position
    /// </summary>
    /// <param name="potition">The position of the object</param>
    /// <param name="pixelColor">The color that gets the object to create</param>
    public static void GenerateTile(Vector3 position, Color pixelColor)
    {
        //is not transperant
        if (pixelColor.a != 0)
        {
            foreach (ColorToPrefab colorMapping in colorMappings)
            {
                if (colorMapping.color.Equals(pixelColor) && colorMapping.prefab != null)
                {
                    child = InstantiateObject(colorMapping.prefab, position);
                    child.transform.SetParent(parent.transform);
                }
            }
        }
    }

    /// <summary>
    /// Create an object base onthe build style
    /// </summary>
    /// <param name="prefab">The object to create</param>
    /// <param name="objectCoordinates">Object poisition</param>
    /// <returns></returns>
    public static GameObject InstantiateObject(GameObject prefab, Vector3 objectCoordinates)
    {
        switch (buildStyle)
        {
            case BuildStyle.FrontToBack:
                return Instantiate(prefab, new Vector3(objectCoordinates.x, objectCoordinates.y + layerHight, objectCoordinates.z), Quaternion.identity);
            case BuildStyle.BottomToTop:
                return Instantiate(prefab, new Vector3(objectCoordinates.x, objectCoordinates.z + layerHight, objectCoordinates.y), Quaternion.identity);
            default:
                break;
        }
        return null;
    }

    /// <summary>
    /// Give the Player the Main Camrea (For the Gameplay)
    /// </summary>
    public static void SetMainCameraToPlayer()
    {
        GameObject cam;
        //If there is no main camera - create main camera
        if (Camera.main == null)
        {
            cam = new GameObject("Main Camera", typeof(Camera));
            cam.transform.tag = "MainCamera";
        }
        else
        {
            cam = Camera.main.gameObject;
        }

        //set the Parent Position and Rotation
        print(GameObject.FindGameObjectWithTag("Player"));
        cam.transform.parent = GameObject.FindGameObjectWithTag("Player").transform;
        cam.transform.localPosition = new Vector3(0, 4, -10);
        //print(cam.transform.rotation);
        cam.transform.Rotate(new Vector3(15, 0, 0), Space.Self);
        //print(cam.transform.rotation);

    }
}
