using System;
using System.Collections;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace DenizYanar
{
    [RequireComponent(typeof(Projectile))]
    public class KatanaProjectile : MonoBehaviour
    {
        private Projectile _projectile;


        private void Awake()
        {
            _projectile = GetComponent<Projectile>();
        }
        
        private void OnEnable()
        {
            _projectile.OnHit += Hit;
        }

        private void OnDisable()
        {
            _projectile.OnHit -= Hit;
        }
        

        private void Hit()
        {
            _projectile.Stop();
        }

        public void CallbackKatana()
        {
            StartCoroutine(KatanaCallback(0.2f));
        }

        private IEnumerator KatanaCallback(float callbackDuration)
        {
            float elapsedTime = 0;
            Vector3 startPos = transform.position;
            
            while (elapsedTime < callbackDuration)
            {
                elapsedTime += Time.deltaTime;
                transform.Rotate(Vector3.forward * (Time.deltaTime * 2200f));
                transform.position = Vector3.Lerp(startPos, _projectile.Author.transform.position, elapsedTime / callbackDuration);
                yield return null;
            }
            
            Destroy(gameObject);
        }
    }
}
