using DenizYanar.Sensors;
using UnityEngine;

namespace DenizYanar.EnemySystem
{
    public class HumanoidDetection : MonoBehaviour
    {
        public ISensor m_RememberedLocationSensor;
        public ISensor m_DetectionSensor;
        public ISensor m_AttackRangeSensor;

        private void Awake()
        {
            ISensor[] sensors = GetComponents<ISensor>();
            m_RememberedLocationSensor = sensors[0];
            m_DetectionSensor = sensors[1];
            m_AttackRangeSensor = sensors[2];
        }
    }
}