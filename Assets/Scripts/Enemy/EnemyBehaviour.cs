using DenizYanar.BehaviourTreeAI;
using UnityEngine;

namespace DenizYanar
{
    public abstract class EnemyBehaviour : MonoBehaviour
    {
        protected BehaviourTree Tree;

        protected abstract void SetupTree();
    }
}
