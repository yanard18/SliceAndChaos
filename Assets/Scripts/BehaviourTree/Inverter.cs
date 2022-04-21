using System;

namespace DenizYanar.BehaviourTreeAI
{
    public class Inverter : Node
    {
        public Inverter(string name)
        {
            m_Name = name;
        }
        
        public override EStatus Process()
        {
            var currentChildStatus = m_TChildren[0].Process();
            return currentChildStatus switch
            {
                EStatus.RUNNING => EStatus.RUNNING,
                EStatus.FAILURE => EStatus.SUCCESS,
                EStatus.SUCCESS => EStatus.FAILURE,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

    }
}
