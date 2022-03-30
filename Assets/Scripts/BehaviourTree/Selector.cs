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
            var currentChildStatus = Children[CurrentChildIndex].Process();
            switch (currentChildStatus)
            {
                case EStatus.RUNNING:
                    return EStatus.RUNNING;
                case EStatus.SUCCESS:
                    CurrentChildIndex = 0;
                    return EStatus.SUCCESS;
                case EStatus.FAILURE:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            CurrentChildIndex++;
            
            // ReSharper disable once InvertIf
            if (CurrentChildIndex >= Children.Count)
            {
                CurrentChildIndex = 0;
                return EStatus.FAILURE;
            }

            return EStatus.RUNNING;
        }
    }
}
