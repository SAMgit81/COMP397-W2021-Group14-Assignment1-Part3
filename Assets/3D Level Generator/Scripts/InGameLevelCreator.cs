using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuildStyle
{
    BottomToTop,
    FrontToBack,
}

public class InGameLevelCreator : MonoBehaviour
{
    [SerializeField]
    BuildStyle buildStyle;
    [Tooltip("A map of this slice of the level")]
    public Texture2D[] maps;
    public ColorToPrefab[] colorMappings;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(colorMappings[0].prefab);
        for (int m = 0; m < maps.Length; m++)
        {
            GenerateLevel(maps[m], m);
        }
    }

    void GenerateLevel(Texture2D map, int z)
    {
        for (int i = 0; i < map.width; i++)
        {
            for (int j = 0; j < map.height; j++)
            {
                GenerateTile(new Vector3(i, j, z), map.GetPixel(i, j));
            }
        }
    }

    void GenerateTile(Vector3 position, Color pixelColor)
    {
        //is not transperant
        if (pixelColor.a != 0)
        {
            foreach (ColorToPrefab colorMapping in colorMappings)
            {
                if (colorMapping.color.Equals(pixelColor))
                {
                    InstantiateObject(colorMapping.prefab, position);
                }
            }
        }
    }

    void InstantiateObject(GameObject prefab, Vector3 objectCoordinates)
    {
        switch (buildStyle)
        {
            case BuildStyle.FrontToBack:
                Instantiate(prefab, objectCoordinates, Quaternion.identity);
                break;
            case BuildStyle.BottomToTop:
                Instantiate(prefab, new Vector3(objectCoordinates.x, objectCoordinates.z, objectCoordinates.y), Quaternion.identity);
                break;
            default:
                break;
        }
    }
}
