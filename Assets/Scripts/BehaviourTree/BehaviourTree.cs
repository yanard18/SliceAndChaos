using System.Collections.Generic;
using UnityEngine;

namespace DenizYanar.BehaviourTreeAI
{
    public class BehaviourTree : Node
    {
        public BehaviourTree()
        {
            Name = "Tree";
        }

        public BehaviourTree(string name)
        {
            Name = name;
        }

        public override EStatus Process()
        {
            return Children[CurrentChild].Process();
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
    }
}
