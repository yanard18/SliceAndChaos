using DenizYanar.BehaviourTreeAI;
using UnityEngine;

namespace DenizYanar
{
    public class DummyBehaviour : MonoBehaviour
    {
        private BehaviourTree _tree;

        private Node.EStatus _treeStatus = Node.EStatus.RUNNING;

        private void Start()
        {
            _tree = new BehaviourTree();
            
        }
    }
}
