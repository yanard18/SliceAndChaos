using DenizYanar.Events;
using JetBrains.Annotations;
using UnityEngine;
using DenizYanar.FSM;
using DenizYanar.Inputs;
using DenizYanar.Movement;

namespace DenizYanar.PlayerSystem.Movement
{
    public class AirState : State
    {

        private readonly PlayerInputs m_Inputs;

        private readonly IMove m_iHorizontalMovement;
        private readonly IMove m_iVerticalMovement;

        private bool m_Dive;

        #region Constructor
        
        
        public AirState(PlayerInputs inputs, IMove iHorizontalMovement, IMove iVerticalMovement, StringEvent nameInformerChannel = null, [CanBeNull] string stateName = null)
        {
            m_StateName = stateName ?? GetType().Name;
            m_ecStateName = nameInformerChannel;
            m_Inputs = inputs;
            m_iHorizontalMovement = iHorizontalMovement;
            m_iVerticalMovement = iVerticalMovement;
        }

        #endregion
        
        #region State Callbacks

        public override void OnEnter()
        {
            base.OnEnter();
            m_Inputs.e_OnDiveStarted += OnDiveStarted;
            m_Inputs.e_OnDiveCancelled += OnDiveCancelled;
        }

        public override void OnExit()
        {
            m_Inputs.e_OnDiveStarted -= OnDiveStarted;
            m_Inputs.e_OnDiveCancelled -= OnDiveCancelled;
            m_Dive = false;
        }

        public override void PhysicsTick()
        {
            base.PhysicsTick();
            
            HandleAirStrafe();
            HandleDive();
        }

        private void HandleAirStrafe()
        {
            m_iHorizontalMovement.Move(m_Inputs.m_HorizontalMovement * Vector2.right);
        }

        private void HandleDive()
        {
            if(PressedToDiveKey())
                m_iVerticalMovement.Move(Vector2.down);
        }

        private bool PressedToDiveKey() => m_Dive;

        #endregion


        private void OnDiveStarted() => m_Dive = true;
        private void OnDiveCancelled() => m_Dive = false;

    }
}
