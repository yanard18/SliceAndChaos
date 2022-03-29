using DenizYanar.Core;
using UnityEngine;

namespace DenizYanar.BehaviourTreeAI
{
    public abstract class Agent : Character
    {
        protected BehaviourTree Tree;

        protected override void Awake()
        {
            base.Awake();
            Tree = new BehaviourTree();
        }
        protected virtual void Update() => Tree.Tick();
        
    }
}
