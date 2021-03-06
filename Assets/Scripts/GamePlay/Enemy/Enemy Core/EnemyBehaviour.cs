using DenizYanar.BehaviourTreeAI;
using UnityEngine;

namespace DenizYanar.EnemySystem
{
    public abstract class EnemyBehaviour : MonoBehaviour
    {
        protected BehaviourTree m_Tree;

        protected abstract void SetupTree();

        protected void RunTree() => StartCoroutine(m_Tree.Behave());
    }
}
