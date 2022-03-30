using DenizYanar.PlayerSystem;
using UnityEngine;

namespace DenizYanar
{
    public class PlayerMagnetController : MonoBehaviour
    {
        [SerializeField] private PlayerInputs _inputs;
        [SerializeField] private PlayerSettings _settings;

        private Magnet _magnet;
        private KatanaProjectile _katanaProjectile;
        private bool _readyForAction;

        
        
        public void SetMagnetController(Magnet m)
        {
            _magnet = m;

            var conf = new MagnetConfigurations(
                EMagnetPolar.PULL,
                _settings.MagnetPullStrength,
                _settings.MagnetAffectRadius,
                _settings.MagnetPullDistanceScale
                );
            
            _magnet.SetMagnet(conf);
        }

        public void SetKatana(KatanaProjectile k) => _katanaProjectile = k;

        public void SetReadyToUse(bool value) => _readyForAction = value;
        

        private void OnEnable()
        {
            _inputs.OnMagnetPullStarted += OnMagnetPullInputStarted;
            _inputs.OnMagnetPullCancelled += OnMagnetPullInputReleased;
            _inputs.OnMagnetPushPressed += OnMagnetPushInputPressed;
        }
        
        private void OnDisable()
        {
            _inputs.OnMagnetPullStarted -= OnMagnetPullInputStarted;
            _inputs.OnMagnetPullCancelled -= OnMagnetPullInputReleased;
            _inputs.OnMagnetPushPressed -= OnMagnetPushInputPressed;
        }

        private void OnMagnetPullInputStarted()
        {
            if(_magnet == null) return;
            _magnet.ActivateMagnet(true);
        }

        private void OnMagnetPullInputReleased()
        {
            if(!_readyForAction) return;
            if(_magnet == null) return;
            _magnet.ActivateMagnet(false);
        }

        private void OnMagnetPushInputPressed()
        {
            if(!_readyForAction) return; 
            if(_magnet == null) return;
            _magnet.ImpulseMagnet(EMagnetPolar.PUSH, _settings.MagnetPushStrength, _settings.MagnetPushDistanceScale, 0.05f);
            _katanaProjectile.CallbackKatana();
            SetReadyToUse(false);
        }
    }
}
