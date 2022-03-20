using System.Collections;
using UnityEngine;

namespace DenizYanar
{
    
    [RequireComponent(typeof(CircleCollider2D), typeof(PointEffector2D))]
    public class MagnetController : MonoBehaviour
    {
        private Coroutine _impulseCoroutine;
        
        private CircleCollider2D _col;
        private PointEffector2D _effector;

        private MagnetConfigurations _conf;

        private void Awake()
        {
            _col = GetComponent<CircleCollider2D>();
            _effector = GetComponent<PointEffector2D>();
            ActivateMagnet(false);

            _conf = new MagnetConfigurations(EMagnetPolar.PULL, _effector.forceMagnitude, _col.radius);
        }
        
        private void SetMagnet(MagnetConfigurations conf)
        { 
            _effector.forceMagnitude = conf.Polar == EMagnetPolar.PULL ? -Mathf.Abs(conf.Power) : conf.Power;
            _col.radius = conf.Radius;
            _conf = conf;
        }

        public void ImpulseMagnet(EMagnetPolar polar, float impulsePower, float impulseDecay)
        {
            if(_impulseCoroutine is {}) return;
            _impulseCoroutine = StartCoroutine(ImpulseMagnetEnumerator(polar, impulsePower, impulseDecay));
        }

        public void ActivateMagnet(bool value) => _effector.enabled = value;
    
        private IEnumerator ImpulseMagnetEnumerator(EMagnetPolar polar, float impulsePower, float impulseDecay)
        {
            var conf = _conf;
            SetMagnet(new MagnetConfigurations(polar, impulsePower, 15f));
            yield return new WaitForSeconds(impulseDecay);
            SetMagnet(conf);
            
            

                
            
            
            ActivateMagnet(false);
            _impulseCoroutine = null;
        }
        
        
    }
    
    
    
}
