using DenizYanar.YanarPro;
using Sirenix.OdinInspector;
using UnityEngine;

public class Aim2D : MonoBehaviour
{
    private Coroutine m_AimCoroutine;

    private bool m_bHasEnable;
    private float m_FlipOffset;
    private Transform m_TargetToAim;
    
    [SerializeField]
    private bool m_bAimInstant;

    [SerializeField] [ValidateInput("@$value > 0", "Value has to be greater than zero")] [HideIf(nameof(m_bAimInstant))]
    private float m_RotationSpeed = 5.0f;

    public void StartToAim(Transform target)
    {
        m_bHasEnable = true;
        m_TargetToAim = target;
    }

    public void StopToAim() => m_bHasEnable = false;
    public void Flip() => m_FlipOffset = m_FlipOffset == 0 ? 180 : 0;

    private void Update()
    {
        if(!m_bHasEnable) return;
        Aim();
    }

    private void Aim()
    {
        var angle = YanarUtils.FindAngleBetweenTwoPositions(transform.position, m_TargetToAim.position);
        var targetRotation = Quaternion.AngleAxis(angle + m_FlipOffset, Vector3.forward);
        transform.rotation = m_bAimInstant ? targetRotation : Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * m_RotationSpeed);
    }
}