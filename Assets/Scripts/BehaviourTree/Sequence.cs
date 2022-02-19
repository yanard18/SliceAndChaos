using DenizYanar.BehaviourTreeAI;

namespace DenizYanar
{
    public class Sequence : Node
    {
        public Sequence(string name)
        {
            Name = name;
        }

        public override EStatus Process()
        {
            var childStatus = Children[CurrentChild].Process();
            if (childStatus == EStatus.RUNNING) return EStatus.RUNNING;
            if (childStatus == EStatus.FAILURE) return childStatus;

            CurrentChild++;
            if (CurrentChild >= Children.Count)
            {
                CurrentChild = 0;
                return EStatus.SUCCESS;
            }
            
            return EStatus.RUNNING;
        }
    }
}
