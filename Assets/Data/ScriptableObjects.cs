using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneData", menuName = "Data/SceneData")]
public class ScriptableObjects : ScriptableObject
{
    [Header("PlayerData")]
    public Vector3 playerPosition;
    public Quaternion playerRotation;
    public int playerHealth;
    
}
