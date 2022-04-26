using System;
using System.Collections;
using DenizYanar.DamageAndHealthSystem;
using DenizYanar.SenseEngine;
using UnityEngine;
using DenizYanar.FSM;
using DenizYanar.Inputs;
using Sirenix.OdinInspector;

namespace DenizYanar.PlayerSystem.Attacks
{
    public class PlayerAttackController : MonoBehaviour
    {
        #region Private Variables

        private StateMachine m_StateMachine;
        private Rigidbody2D m_Rb;
        private bool m_bHasAttackCooldown; 
        
        private SliceState m_sSlash;
        private ThrowSwordState m_sThrow;
        private WaitSwordState m_sWait;
        private IdleState m_sIdle;
        
        #endregion

        #region Serialized Variables
    
        [SerializeField] [Required]
        private PlayerConfigurations m_Configurations;
        
        [SerializeField] [Required]
        private PlayerInputs m_Inputs;

        [Header("Slash Sense Players")]
        
        [SerializeField]
        private SenseEnginePlayer m_sepAttack;
        
        [SerializeField]    
        private SenseEnginePlayer m_sepHit;

        #endregion
        
        #region Public Variables

        #endregion


        #region Monobehaviour

        private void OnEnable()
        {
            m_Inputs.e_OnAttack1Started += OnAttack1Pressed;
            m_Inputs.e_OnAttack2Started += OnAttack2Pressed;
        }

        private void OnDisable()
        {
            m_Inputs.e_OnAttack1Started -= OnAttack1Pressed;
            m_Inputs.e_OnAttack2Started -= OnAttack2Pressed;
        }

        private void Awake()
        {
            m_StateMachine = new StateMachine();
            m_Rb = GetComponent<Rigidbody2D>();

            var damageArea = new VisionDamageArea(m_Inputs, m_Configurations, this);

            m_sIdle = new IdleState();
            m_sSlash = new SliceState(this, m_Configurations, m_Inputs, CreateAttackCooldown, m_Rb, damageArea, m_sepAttack, m_sepHit);
            m_sThrow = new ThrowSwordState(ThrowKatana, OnSwordCalled, OnSwordReturned, transform, m_Configurations, m_Inputs);
            m_sWait = new WaitSwordState();

            m_StateMachine.InitState(m_sIdle);
            
            To(m_sIdle,m_sSlash,() => false);
            To(m_sIdle, m_sThrow, () => false);
            To(m_sThrow, m_sWait, () => false);
            To(m_sWait, m_sIdle, () => false);
            To(m_sSlash, m_sIdle, HasNotAttackCooldown());
            
            void To(State from, State to, Func<bool> condition) => m_StateMachine.AddTransition(from, to, condition);
            
            Func<bool> HasNotAttackCooldown() => () => !m_bHasAttackCooldown;
        }

        private void Update() => m_StateMachine.Tick();

        #endregion

        #region Inputs

        private void OnAttack1Pressed() => m_StateMachine.TriggerState(m_sSlash);

        private void OnAttack2Pressed()
        {
           /* if(_stateMachine.TriggerState(_wait))
                return;*/

            m_StateMachine.TriggerState(m_sThrow);
        }

        private void OnSwordCalled() => m_StateMachine.TriggerState(m_sWait);

        private void OnSwordReturned() => m_StateMachine.TriggerState(m_sIdle);

        #endregion
        
        #region Method Referances
        
        private KatanaProjectile ThrowKatana(Vector2 dir, float throwSpeed, float angularVelocity)
        {
            var p = Instantiate(m_Configurations.SwordProjectile, transform.position, Quaternion.identity);
            p.Init(dir.normalized * throwSpeed, angularVelocity: angularVelocity, author: gameObject, lifeTime: 0f);
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            p.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            return p.GetComponent<KatanaProjectile>();
        }

        private void CreateAttackCooldown(float duration) => StartCoroutine(AttackCooldownCoroutine(duration));
        
        private IEnumerator AttackCooldownCoroutine(float duration)
        {
            m_bHasAttackCooldown = true;
            yield return new WaitForSeconds(duration);
            m_bHasAttackCooldown = false;
        }

        #endregion
        
        

    }
}
