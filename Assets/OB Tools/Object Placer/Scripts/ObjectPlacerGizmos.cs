/*
 * OB's Object Placer
 * Created by Omar Balfaqih
 * https://obalfaqih.com
 * 
 */
 #if UNITY_EDITOR
using UnityEngine;

[ExecuteInEditMode]
public class ObjectPlacerGizmos : MonoBehaviour
{
    [HideInInspector]
    public bool isDrawing;
    private Vector3 _position, _size;
    [HideInInspector]
    public Quaternion _rotation;
    [HideInInspector]
    public bool lookUp;
    [HideInInspector]
    public Color color;

    public void DrawPlacer(Vector3 pos, Vector3 size, bool _draw = true)
    {
        _position = pos;
        transform.position = _position;
        _size = size;
        isDrawing = _draw;
        
    }

    public void UpdatePlacer(Vector3 pos)
    {
        _position = pos;
        transform.position = _position;
    }

    private void OnDrawGizmos()
    {
        if (isDrawing)
        {
            Color tmp = Gizmos.color;
            Gizmos.color = color;

            GL.PushMatrix();
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawCube(Vector3.zero, _size);
            GL.PopMatrix();

            Gizmos.color = tmp;
        }
    }
}
#endif