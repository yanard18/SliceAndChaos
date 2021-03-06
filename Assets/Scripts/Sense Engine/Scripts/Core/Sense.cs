using UnityEngine;

namespace DenizYanar.SenseEngine
{
    [ExecuteAlways]
    public abstract class Sense : MonoBehaviour
    {
        [Header("General Sense Configurations")]

        public string Label = string.Empty;
        public Color Color = new Color(0.1f, 0.1f, 0.1f, 1f);

        public abstract void Play();
    }
}
