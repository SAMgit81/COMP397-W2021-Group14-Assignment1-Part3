/*
 * OB's Object Placer
 * Created by Omar Balfaqih
 * https://obalfaqih.com
 * 
 */
#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEngine.Timeline;
using System.Reflection;

public class ObjectPlacer : EditorWindow
{
    private Vector3 currentPos;
    private GameObject obj;
    private ObjectPlacerGizmos opg;
    private bool isMoving = false;
    private bool lookUp = false;
    private bool offsetY = false;
    private bool useOffset;
    private Vector3 offset;
    readonly string[] offsetModes = { "Local", "World" };
    int selectedOffsetMode = 0;

    private bool gizmoSettings = false;
    private Color gizmoColor = new Color(0.3820755f, 0.3820755f, 1, 0.35f);
    private Vector3 gizmoSize = new Vector3(0.8f, 1f, 0.8f);



    readonly string[] modes = { "Scene", "Timeline (Clip)" };
    private int selectedMode = 0;

    [MenuItem ("Window/OB Tools/Object Placer &p", false, 0)]

    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(ObjectPlacer), false, "Object Placer");
    }

    void CheckGizmo()
    {
        if (opg == null)
            opg = FindObjectOfType<ObjectPlacerGizmos>();
        if (opg == null)
        {
            obj = new GameObject("Gizmos Drawer");
            obj.AddComponent<ObjectPlacerGizmos>();
            opg = obj.GetComponent<ObjectPlacerGizmos>();
        }
        if (opg == null)
            opg = FindObjectOfType<ObjectPlacerGizmos>();

        SceneVisibilityManager sm;
        if (SceneVisibilityManager.instance != null)
        {
            sm = SceneVisibilityManager.instance;
        }
        else
        {
            sm = new SceneVisibilityManager();
        }

        sm.DisablePicking(opg.gameObject, true);
        opg.DrawPlacer(Vector3.zero, gizmoSize, false);
    }

    void OnGUI()
    {
        GUILayout.Label("Move GameObject In:");
        selectedMode = GUILayout.SelectionGrid(selectedMode, modes, 1);
        GUILayout.Label("Placement Settings:");
        lookUp = GUILayout.Toggle(lookUp, "Look Up");
        useOffset = GUILayout.Toggle(useOffset, "Offset");
        if (useOffset)
        {
            offset.x = EditorGUILayout.FloatField("X", offset.x);
            offset.y = EditorGUILayout.FloatField("Y", offset.y);
            offset.z = EditorGUILayout.FloatField("Z", offset.z);
            if (selectedMode == 0)
                selectedOffsetMode = GUILayout.SelectionGrid(selectedOffsetMode, offsetModes, 1);
        }
        gizmoSettings = GUILayout.Toggle(gizmoSettings, "Show Gizmo Settings");
        if (gizmoSettings)
        {
            GUILayout.Label("Gizmo Settings:");
            gizmoColor = EditorGUILayout.ColorField(gizmoColor);
            GUILayout.Label("Size:");
            gizmoSize.x = EditorGUILayout.FloatField("X", gizmoSize.x);
            gizmoSize.y = EditorGUILayout.FloatField("Y", gizmoSize.y);
            gizmoSize.z = EditorGUILayout.FloatField("Z", gizmoSize.z);
            if (GUILayout.Button("Reset Gizmo Settings", GUILayout.Height(25)))
            {
                gizmoColor = new Color(0.3820755f, 0.3820755f, 1, 0.35f);
                gizmoSize = new Vector3(0.8f, 1f, 0.8f);
            }
        }

        if (GUILayout.Button("Place Object", GUILayout.Height(35)))
        {
            CheckGizmo();
            if (Selection.activeGameObject)
            {
                isMoving = true;
            }
            else
            {
                if (selectedMode == 0)
                {
                    isMoving = false;
                    Debug.Log("Select a gameobject to move.");
                }
            }

            if (selectedMode == 1)
                isMoving = true;
        }
        if (opg)
        {
            opg.lookUp = lookUp;
            opg.color = gizmoColor;
        }
    }

    void OnEnable()
    {
        SceneView.duringSceneGui += SceneGUI;
        CheckGizmo();
    }

    private void OnDisable()
    {
        if(opg)
            opg.isDrawing = false;
        isMoving = false;
        if(obj)
            DestroyImmediate(obj);
    }

    void SceneGUI(SceneView sceneView)
    {
        Event cur = Event.current;
        if(cur.keyCode == KeyCode.Escape)
        {
            isMoving = false;
            opg.isDrawing = false;
        }
        if (isMoving)
        {
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
            if (cur.type == EventType.MouseDown && cur.button == 0 && cur.clickCount == 1)
            {
                if (Selection.activeGameObject || selectedMode == 1)
                {
                    GameObject tmp_selection = Selection.activeGameObject;
                    var mousePos = HandleUtility.GUIPointToScreenPixelCoordinate(cur.mousePosition);
                    Ray ray = sceneView.camera.ScreenPointToRay(mousePos);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit))
                    {
                        if (tmp_selection == hit.transform.gameObject)
                            return;
                        switch (selectedMode)
                        {
                            case 0:
                                SetGameObjectTransform(tmp_selection, hit);
                                break;

                            case 1:
                                SetClipOffset(hit.point, hit.normal);
                                break;
                        }
                        Selection.activeGameObject = tmp_selection;
                        isMoving = false;
                        opg.isDrawing = false;
                    }
                }
            }

            if (Selection.activeGameObject || selectedMode == 1)
            {
                GameObject tmp_selection = Selection.activeGameObject;
                var mousePos = HandleUtility.GUIPointToScreenPixelCoordinate(cur.mousePosition);

                Ray ray = sceneView.camera.ScreenPointToRay(mousePos);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (opg && isMoving)
                    {
                        opg.transform.rotation = Quaternion.identity;
                        if (lookUp)
                        {
                            opg.transform.rotation *= Quaternion.FromToRotation(opg.transform.up, hit.normal);
                        }
                        opg.DrawPlacer(hit.point + opg.transform.TransformDirection(new Vector3(0, gizmoSize.y / 2, 0)), gizmoSize);
                    }
                }
            }
            else
            {
                if (opg)
                {
                    opg.isDrawing = false;
                    isMoving = false;
                }
            }
        }
    }

    public void SetGameObjectTransform(GameObject tmp_selection, RaycastHit hit)
    {
        Undo.RecordObject(tmp_selection.transform, "Object Placer: Adjusting Transform");
        if (lookUp)
        {
            tmp_selection.transform.rotation = Quaternion.identity;
            tmp_selection.transform.rotation *= Quaternion.FromToRotation(tmp_selection.transform.up, hit.normal);
        }
        if (useOffset)
        {
            Vector3 _offset = offset;
            if(selectedOffsetMode == 0)
            {
                // Local offset
                _offset = tmp_selection.transform.TransformDirection(offset);
            }
            tmp_selection.transform.position = hit.point + _offset;
        }
        else
            tmp_selection.transform.position = hit.point;
        HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Keyboard));
        opg.isDrawing = false;
        isMoving = false;
    }

    public void SetClipOffset(Vector3 _position, Vector3 _normal)
    {
        var obj = UnityEditor.Selection.activeObject;
        if (obj != null)
        {
            var fi = obj.GetType().GetField("m_Clip", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
            if (fi != null)
            {
                var clip = fi.GetValue(obj) as TimelineClip;
                if (clip != null)
                {
                    AnimationPlayableAsset animationPlayableAsset = clip.asset as AnimationPlayableAsset;
                    animationPlayableAsset.position = new Vector3(0, 0, 0);
                    var view = SceneView.lastActiveSceneView;
                    if (view != null)
                    {
                        Undo.RecordObject(animationPlayableAsset, "Object Placer: Adjusting Clip Offset");
                        if (lookUp)
                            animationPlayableAsset.rotation *= Quaternion.FromToRotation(Vector3.up, _normal);
                        if (useOffset)
                            animationPlayableAsset.position = _position + offset;
                        else
                            animationPlayableAsset.position = _position;
                    }
                }
                else
                {
                    Debug.Log("Select a clip please.");
                }
            }
            else
            {
                Debug.Log("Select a clip please.");
            }
        }
        else
        {
            Debug.Log("Select a clip please.");
        }
        HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Keyboard));
        opg.isDrawing = false;
        isMoving = false;
    }
}
#endif