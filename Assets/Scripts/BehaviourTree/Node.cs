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
        public int CurrentChild = 0;
        public string Name;
        
        public Node() {}

        public Node(string name)
        {
            Name = name;
        }

        public virtual EStatus Process()
        {
            return Children[CurrentChild].Process(); 
        }

        public void AddChild(Node node)
        {
            Children.Add(node);
        }
        

    }
}
