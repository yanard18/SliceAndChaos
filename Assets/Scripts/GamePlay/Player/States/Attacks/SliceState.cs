using System;
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
        private readonly Action<float> m_fAttackCooldown;
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
            m_fAttackCooldown = fAttackCooldown;
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

            // Check is camera exist
            if (Camera.main is null) return;

            // Start attack cooldown
            m_fAttackCooldown.Invoke(m_PlayerConfigurations.AttackCooldownDuration);

            // Calculate attack direction
            Vector2 playerPosition = m_PlayerAttackController.transform.position;

            var attackDir =
                YanarUtils.FindDirectionBetweenPositionAndMouse(playerPosition, m_PlayerInputs.m_MousePosition);

#if UNITY_EDITOR
            Debug.DrawRay(playerPosition, attackDir, Color.green, 5.0f);
#endif

            // Push player
            PushPlayerAlongAttackDir(attackDir);
            m_DamageArea.CreateArea(new Damage(m_PlayerConfigurations.AttackDamage, m_PlayerAttackController.transform.root.gameObject));


            m_sepAttack.Play();


        }

        #endregion

        #region Local Methods

        private void PushPlayerAlongAttackDir(Vector2 attackDir)
        {
            m_Rb.AddForce(attackDir * m_PlayerConfigurations.AttackPushForce, ForceMode2D.Impulse);
        }

        
        
        private void PlayOnHitSense(Vector3 playerPosition, Vector2 attackDir)
        {
            var spawner = m_sepHit.GetComponent<SenseInstantiateObject>();
            spawner.InstantiatePosition = playerPosition + (Vector3) attackDir * -10f;
            var angle = Mathf.Atan2(attackDir.y, attackDir.x) * Mathf.Rad2Deg;
            spawner.InstantiateRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            m_sepHit.Play();
        }

        

        #endregion
    }
}