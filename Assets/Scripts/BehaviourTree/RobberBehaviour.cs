using UnityEngine;
using DenizYanar.BehaviourTreeAI;

namespace DenizYanar
{
    public class RobberBehaviour : MonoBehaviour
    {
        private BehaviourTree _tree;

        private Node.EStatus _treeStatus = Node.EStatus.RUNNING;
        
        private void Start()
        {
            _tree = new BehaviourTree();
            var steal = new Node("Steel something");
            var goToDiamond = new Leaf("Go To Diamond", GoToDiamond);
            var goToVan = new Node("Go To Van");
            var eat = new Node("Eat");
            var eatPizza = new Node("Eat Pizza");
            var eatPasta = new Node("Eat Pasta");
            
            steal.AddChild(goToDiamond);
            steal.AddChild(goToVan);
            eat.AddChild(eatPizza);
            eat.AddChild(eatPasta);
            _tree.AddChild(steal);
            _tree.AddChild(eat);
            
            _tree.PrintTree();
        }

        private Node.EStatus GoToDiamond()
        {
            Debug.Log("Robber start to go for diamond!");
            return Node.EStatus.SUCCESS;
        }

        private void Update()
        {
            if (_treeStatus == Node.EStatus.RUNNING)
                _treeStatus = _tree.Process();
        }
    }
}
