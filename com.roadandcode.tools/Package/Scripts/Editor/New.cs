using UnityEditor.EditorTools;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System;

public class New : Editor
{
    // Create a new instance of the ReferenceFinderWindow class
    //public static ReferenceFinderWindow window;


    public static List<GameObject> myObjects = new List<GameObject>();

    [MenuItem("Tools/MY references", false, 0)]
    static void ShowReferencesMenuItem(MenuCommand command)
    {
        MyReferences((GameObject)command.context);

    }


    [MenuItem("Tools/tool 01/hello there", false, 0)]
    static void one(MenuCommand command)
    {
        MyReferences((GameObject)command.context);

    }

    static void MyReferences(GameObject selectedObject)
    {

        Debug.Log("Hello there you struggling developer");
    }

    
}


