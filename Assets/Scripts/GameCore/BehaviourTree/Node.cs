using System.Collections.Generic;

namespace DenizYanar.BehaviourTreeAI
{
    public class Node
    {
        public enum EStatus
        {
            SUCCESS,
            RUNNING,
            FAILURE
        };

        public EStatus m_Status;
        public readonly List<Node> m_TChildren = new ();
        protected int m_CurrentChildIndex = 0;
        public string m_Name;

        public Node() {}

        public Node(string name)
        {
            m_Name = name;
        }

        public virtual EStatus Process()
        {
            return m_TChildren[m_CurrentChildIndex].Process(); 
        }

        public void AddChild(Node node)
        {
            m_TChildren.Add(node);
        }
        

    }
}
