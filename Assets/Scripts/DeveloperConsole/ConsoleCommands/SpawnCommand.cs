using DenizYanar.DeveloperConsoleSystem;
using DenizYanar.EnemySystem;
using UnityEngine;

namespace DenizYanar
{
    [CreateAssetMenu(menuName = "Developer Console/Commands/Spawn Enemy")]
    public class SpawnCommand : ConsoleCommand
    {
        [SerializeField] private EnemyTable m_EnemyTable;


        public override void Execute(string[] parameters)
        {
            var enemyID = int.Parse(parameters[1]);
            var xCoordinate = int.Parse(parameters[2]);
            var yCoordinate = int.Parse(parameters[3]);
            var nEnemy = int.Parse(parameters[4]);
            
            var spawnPosition = new Vector2(xCoordinate, yCoordinate);
            
            if(nEnemy > 1000) return;
            
            for(var i = 0; i<nEnemy; i++)
                Instantiate(m_EnemyTable.m_TEnemies[enemyID], spawnPosition, Quaternion.identity);
        }
    }
}
