using DenizYanar.Inputs;
using DenizYanar.PlayerSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DenizYanar
{
    public class PlayerMagnetController : MonoBehaviour
    {
        [SerializeField] [Required]
        private PlayerInputs m_Inputs;
        
        [SerializeField] [Required]
        private PlayerSettings m_Settings;

        private Magnet m_Magnet;
        private KatanaProjectile m_KatanaProjectile;
        private bool m_bIsReadyForAction;

        
        
        public void SetMagnetController(Magnet m)
        {
            m_Magnet = m;

            var conf = new MagnetConfigurations(
                EMagnetPolar.PULL,
                m_Settings.MagnetPullStrength,
                m_Settings.MagnetAffectRadius,
                m_Settings.MagnetPullDistanceScale
                );
            
            m_Magnet.SetMagnet(conf);
        }

        public void SetKatana(KatanaProjectile k) => m_KatanaProjectile = k;

        public void SetReadyToUse(bool value) => m_bIsReadyForAction = value;
        

        private void OnEnable()
        {
            m_Inputs.e_OnMagnetPullStarted += OnMagnetPullInputStarted;
            m_Inputs.e_OnMagnetPullCancelled += OnMagnetPullInputReleased;
            m_Inputs.e_OnMagnetPushPressed += OnMagnetPushInputPressed;
        }
        
        private void OnDisable()
        {
            m_Inputs.e_OnMagnetPullStarted -= OnMagnetPullInputStarted;
            m_Inputs.e_OnMagnetPullCancelled -= OnMagnetPullInputReleased;
            m_Inputs.e_OnMagnetPushPressed -= OnMagnetPushInputPressed;
        }

        private void OnMagnetPullInputStarted()
        {
            if(m_Magnet == null) return;
            m_Magnet.ActivateMagnet(true);
        }

        private void OnMagnetPullInputReleased()
        {
            if(!m_bIsReadyForAction) return;
            if(m_Magnet == null) return;
            m_Magnet.ActivateMagnet(false);
        }

        private void OnMagnetPushInputPressed()
        {
            if(!m_bIsReadyForAction) return; 
            if(m_Magnet == null) return;
            m_Magnet.ImpulseMagnet(EMagnetPolar.PUSH, m_Settings.MagnetPushStrength, m_Settings.MagnetPushDistanceScale, 0.05f);
            m_KatanaProjectile.CallbackKatana();
            SetReadyToUse(false);
        }
    }
}
