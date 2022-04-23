using System.Collections;
using UnityEngine;

namespace DenizYanar.Turret
{
    public class TurretBrain : MonoBehaviour
    {
        private enum ETurretState
        {
            PASSIVE,
            PATROL,
            ATTACK
        };

        private ETurretState m_TurretState;


        private Transform m_Target;
        
        private TurretGunInputReader m_GunInputReader;
        private TurretLaserSensor m_LaserSensor;
        private TurretRotor m_Rotor;
        private TurretTargetSensor m_TargetSensor;

        #region Monobehaviour

        private void Awake()
        {
            m_LaserSensor = GetComponentInChildren<TurretLaserSensor>();
            m_TargetSensor = GetComponentInChildren<TurretTargetSensor>();
            m_GunInputReader = GetComponentInChildren<TurretGunInputReader>();
            m_Rotor = GetComponentInChildren<TurretRotor>();
        }
        
        private void Update() => Tick();

        #endregion

        private void Tick()
        {
            if (m_LaserSensor.HandleDetection() && m_TurretState != ETurretState.ATTACK)
            {
                m_TurretState = ETurretState.ATTACK;
                m_GunInputReader.InvokeOnFireStarted();
            }
            else if (m_LaserSensor.HandleDetection() is false && m_TurretState == ETurretState.ATTACK)
            {
                m_TurretState = ETurretState.PATROL;
                m_GunInputReader.InvokeOnFireCancelled();
            }

            var target = m_TargetSensor.Detect();
            if(target is {})
                m_Rotor.LookPosition(target.position);

        }

        

        private IEnumerator PrepareForAttack(float duration)
        {
            //Play sound effect
            yield return new WaitForSeconds(duration);
            m_GunInputReader.InvokeOnFireStarted();
        }
    }
}
