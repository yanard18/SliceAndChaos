namespace DenizYanar.BehaviourTreeAI
{
    public class Leaf : Node
    {
        public delegate EStatus Tick();
        private readonly Tick _processMethod;

        public Leaf() { }

        public Leaf(string name, Tick processMethod)
        {
            Name = name;
            _processMethod = processMethod;
        }
        
        public override EStatus Process()
        {
            return _processMethod?.Invoke() ?? EStatus.FAILURE;
        }

    }
}
