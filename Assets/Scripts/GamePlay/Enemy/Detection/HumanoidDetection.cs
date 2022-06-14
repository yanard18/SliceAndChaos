using DenizYanar.Sensors;
using UnityEngine;

namespace DenizYanar.EnemySystem
{
    public class HumanoidDetection : MonoBehaviour
    {
        public Sensor m_RememberedLocationSensor;
        public Sensor m_DetectionSensor;
        public Sensor m_AttackRangeSensor;

        private void Awake()
        {
            Sensor[] sensors = GetComponents<Sensor>();
            m_RememberedLocationSensor = sensors[0];
            m_DetectionSensor = sensors[1];
            m_AttackRangeSensor = sensors[2];
        }
    }
}