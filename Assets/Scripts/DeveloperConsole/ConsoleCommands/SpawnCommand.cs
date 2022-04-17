using DenizYanar.DeveloperConsoleSystem;
using DenizYanar.EnemySystem;
using UnityEngine;

namespace DenizYanar
{
    [CreateAssetMenu(menuName = "Developer Console/Commands/Spawn Enemy")]
    public class SpawnCommand : ConsoleCommand
    {
        [SerializeField] private EnemyTable _enemyTable;


        public override void Execute(string[] parameters)
        {
            var id = int.Parse(parameters[1]);
            var x = int.Parse(parameters[2]);
            var y = int.Parse(parameters[3]);
            var amount = int.Parse(parameters[4]);
            
            var spawnPos = new Vector2(x, y);
            
            if(amount > 1000) return;
            
            for(var i = 0; i<amount; i++)
                Instantiate(_enemyTable.Enemies[id], spawnPos, Quaternion.identity);
        }
    }
}
