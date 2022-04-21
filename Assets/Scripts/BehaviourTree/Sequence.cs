using System;
using DenizYanar.BehaviourTreeAI;

namespace DenizYanar
{
    public class Sequence : Node
    {
        public Sequence(string name)
        {
            m_Name = name;
        }

        public override EStatus Process()
        {
            var childStatus = m_TChildren[m_CurrentChildIndex].Process();
            switch (childStatus)
            {
                case EStatus.RUNNING:
                    return EStatus.RUNNING;
                case EStatus.FAILURE:
                    return EStatus.FAILURE;
                case EStatus.SUCCESS:
                    m_CurrentChildIndex++;
            
                    // ReSharper disable once InvertIf
                    if (m_CurrentChildIndex >= m_TChildren.Count)
                    {
                        m_CurrentChildIndex = 0;
                        return EStatus.SUCCESS;
                    }
                    
                    return EStatus.RUNNING;
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }
    }
}
