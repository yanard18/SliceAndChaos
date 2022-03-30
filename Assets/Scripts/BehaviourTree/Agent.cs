using DenizYanar.Core;


namespace DenizYanar.BehaviourTreeAI
{
    public abstract class Agent : Character
    {
        protected BehaviourTree Tree;

        protected override void Awake()
        {
            base.Awake();
            Tree = new BehaviourTree();
            StartCoroutine(Tree.Behave());
        }
        
        
        
    }
}
