using System;
using System.Collections;
using System.Collections.Generic;
using DenizYanar.Attacks;
using DenizYanar.DamageAndHealthSystem;
using DenizYanar.SenseEngine;
using DenizYanar.FSM;
using DenizYanar.Inputs;
using DenizYanar.YanarPro;
using UnityEngine;

namespace DenizYanar.PlayerSystem.Attacks
{
    public class SliceState : State
    {
        private readonly PlayerAttackController m_PlayerAttackController;
        private readonly PlayerConfigurations m_PlayerConfigurations;

        private readonly PlayerInputs m_PlayerInputs;
        private readonly Action<float> m_FAttackCooldown;
        private readonly IAttack m_IAttack;
        private readonly Rigidbody2D m_Rb;
        private readonly SenseEnginePlayer m_sepAttack;
        private readonly SenseEnginePlayer m_sepHit;


        #region Constructor

        public SliceState
        (
            PlayerAttackController playerAttackController,
            PlayerConfigurations playerConfigurations,
            PlayerInputs playerInput,
            IAttack iAttack,
            Action<float> fAttackCooldown,
            Rigidbody2D rb,
            SenseEnginePlayer sepAttackSense,
            SenseEnginePlayer sepHitSense
        )

        {
            m_PlayerAttackController = playerAttackController;
            m_PlayerConfigurations = playerConfigurations;
            m_PlayerInputs = playerInput;
            m_FAttackCooldown = fAttackCooldown;
            m_IAttack = iAttack;
            m_Rb = rb;
            m_sepAttack = sepAttackSense;
            m_sepHit = sepHitSense;
        }

        #endregion

        #region State Callbacks

        public override void OnEnter()
        {
            base.OnEnter();

            if (!YanarUtils.IsMainCameraExist()) return;

            Attack();
        }

        private void Attack()
        {
            m_FAttackCooldown.Invoke(m_PlayerConfigurations.AttackCooldownDuration);

            List<DamageResult> TDamageResults = m_IAttack.Attack();
            Vector2 playerPosition = m_PlayerAttackController.transform.position;
            var attackDir = YanarUtils.FindDirectionToMouse(playerPosition, m_PlayerInputs.m_MousePosition);

            PlayOnHitSense(TDamageResults, playerPosition, attackDir);
            PushPlayerAlongAttackDir(attackDir);
        }

        #endregion

        #region Local Methods

        private void PushPlayerAlongAttackDir(Vector2 attackDir) =>
            m_Rb.velocity = attackDir * m_PlayerConfigurations.AttackPushForce;

        private void PlayOnHitSense(ICollection TDamageResults, Vector2 playerPosition,
            Vector2 attackDir)
        {
            m_sepAttack.Play();

            if (TDamageResults.Count > 0) m_sepHit.PlayHitSEP(playerPosition, attackDir);
        }

        #endregion
    }
}