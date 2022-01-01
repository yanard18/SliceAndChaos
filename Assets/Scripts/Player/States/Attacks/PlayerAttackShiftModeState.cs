using DenizYanar.FSM;
using UnityEngine;

namespace DenizYanar
{
    public class PlayerAttackShiftModeState : State
    {
        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("SHIFT IS WORKING!");
        }
    }
}