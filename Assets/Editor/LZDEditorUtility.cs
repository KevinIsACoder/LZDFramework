using UnityEngine;
using UnityEditor;
namespace LZDEditorTools{
  public static class LZDEditorUtility{

        private static float m_space = 5;
        private static float m_leftSpace = 10;
        private static float m_indexWidth = 20;
        private static float m_singleHight = EditorGUIUtility.singleLineHeight;

        public static void ScriptTitle(object target){
            GUI.enabled = false;
            if(target is MonoBehaviour){
               EditorGUILayout.ObjectField("Script",MonoScript.FromMonoBehaviour(target as MonoBehaviour),typeof(MonoBehaviour),false);
            }
            else if(target is MonoScript){
                EditorGUILayout.ObjectField("Script",MonoScript.FromMonoBehaviour(target as MonoBehaviour),typeof(MonoBehaviour),false);
            }
            GUI.enabled = true;
        }

        public static Rect DrawRecorderableListHeader(Rect rect){
            float width = m_leftSpace + m_indexWidth;
            EditorGUI.LabelField(new Rect(rect.x,rect.y,width,m_singleHight),"NO.");
            rect.x += width; rect.width -= width;
            return rect;
        }

        public static Rect DrawElementList(Rect rect,SerializedProperty property,int index){
            if(GUI.Button(new Rect(rect.x,rect.y,m_indexWidth,m_singleHight),"")){

                DrawMenu(property,index);
            }
            rect.x += m_indexWidth; rect.y -= m_singleHight;
            return rect;
        }

        public static GenericMenu DrawMenu(SerializedProperty property,int index){
            GenericMenu menue = new GenericMenu();
            menue.AddItem(new GUIContent("Delete"), true, delegate
            {
                property.DeleteArrayElementAtIndex(index);
                property.serializedObject.ApplyModifiedProperties(); //调用这句相当于Apply
            });
            menue.AddItem(new GUIContent("Insert"), true, delegate {

                property.InsertArrayElementAtIndex(index);
                property.serializedObject.ApplyModifiedProperties();
            });
            menue.AddItem(new GUIContent("Move_ToTop"), true, delegate
            {
                property.MoveArrayElement(index, 0);
                property.serializedObject.ApplyModifiedProperties();
            });
            return menue;
        }
  }
}