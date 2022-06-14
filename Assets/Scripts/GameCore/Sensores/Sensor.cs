using UnityEngine;

namespace DenizYanar.Sensors
{
    public abstract class Sensor : MonoBehaviour
    {
        public abstract Transform Scan();
    }
}