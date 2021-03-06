using Sirenix.OdinInspector.Editor;
using UnityEditor;
using DenizYanar.EnemySystem;


namespace DenizYanar.OdinEditor
{
    public class EnemyDataEditor : OdinMenuEditorWindow
    {
        [MenuItem("Tools/Enemy Data")]
        private static void OpenWindow()
        {
            GetWindow<EnemyDataEditor>().Show();
        }
        
        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree();

            
            tree.AddAllAssetsAtPath("Enemy Data", "Assets/Data/Enemy", typeof(EnemySettings));
            
            return tree;
        }
        
        
    }
}
