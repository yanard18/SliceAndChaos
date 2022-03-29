using System;
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
            switch (childStatus)
            {
                case EStatus.RUNNING:
                    return EStatus.RUNNING;
                case EStatus.FAILURE:
                    return EStatus.FAILURE;
                case EStatus.SUCCESS:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            CurrentChild++;
            
            // ReSharper disable once InvertIf
            if (CurrentChild >= Children.Count)
            {
                CurrentChild = 0;
                return EStatus.SUCCESS;
            }
            
            return EStatus.RUNNING;
        }
    }
}
