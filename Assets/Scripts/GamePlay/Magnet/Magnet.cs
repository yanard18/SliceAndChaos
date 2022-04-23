using System.Collections;
using DenizYanar.SenseEngine;
using UnityEngine;

namespace DenizYanar
{
    [RequireComponent(typeof(CircleCollider2D), typeof(PointEffector2D))]
    public class Magnet : MonoBehaviour
    {
        private Coroutine m_ImpulseCoroutine;

        private CircleCollider2D m_Col;
        private PointEffector2D m_Effector;
        private MagnetConfigurations m_Conf;

        private bool m_bHasUsageCooldown;

        [SerializeField]
        private SenseEnginePlayer m_sepMagnetActivate;
        
        [SerializeField]
        private SenseEnginePlayer m_sepMagnetDeactivate;
        
        [SerializeField]
        private SenseEnginePlayer m_sepMagnetPushImpulse;

        
        private void Awake()
        {
            m_Col = GetComponent<CircleCollider2D>();
            m_Effector = GetComponent<PointEffector2D>();
            ActivateMagnet(false);

            m_Conf = new MagnetConfigurations(EMagnetPolar.PULL, m_Effector.forceMagnitude, m_Col.radius,
                m_Effector.distanceScale);
        }

        public void SetMagnet(MagnetConfigurations conf)
        {
            m_Effector.forceMagnitude = conf.m_Polar == EMagnetPolar.PULL ? -Mathf.Abs(conf.m_Power) : conf.m_Power;
            m_Col.radius = conf.m_Radius;
            m_Effector.distanceScale = conf.m_DistanceScale;
            m_Conf = conf;
        }

        public void ImpulseMagnet(EMagnetPolar polar, float impulsePower, float distanceScale, float impulseDecay,
            float usageCooldown = 1.0f)
        {
            if (m_ImpulseCoroutine is { }) return;
            m_ImpulseCoroutine =
                StartCoroutine(ImpulseMagnetEnumerator(polar, impulsePower, distanceScale, impulseDecay,
                    usageCooldown));
        }

        public void ActivateMagnet(bool value)
        {
            if (value && m_bHasUsageCooldown) return;

            m_Effector.enabled = value;
            PlaySenseEffects();


            void PlaySenseEffects()
            {
                if (value)
                {
                    if (m_sepMagnetActivate != null)
                        m_sepMagnetActivate.Play();
                }
                else
                {
                    if (m_sepMagnetDeactivate != null)
                        m_sepMagnetDeactivate.Play();
                }
            }
        }

        private IEnumerator ImpulseMagnetEnumerator(EMagnetPolar polar, float impulsePower, float distanceScale,
            float impulseDecay, float usageCooldown)
        {
            var conf = m_Conf;
            SetMagnet(new MagnetConfigurations(polar, impulsePower, conf.m_Radius, distanceScale));
            ActivateMagnet(true);
            StartCoroutine(StartUsageCooldown(usageCooldown));
            PlayEffect(m_sepMagnetPushImpulse);
            yield return new WaitForSeconds(impulseDecay);
            SetMagnet(conf);
            ActivateMagnet(false);

            m_ImpulseCoroutine = null;
        }

        private IEnumerator StartUsageCooldown(float cooldownDuration)
        {
            m_bHasUsageCooldown = true;
            yield return new WaitForSeconds(cooldownDuration);
            m_bHasUsageCooldown = false;
        }

        private void PlayEffect(SenseEnginePlayer player)
        {
            if (player != null)
                player.Play();
        }
    }
}