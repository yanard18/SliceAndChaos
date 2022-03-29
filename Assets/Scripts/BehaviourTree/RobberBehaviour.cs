using UnityEngine;
using DenizYanar.BehaviourTreeAI;
using DenizYanar.Core;

namespace DenizYanar
{
    public class RobberBehaviour : Agent
    {
        protected override void Awake()
        {
            base.Awake();
            
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
            Tree.AddChild(steal);
            Tree.AddChild(eat);
            
            Tree.PrintTree();
        }

        private Node.EStatus GoToDiamond()
        {
            Debug.Log("Robber start to go for diamond!");
            return Node.EStatus.SUCCESS;
        }

        protected override void Death(Damage damage)
        {
            
        }
    }
}
