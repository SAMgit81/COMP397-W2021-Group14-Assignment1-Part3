using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrays_ScriptableObject : ScriptableObject
{
    #region Variables
    [Tooltip("Dictionary to tell the program out to build the scene")]
    public List<ColorToPrefab> ColorMapping;
    [Tooltip("A map of this slice of the level")]
    public Texture2D[] textures;
    public Sprite thisSprite;
    #endregion

    //All the functions to do with this class
    #region Functions
    public void PrintValues()
    {
        foreach (ColorToPrefab item in ColorMapping)
        {
            Debug.Log(item.color + " - " + item.prefab);
        }
    }

    /// <summary>
    /// Make the input images to readable
    /// </summary>
    public void ImageTosprite()
    {
        Texture2D readableTexture;
        for (int i = 0; i < textures.Length; i++)
        {
            byte[] pix = textures[i].GetRawTextureData();
            readableTexture = new Texture2D(textures[i].width, textures[i].height, textures[i].format, false);
            readableTexture.LoadRawTextureData(pix);
            readableTexture.Apply();
            textures[i] = readableTexture;
        }
    }

    /// <summary>
    /// calls the Creat level function
    /// </summary>
    /// <param name="style">How to build the level</param>
    public void CreateLevel(BuildStyle style, int layerHight)
    {
        ImageTosprite();
        InEditorLevelGenerator.GenerateLevel(textures, ColorMapping, style,layerHight);
    }

    /// <summary>
    /// Gets all the colors that are in all of the input images
    /// </summary>
    public void GetAllColorsFromImages()
    {
        ImageTosprite();
        List<Color> colors = new List<Color>();

        Color pixleColor;
        foreach (Texture2D texture2D in textures)
        {
            for (int i = 0; i < texture2D.width; i++)
            {
                for (int j = 0; j < texture2D.height; j++)
                {
                    pixleColor = texture2D.GetPixel(i, j);
                    if (pixleColor.a != 0 && colors.Contains(pixleColor) == false)
                    {
                        colors.Add(pixleColor);
                    }
                }
            }
            CreateColorSlots(colors);
        }
    }

    /// <summary>
    /// Gets the colors that are already in the editor window list 
    /// </summary>
    public List<Color> GetColorsFromColorMappings()
    {
        List<Color> colors = new List<Color>();

        foreach (ColorToPrefab colorToPrefab in ColorMapping)
        {
            if (colorToPrefab.color != null)
            {
                colors.Add(colorToPrefab.color);
            }
        }
        return colors;
    }

    /// <summary>
    /// Creating slots for the colors list in the editor window
    /// </summary>
    /// <param name="colors">The colors from the texture</param>
    public void CreateColorSlots(List<Color> colors)
    {
        List<Color> colorsAlreadyInMappings = GetColorsFromColorMappings();
        foreach (Color color in colors)
        {
            if (!colorsAlreadyInMappings.Contains(color))
            {
                ColorMapping.Add(new ColorToPrefab(color));
            }
        }
    }
    #endregion
}
