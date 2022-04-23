using System;

namespace DenizYanar.FSM
{
    public class Transition
    {
        public readonly Func<bool> m_Condition;
        public readonly State m_To;

        public Transition(State to, Func<bool> condition)
        {
            m_Condition = condition;
            m_To = to;
        }
    }
}
