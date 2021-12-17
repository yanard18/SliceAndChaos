using System;

namespace DenizYanar
{
    public class Transition
    {
        public readonly Func<bool> Condition;
        public readonly State To;

        public Transition(State to, Func<bool> condition)
        {
            Condition = condition;
            To = to;
        }
    }
}
