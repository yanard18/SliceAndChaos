using Cinemachine;
using DenizYanar.Inputs;
using DenizYanar.YanarPro;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DenizYanar.PlayerSystem
{
    public class CameraZoomToTarget : MonoBehaviour
    {
        private Vector3 m_DefaultCamOffset;
        private bool m_bIsActive;

        private CinemachineFramingTransposer m_Cam;

        [SerializeField] [Required]
        private PlayerInputs m_PlayerInputs;

        [SerializeField] [ValidateInput("@$value > 0", "Distance has to be greater than zero")]
        [Range(0.1f, 20f)]
        private float m_LookDistance;

        private void OnEnable()
        {
            m_PlayerInputs.e_OnCameraZoomPressed += OnZoomPressed;
            m_PlayerInputs.e_OnCameraZoomCancelled += OnZoomCancelled;
        }

        private void OnDisable()
        {
            m_PlayerInputs.e_OnCameraZoomPressed -= OnZoomPressed;
            m_PlayerInputs.e_OnCameraZoomCancelled -= OnZoomCancelled;
        }

        private void Awake()
        {
            m_Cam = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>();
            m_DefaultCamOffset = m_Cam.m_TrackedObjectOffset;
        }

        private void Update() => HandleCinemachineZoom();

        private void HandleCinemachineZoom()
        {
            if (Player.s_Instance == null) return;
            if (!m_bIsActive) return;

            Vector3 dirToMouse =
                YanarUtils.FindDirectionToMouse(Player.s_Instance.transform.position, m_PlayerInputs.m_MousePosition);

            SetCameraOffset(dirToMouse * m_LookDistance);
        }

        private void SetCameraOffset(Vector3 target) => m_Cam.m_TrackedObjectOffset = m_DefaultCamOffset + target;

        private void OnZoomPressed() => m_bIsActive = true;
        private void OnZoomCancelled()
        {
            m_bIsActive = false;
            SetCameraOffset(m_DefaultCamOffset);
        }
    }
}