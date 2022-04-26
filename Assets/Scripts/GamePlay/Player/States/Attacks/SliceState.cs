using System;
using System.Collections;
using System.Collections.Generic;
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
        private readonly Rigidbody2D m_Rb;
        private readonly IDamageArea m_DamageArea;
        private readonly SenseEnginePlayer m_sepAttack;
        private readonly SenseEnginePlayer m_sepHit;


        #region Constructor

        public SliceState
        (
            PlayerAttackController playerAttackController,
            PlayerConfigurations playerConfigurations,
            PlayerInputs playerInput,
            Action<float> fAttackCooldown,
            Rigidbody2D rb,
            IDamageArea damageArea,
            SenseEnginePlayer sepAttackSense,
            SenseEnginePlayer sepHitSense
        )

        {
            m_PlayerAttackController = playerAttackController;
            m_PlayerConfigurations = playerConfigurations;
            m_PlayerInputs = playerInput;
            m_FAttackCooldown = fAttackCooldown;
            m_Rb = rb;
            m_DamageArea = damageArea;
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

            Vector2 playerPosition = m_PlayerAttackController.transform.position;
            var attackDir = YanarUtils.FindDirectionToMouse(playerPosition, m_PlayerInputs.m_MousePosition);

            YanarDebugs.DrawRayInEditor(playerPosition, attackDir, Color.green, 5.0f);

            var damage = new Damage(
                m_PlayerConfigurations.AttackDamage,
                m_PlayerAttackController.transform.root.gameObject
            );

            List<DamageResult> TDamageResults = m_DamageArea.CreateArea(damage);


            PlaySenseEnginePlayers(TDamageResults, playerPosition, attackDir);

            PushPlayerAlongAttackDir(attackDir);
        }

        #endregion

        #region Local Methods

        private void PushPlayerAlongAttackDir(Vector2 attackDir) =>
            m_Rb.velocity = attackDir * m_PlayerConfigurations.AttackPushForce;

        private void PlaySenseEnginePlayers(ICollection TDamageResults, Vector2 playerPosition,
            Vector2 attackDir)
        {
            m_sepAttack.Play();

            if (TDamageResults.Count > 0) m_sepHit.PlayHitSEP(playerPosition, attackDir);
        }

        #endregion
    }
}