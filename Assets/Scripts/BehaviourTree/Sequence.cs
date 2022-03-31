using System;
using DenizYanar.BehaviourTreeAI;
using UnityEngine;

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
            var childStatus = Children[CurrentChildIndex].Process();
            switch (childStatus)
            {
                case EStatus.RUNNING:
                    return EStatus.RUNNING;
                case EStatus.FAILURE:
                    return EStatus.FAILURE;
                case EStatus.SUCCESS:
                    CurrentChildIndex++;
            
                    // ReSharper disable once InvertIf
                    if (CurrentChildIndex >= Children.Count)
                    {
                        CurrentChildIndex = 0;
                        return EStatus.SUCCESS;
                    }
                    
                    return EStatus.RUNNING;
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }
    }
}
