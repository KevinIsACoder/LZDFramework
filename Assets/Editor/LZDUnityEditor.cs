using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
/*
*AUTHOR: #AUTHOR#
*CREATETIME: #CREATETIME#
*DESCRIPTION: 
*/
namespace lzdUnityEditor
{
    public class LZDUnityEditor
    {
        private const string root_name = "LZDUnityEditor";
        [MenuItem(root_name + "/BundleObject",false,100)]
        private static void createBundle()
        {
            MyScriptObject.CreateScriptObject<BundleObject>("Bundle");
        }

    }
}