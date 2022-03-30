using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DenizYanar.BehaviourTreeAI
{
    public class BehaviourTree : Node
    {
        private EStatus _currentStatus = EStatus.RUNNING;
        private readonly WaitForSeconds _tickRate;

        public BehaviourTree(string name = "Tree", float? tickRate = null)
        {
            Name = name;
            _tickRate = tickRate.HasValue ? new WaitForSeconds(tickRate.Value) : new WaitForSeconds(Random.Range(0.1f, 0.5f));
        }
        
        

        public override EStatus Process()
        {
            // ReSharper disable once ConvertIfStatementToReturnStatement
            Debug.Log(CurrentChildIndex);
            if (Children.Count == 0) return EStatus.SUCCESS;
            Debug.Log("WOW");
            return Children[CurrentChildIndex].Process();
        }
        
        private struct NodeLevel
        {
            public int Level;
            public Node Node;
        }
        
        public void PrintTree()
        {
            var treePrintOut = "";
            Stack<NodeLevel> nodeStack = new Stack<NodeLevel>();
            var currentNode = this;

            nodeStack.Push(new NodeLevel{Level = 0, Node = currentNode } );

            while (nodeStack.Count != 0)
            {
                var nextNode = nodeStack.Pop();
                treePrintOut += new string('-', nextNode.Level) + nextNode.Node.Name + '\n';
                for (var i = nextNode.Node.Children.Count - 1; i >= 0; i--)
                {   
                    nodeStack.Push(new NodeLevel {Level = nextNode.Level+1, Node = nextNode.Node.Children[i] });
                }   
            }
            
            Debug.Log(treePrintOut);
        }

        public IEnumerator Behave()
        {

            while (true)
            {
                _currentStatus = Process();
                yield return _tickRate;
            }
            
            // ReSharper disable once IteratorNeverReturns
        }
    }
}
