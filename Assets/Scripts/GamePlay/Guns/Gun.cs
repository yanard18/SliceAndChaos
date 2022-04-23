using UnityEngine;

namespace DenizYanar.Guns
{
    public class Gun : MonoBehaviour
    {
        private bool m_bIsFiring;
        private float m_BackupFireCooldown;
        
        private GunInputReader m_Input;
        private GunLauncher m_Launcher;
        private GunMagazine m_Magazine;

        [SerializeField]
        private float m_FireCooldown = 1.0f;

        #region Monobehaviour
        
        private void OnEnable()
        {
            m_Input.e_OnFireStarted += FireStarted;
            m_Input.e_OnFireCancelled += FireCancelled;
            m_Input.e_OnReload += Reload;
        }

        private void OnDisable()
        {
            m_Input.e_OnFireStarted -= FireStarted;
            m_Input.e_OnFireCancelled -= FireCancelled;
            m_Input.e_OnReload -= Reload;
        }

        private void Awake()
        {
            m_BackupFireCooldown = m_FireCooldown;
            
            m_Input = GetComponentInChildren<GunInputReader>();
            m_Magazine = GetComponentInChildren<GunMagazine>();
            m_Launcher = GetComponentInChildren<GunLauncher>();
        }
        
        private void Update() => FireCycle();
        
        #endregion

        private void FireStarted() => m_bIsFiring = true;

        private void FireCancelled() => m_bIsFiring = false;

        private void Reload() => m_Magazine.Reload();

        private void FireCycle()
        {
            if (m_FireCooldown > 0)
                m_FireCooldown -= Time.deltaTime;
            else
            {
                if (!m_bIsFiring) return;
                if (m_Magazine.SpendAmmo() is false) return;
                m_Launcher.Shot();
                m_FireCooldown = m_BackupFireCooldown;
            }
        }
    }
}
