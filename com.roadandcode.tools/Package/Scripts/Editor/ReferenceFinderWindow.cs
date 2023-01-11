using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ReferenceFinderWindow : EditorWindow
{
    // A list to store the referencing objects
    public List<GameObject> referencingObjects;
    public GameObject SelectedObject;
    private string info;

    

    void OnGUI()
    {
        ReferenceFinderWindow myWindow = EditorWindow.GetWindow<ReferenceFinderWindow>();

        myWindow.Repaint();
       
        if (referencingObjects.Count > 0)
        {
            info = " is referenced by these gameobjects";
        }
        else
        {
            info = " is not referenced by any gameobject";
        }
        GUILayout.Label(SelectedObject.name+ info, EditorStyles.boldLabel);

        // Iterate over the referencing objects and display their names
        foreach (GameObject obj in referencingObjects)
        {
            //EditorGUILayout.LabelField(obj.name);
            if (GUILayout.Button(obj.name))
            {
                HighlightObject(obj);
            }
        }
    }

    public void HighlightObject(GameObject obj)
    {
        Selection.objects = new Object[] { obj };

        //EditorApplication.FocusProjectWindow();
        EditorGUIUtility.PingObject(obj);
    }
}
