using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
/*
*AUTHOR: #AUTHOR#
*CREATETIME: #CREATETIME#
*DESCRIPTION: 
*/
[CustomEditor(typeof(BundleObject))]
public class BuildEditor : Editor {

    private SerializedProperty m_target;
    private SerializedProperty m_outPath;
    private SerializedProperty m_filetxtName;
    private SerializedProperty m_rebuild;
    private SerializedProperty m_bundleList;
    private SerializedProperty m_copyList;

    private SerializedProperty m_bundleFoldOut;
    private SerializedProperty m_copyFoldOut;
    private ReorderableList bundleList;
    private ReorderableList copyList;
    private string SaveName;

    private float m_space = 10;
    private float m_width = 100; 
    void OnEnable()
    {
        m_target = serializedObject.FindProperty("target");
        m_outPath = serializedObject.FindProperty("outPath");
        m_filetxtName = serializedObject.FindProperty("filetxtName");
        m_rebuild = serializedObject.FindProperty("forceRebuild");
        m_bundleList = serializedObject.FindProperty("bundleinfo");
        m_copyList = serializedObject.FindProperty("copyinfo");
        m_bundleFoldOut = serializedObject.FindProperty("bundleFoldOut");
        m_copyFoldOut = serializedObject.FindProperty("copyFoldOut");

        bundleList = new ReorderableList(serializedObject, m_bundleList, true, true, true, true);
        bundleList.drawHeaderCallback += DrawBundleListHeader;
        bundleList.drawElementCallback += DrawBundleElement;
        bundleList.onRemoveCallback += RemoveCallBack;

        copyList = new ReorderableList(serializedObject, m_copyList, true, true, true, true);
        copyList.drawHeaderCallback += DrawCopyListHeader;
        copyList.drawElementCallback += DrawCopyListElement;
    }
    void Awake()
    {

    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawButtonFunction();
        EditorGUILayout.PropertyField(m_target);
        EditorGUILayout.PropertyField(m_outPath);
        EditorGUILayout.PropertyField(m_filetxtName);
        EditorGUILayout.PropertyField(m_rebuild);

        EditorGUILayout.Space();
        if (m_bundleFoldOut.boolValue = EditorGUILayout.Foldout(m_bundleFoldOut.boolValue, string.Format("Bundle({0})", bundleList.count)))
        {
            bundleList.DoLayoutList();
        }
        if(m_copyFoldOut.boolValue = EditorGUILayout.Foldout(m_copyFoldOut.boolValue, string.Format("Copy({0})", copyList.count)))
        {
            copyList.DoLayoutList();
        }
        serializedObject.ApplyModifiedProperties();
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void DrawButtonFunction()
    {
        EditorGUILayout.BeginHorizontal();
        if(GUILayout.Button("Build Bundle"))
        {
            BundleBuilder.BuildBundle(target as BundleObject);
        }
        if(GUILayout.Button("Save as"))
        {
            if (!string.IsNullOrEmpty(SaveName))
            {
                MyScriptObject.CreateScriptObject<BundleObject>(SaveName);
            }
        }
        SaveName = EditorGUILayout.TextField(SaveName);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
    }
    void DrawBundleListHeader(Rect rect)
    {
        rect.y += 1;
        float height = EditorGUIUtility.singleLineHeight;
        EditorGUI.LabelField(new Rect(rect.x, rect.y, m_width, height), "BundleName");
        rect.x += m_width + m_space;
        EditorGUI.LabelField(new Rect(rect.x, rect.y, m_width, height), "SearchPattern");
        rect.x += m_width + m_space;
        EditorGUI.LabelField(new Rect(rect.x, rect.y, m_width, height), "SerachOperation");
        rect.x += m_width + m_space;
        EditorGUI.LabelField(new Rect(rect.x , rect.y, 200, height), "SearchPath");
    }

    void DrawBundleElement(Rect rect,int index,bool isActive,bool focuse)
    {
        rect.y += 1;
        float height = EditorGUIUtility.singleLineHeight;
        SerializedProperty bundleinfo = m_bundleList.GetArrayElementAtIndex(index);
        SerializedProperty bundleName = bundleinfo.FindPropertyRelative("bundleName");
        SerializedProperty filePattern = bundleinfo.FindPropertyRelative("filePattern");
        SerializedProperty searchOperation = bundleinfo.FindPropertyRelative("searchOption");
        SerializedProperty searchPath = bundleinfo.FindPropertyRelative("searchPath");
        EditorGUI.PropertyField(new Rect(rect.x,rect.y,m_width,height),bundleName,GUIContent.none);
        rect.x += m_width + m_space;
        EditorGUI.PropertyField(new Rect(rect.x, rect.y, m_width, height), filePattern, GUIContent.none);
        rect.x += m_width + m_space;
        EditorGUI.PropertyField(new Rect(rect.x, rect.y, m_width, height), searchOperation, GUIContent.none);
        rect.x += m_width + m_space;
        EditorGUI.PropertyField(new Rect(rect.x, rect.y, 200, height), searchPath, GUIContent.none);
    }

    void DrawCopyListHeader(Rect rect)
    {
        rect.y += 1;
        float height = EditorGUIUtility.singleLineHeight;
        EditorGUI.LabelField(new Rect(rect.x, rect.y, m_width + 100, height), "SourthPath");
        rect.x += m_width + m_space;
        EditorGUI.LabelField(new Rect(rect.x, rect.y, m_width, height), "SearchPattern");
        rect.x += m_width + m_space;
        EditorGUI.LabelField(new Rect(rect.x, rect.y, m_width, height), "SerachOption");
        rect.x += m_width + m_space;
        EditorGUI.LabelField(new Rect(rect.x, rect.y, 200, height), "DestPath");
    }
    void DrawCopyListElement(Rect rect,int index,bool isActive,bool focus)
    {
        rect.y += 1;
        float height = EditorGUIUtility.singleLineHeight;
        SerializedProperty copyinfo = m_copyList.GetArrayElementAtIndex(index);
        SerializedProperty sourthPath = copyinfo.FindPropertyRelative("sourthPath");
        SerializedProperty filePattern = copyinfo.FindPropertyRelative("filePattern");
        SerializedProperty destPath = copyinfo.FindPropertyRelative("destPath");
        SerializedProperty searchOption = copyinfo.FindPropertyRelative("searchOption");
        EditorGUI.PropertyField(new Rect(rect.x, rect.y, m_width + 100, height),sourthPath, GUIContent.none);
        rect.x += m_width + m_space;
        EditorGUI.PropertyField(new Rect(rect.x, rect.y, m_width, height),filePattern, GUIContent.none);
        rect.x += m_width + m_space;
        EditorGUI.PropertyField(new Rect(rect.x, rect.y, m_width, height), destPath, GUIContent.none);
        rect.x += m_width + m_space;
        EditorGUI.PropertyField(new Rect(rect.x, rect.y, 200, height), searchOption, GUIContent.none);
    }
    void RemoveCallBack(ReorderableList list)
    {
        if(EditorUtility.DisplayDialog("Warning!","Do you want to delete the element!", "OK", "Cancel"))
        {
            ReorderableList.defaultBehaviours.DoRemoveButton(list);
        }
    }
}