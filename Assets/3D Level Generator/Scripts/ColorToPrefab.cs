using UnityEngine;

[System.Serializable]
public class ColorToPrefab
{
    #region Variables
    [Tooltip("The color to replace")]
    public Color color;
    [Tooltip("The prefab to replace it with")]
    public GameObject prefab;
    #endregion

    #region Constructor 
    public ColorToPrefab(){}

    public ColorToPrefab(Color color)
    {
        this.color = color;
    }
    #endregion
}
