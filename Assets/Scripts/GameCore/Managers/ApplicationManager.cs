using UnityEngine;

namespace DenizYanar
{
    public class ApplicationManager : MonoBehaviour
    {
        [SerializeField]
        private int m_TargetFrameRate = 300;

        [SerializeField]
        private bool m_bIsCursorVisible;

        [SerializeField]
        private bool m_bIsCursorLocked;

        private void Awake()
        {
            Application.targetFrameRate = m_TargetFrameRate;
            Cursor.visible = m_bIsCursorVisible;
            Cursor.lockState = m_bIsCursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }
}