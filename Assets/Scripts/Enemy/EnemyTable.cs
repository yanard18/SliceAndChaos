using UnityEngine;

namespace DenizYanar.EnemySystem
{
    [CreateAssetMenu(menuName = "Enemy/Enemy Table")]
    public class EnemyTable : ScriptableObject
    {
        public Enemy[] Enemies;
    }
}
