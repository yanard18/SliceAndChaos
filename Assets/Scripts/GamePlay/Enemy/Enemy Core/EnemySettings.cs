using Sirenix.OdinInspector;
using UnityEngine;

namespace DenizYanar.EnemySystem
{
    public class EnemySettings : ScriptableObject
    {
        [BoxGroup("Basic Info")]
        [LabelWidth(100)]
        public string m_Alias;

        [BoxGroup("Basic Info")]
        [LabelWidth(100)]
        [TextArea]
        public string m_Description;
        
        [PreviewField(75)]
        [HorizontalGroup("Basic Stats", 75)]
        [HideLabel]
        public Sprite m_Icon;

        [Range(0, 1000)]
        [GUIColor(0.0f,0.75f,1)]
        [VerticalGroup("Basic Stats/Stats")]
        public float m_Health;


        [Range(0, 1000)]
        [GUIColor(1,0,0)]
        [VerticalGroup("Basic Stats/Stats")]
        public float m_Damage;

        [Range(0, 100)]
        [GUIColor(0,1,0)]
        [VerticalGroup("Basic Stats/Stats")] 
        public float m_Speed;

        [Range(0, 100)]
        [VerticalGroup("Basic Stats/Stats")]
        public float m_JumpForce;

    }
}
