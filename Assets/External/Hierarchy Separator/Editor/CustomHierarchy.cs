using UnityEditor;
using UnityEngine;

namespace DenizYanar
{
    [InitializeOnLoad]
    public class CustomHierarchy : MonoBehaviour
    {
        private static Vector2 offset = new Vector2(8, 1);

        static CustomHierarchy()
        {
            EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOnGUI;
        }


        /// <summary>
        /// An custom hierarchy editor for seperators.
        /// </summary>
        /// <param name="instanceID"></param>
        /// <param name="selectionRect"></param>

        private static void HandleHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
        {
            Color textColor = Color.white;
            Color backgroundColor = Color.black;

            string seperatorDefiner = "[SEPERATOR]";
            
            var obj = EditorUtility.InstanceIDToObject(instanceID);
            if(obj != null)
            {
                if (obj.name.Contains(seperatorDefiner))
                {
                    string newName = obj.name.Remove(0, seperatorDefiner.Length);

                    
                    Rect offsetRect = new Rect(selectionRect.position + offset, selectionRect.size);
                    EditorGUI.DrawRect(selectionRect, backgroundColor);
                    EditorGUI.LabelField(offsetRect, newName, new GUIStyle() 
                    {
                        normal = new GUIStyleState() { textColor = textColor },
                        fontStyle = FontStyle.Bold
                    });
                }

 
            }
        }
    }
}
