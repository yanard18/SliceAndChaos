using System;
using DenizYanar.BehaviourTreeAI;

namespace DenizYanar
{
    public class Selector : Node
    {
        public Selector(string name)
        {
            m_Name = name;
        }

        public override EStatus Process()
        {
            var currentChildStatus = m_TChildren[m_CurrentChildIndex].Process();
            switch (currentChildStatus)
            {
                case EStatus.RUNNING:
                    return EStatus.RUNNING;
                case EStatus.SUCCESS:
                    m_CurrentChildIndex = 0;
                    return EStatus.SUCCESS;
                case EStatus.FAILURE:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            m_CurrentChildIndex++;
            
            // ReSharper disable once InvertIf
            if (m_CurrentChildIndex >= m_TChildren.Count)
            {
                m_CurrentChildIndex = 0;
                return EStatus.FAILURE;
            }

            return EStatus.RUNNING;
        }
    }
}
