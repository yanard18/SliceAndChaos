using System;
using DenizYanar.BehaviourTreeAI;

namespace DenizYanar
{
    public class Selector : Node
    {
        public Selector(string name)
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
                case EStatus.SUCCESS:
                    CurrentChild = 0;
                    return EStatus.SUCCESS;
                case EStatus.FAILURE:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            CurrentChild++;
            
            // ReSharper disable once InvertIf
            if (CurrentChild >= Children.Count)
            {
                CurrentChild = 0;
                return EStatus.FAILURE;
            }

            return EStatus.RUNNING;
        }
    }
}
