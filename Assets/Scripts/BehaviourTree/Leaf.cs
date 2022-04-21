namespace DenizYanar.BehaviourTreeAI
{
    public class Leaf : Node
    {
        public delegate EStatus Tick();
        private readonly Tick m_ProcessMethod;

        public Leaf() { }

        public Leaf(string name, Tick processMethod)
        {
            m_Name = name;
            m_ProcessMethod = processMethod;
        }
        
        public override EStatus Process()
        {
            return m_ProcessMethod?.Invoke() ?? EStatus.FAILURE;
        }

    }
}
