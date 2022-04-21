using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DenizYanar.BehaviourTreeAI
{
    public class BehaviourTree : Node
    {
        private EStatus m_CurrentStatus = EStatus.RUNNING;
        public readonly float m_TickRate;

        public BehaviourTree(string name = "Tree", float? tickRate = null)
        {
            m_Name = name;
            m_TickRate = tickRate ?? Random.Range(0.1f, 0.5f);
        }
        
        

        public override EStatus Process()
        {
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (m_TChildren.Count == 0) return EStatus.SUCCESS;
            return m_TChildren[m_CurrentChildIndex].Process();
        }
        
        private struct NodeLevel
        {
            public int m_Level;
            public Node m_Node;
        }
        
        public void PrintTree()
        {
            var treePrintOut = "";
            Stack<NodeLevel> nodeStack = new ();
            var currentNode = this;

            nodeStack.Push(new NodeLevel{m_Level = 0, m_Node = currentNode } );

            while (nodeStack.Count != 0)
            {
                var nextNode = nodeStack.Pop();
                treePrintOut += new string('-', nextNode.m_Level) + nextNode.m_Node.m_Name + '\n';
                for (var i = nextNode.m_Node.m_TChildren.Count - 1; i >= 0; i--)
                {   
                    nodeStack.Push(new NodeLevel {m_Level = nextNode.m_Level+1, m_Node = nextNode.m_Node.m_TChildren[i] });
                }   
            }
            
            Debug.Log(treePrintOut);
        }

        public IEnumerator Behave()
        {

            while (true)
            {
                m_CurrentStatus = Process();
                yield return new WaitForSeconds(m_TickRate);
            }
            
            // ReSharper disable once IteratorNeverReturns
        }
    }
}
