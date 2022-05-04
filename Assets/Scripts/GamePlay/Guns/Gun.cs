using System.Collections;
using UnityEngine;

namespace DenizYanar.Guns
{
    public class Gun : MonoBehaviour
    {
        private bool m_bIsFireEnabled;
        private GunLauncher m_Launcher;
        private GunMagazine m_Magazine;
        private bool m_bHasCooldown;

        [SerializeField]
        private float m_FireCooldown = 1.0f;

        #region Monobehaviour
        
        private void Awake()
        {
            m_Magazine = GetComponentInChildren<GunMagazine>();
            m_Launcher = GetComponentInChildren<GunLauncher>();
        }

        private void Update()
        {
            if (m_bIsFireEnabled) Fire();
        }

        #endregion

        public void Reload() => m_Magazine.StartReload();

        public void StartFire() => m_bIsFireEnabled = true;
        public void StopFire() => m_bIsFireEnabled = false;
        private void Fire()
        {
            if (m_bHasCooldown) return;
            if(m_Magazine.MagazineStatus == GunMagazine.EMagazineStatus.EMPTY) return;
            m_Magazine.SpendAmmo(); 
            m_Launcher.Shot();
            StartCoroutine(StartFireCooldown(m_FireCooldown));
        }

        private IEnumerator StartFireCooldown(float cooldownDuration)
        {
            m_bHasCooldown = true;
            yield return new WaitForSeconds(cooldownDuration);
            m_bHasCooldown = false;
        }
    }
}
