using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
namespace LZDEditorTools{
[CustomEditor(typeof(BuildBundle))]
public class BuildBundleEditor:Editor{
    
    private BuildBundle bundleObject;
    private SerializedObject bundle;
    private SerializedProperty assetname;
    private SerializedProperty bundleExtension;
    private SerializedProperty fileListName;
    private SerializedProperty targetPlatform;
    private SerializedProperty removePath;
    private SerializedProperty bundleList;
    private SerializedProperty copyList;
    private string saveName = "";
    void OnEnable(){
        bundleObject = BuildBundle.Load<BuildBundle>(BuildBundle.assetName);
        bundle = new SerializedObject(bundleObject);
        assetname = bundle.FindProperty("assetName");
        bundleExtension = bundle.FindProperty("bundleExtension");
        fileListName = bundle.FindProperty("fileListName");
        targetPlatform = bundle.FindProperty("target");
        removePath = bundle.FindProperty("RemoveOldPath");
        bundleList = bundle.FindProperty("bundleList");
        copyList = bundle.FindProperty("copyList");
    }
    public override void OnInspectorGUI(){
        serializedObject.Update(); //Update SerializedObject
        LZDEditorUtility.ScriptTitle("Script");
        EditorGUILayout.BeginHorizontal ();
        if(GUILayout.Button("Save as")){
           BuildBundle.Create<BuildBundle>(saveName);
        }
        saveName = GUILayout.TextField(saveName);
        EditorGUILayout.EndHorizontal();
    }
}

}