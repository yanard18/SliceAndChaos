using System.Collections;
using DenizYanar.Player;
using UnityEngine;

namespace DenizYanar
{
    [RequireComponent(typeof(Projectile))]
    public class KatanaProjectile : MonoBehaviour
    {
        private Projectile _projectile;

        [SerializeField] private float _turnBackDuration = 0.3f; 
        
        #region Monobehaviour
        
        private void Awake() => _projectile = GetComponent<Projectile>();
        private void OnEnable() => _projectile.OnHit += Hit;
        private void OnDisable() => _projectile.OnHit -= Hit;
        

        #endregion

        #region Global Methods
        
        public void CallbackKatana() => StartCoroutine(KatanaCallback(_turnBackDuration));

        #endregion

        #region Local Methods
        private void Hit(Collider2D other) => _projectile.Stop();

        private IEnumerator KatanaCallback(float turnBackDuration)
        {
            float elapsedTime = 0;
            var startPos = transform.position;
            
            while (elapsedTime < turnBackDuration)
            {
                elapsedTime += Time.deltaTime;
                transform.Rotate(Vector3.forward * (Time.deltaTime * 2200f));
                transform.position = Vector3.Lerp(startPos, _projectile.Author.transform.position, elapsedTime / turnBackDuration);
                yield return null;
            }

            _projectile.Author.GetComponent<PlayerAttackController>().IsSwordTurnedBack = true;
            Destroy(gameObject);
        }

        #endregion
        
    }
}
