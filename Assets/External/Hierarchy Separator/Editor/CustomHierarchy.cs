using UnityEditor;
using UnityEngine;

namespace DenizYanar
{
    [InitializeOnLoad]
    public class CustomHierarchy : MonoBehaviour
    {
        static CustomHierarchy()
        {
            EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOnGUI;
        }


        /// <summary>
        /// A custom hierarchy editor for separators.
        /// </summary>
        /// <param name="instanceID"></param>
        /// <param name="selectionRect"></param>

        private static void HandleHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
        {
            GameObject obj = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
            
            if (obj == null) return;
            if (obj.GetComponent<HierarchySeparator>() == null) return;
            HierarchySeparator separator = obj.GetComponent<HierarchySeparator>();
            if(separator.Profile == null) return;
            
            Rect offsetRect = new Rect(selectionRect.position + separator.Profile.TextOffset, selectionRect.size);
            EditorGUI.DrawRect(selectionRect, separator.Profile.BackgroundColor);
            EditorGUI.LabelField(offsetRect, obj.name, new GUIStyle()
            {
                normal = new GUIStyleState() {textColor = separator.Profile.TextColor},
                fontStyle = separator.Profile.FontStyle,
                alignment = separator.Profile.TextAlignment
            });
        }
    }
}
