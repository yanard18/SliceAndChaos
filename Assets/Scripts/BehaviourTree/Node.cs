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

        public EStatus Status;
        public List<Node> Children = new List<Node>(); 
        public int CurrentChildIndex = 0;
        public string Name;
        
        public Node() {}

        public Node(string name)
        {
            Name = name;
        }

        public virtual EStatus Process()
        {
            return Children[CurrentChildIndex].Process(); 
        }

        public void AddChild(Node node)
        {
            Children.Add(node);
        }
        

    }
}
