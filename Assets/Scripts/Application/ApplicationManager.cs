using UnityEngine;

namespace DenizYanar
{
    public class ApplicationManager : MonoBehaviour
    {
        [SerializeField] private int m_TargetFrameRate = 300;

        private void Awake()
        {
            Application.targetFrameRate = m_TargetFrameRate;
        }
    }
}
