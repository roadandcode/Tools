using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class ReferenceFinder : Editor
{

    // Create a new instance of the ReferenceFinderWindow class
    public static ReferenceFinderWindow window;


    public static List<GameObject> myObjects = new List<GameObject>();

    [MenuItem("GameObject/Show references", false, 0)]
    static void ShowReferencesMenuItem(MenuCommand command)
    {
        ShowReferences((GameObject)command.context);

    }

    static void ShowReferences(GameObject selectedObject)
    {
        List<GameObject> referencingObjects = new List<GameObject>();


        // Get a list of all script components in the scene
        MonoBehaviour[] scripts = FindObjectsOfType<MonoBehaviour>();

        // Iterate through the list of script components
        foreach (MonoBehaviour script in scripts)
        {


            // Use reflection to get a list of all serialized fields in the script
            FieldInfo[] fieldTypeLists = script.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            // Iterate through the list of fields that are marked as List and Array
            foreach (FieldInfo field in fieldTypeLists)
            {

                // Check if the field is a reference to a list or array of objects
                if (field.FieldType.IsArray || (field.FieldType.IsGenericType && field.FieldType.GetGenericTypeDefinition() == typeof(List<>)))
                {


                    // Get the value of the field (the list or array)
                    object fieldValue = field.GetValue(script);

                    // Check if the list or array contains the selected object
                    if (fieldValue is IEnumerable<object> enumerableField)
                    {

                        if (enumerableField.Contains(selectedObject))
                        {
                            referencingObjects.Add(script.gameObject);
                        }
                    }
                }


                // Check if the field is a serialized field
                if (Attribute.IsDefined(field, typeof(SerializeField)) || field.IsPublic)
                {
                    // Get the value of the field
                    object fieldValue = field.GetValue(script);

                    if (fieldValue != null)
                    {
                        var ob_name = fieldValue.ToString();

                        var m_obj_name = selectedObject.name + " (UnityEngine.GameObject)";


                        // Check if the field value is the selected object
                        if (ob_name == m_obj_name)
                        {
                            referencingObjects.Add(script.gameObject);
                        }
                    }

                }


            }
        }

        ReferenceFinderWindow myWindow = EditorWindow.GetWindow<ReferenceFinderWindow>();
        myWindow.Close();

        window = (ReferenceFinderWindow)CreateInstance(typeof(ReferenceFinderWindow));

        // Set the list of referencing objects
        window.referencingObjects = referencingObjects;
        window.SelectedObject = selectedObject;
        //  Show the window
        window.Show();


    }
}


