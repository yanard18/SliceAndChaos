using System;
using System.Collections;
using DenizYanar.Player;
using UnityEngine;
using DenizYanar.Projectiles;

namespace DenizYanar
{
    public class KatanaProjectile : Projectile
    {
        private Magnet _magnet;
        
        [SerializeField] private float _turnBackDuration = 0.3f;

        private Action _onSwordCalled;
        private Action _onSwordReturned;


        #region Global Methods

        public void CallbackKatana() => StartCoroutine(KatanaCallback(_turnBackDuration));

        public void SetOnSwordCalled(Action a) => _onSwordCalled = a;
        public void SetOnSwordReturned(Action a) => _onSwordReturned = a;


        #endregion
        
        #region Monobehaviour

        protected override void Awake()
        {
            base.Awake();
            FindMagnetController();
        }
        private void Start() => ConfigurePlayerInputForMagnet();

        #endregion

        #region Local Methods

        private void FindMagnetController()
        {
            var magnet = GetComponentInChildren<Magnet>();

            _magnet = magnet;
        }


        private void ConfigurePlayerInputForMagnet()
        {
            var magnetPlayerController = Author.GetComponent<PlayerMagnetController>();
            magnetPlayerController.SetMagnetController(_magnet);
            magnetPlayerController.SetKatana(this);
            magnetPlayerController.SetReadyToUse(true);
        }

        private IEnumerator KatanaCallback(float turnBackDuration)
        {
            _onSwordCalled?.Invoke();
            float elapsedTime = 0;
            var startPos = transform.position;


            while (elapsedTime < turnBackDuration)
            {
                elapsedTime += Time.deltaTime;
                transform.Rotate(Vector3.forward * (Time.deltaTime * 2200f));
                transform.position = Vector3.Lerp(startPos, Author.transform.position,
                    elapsedTime / turnBackDuration);
                yield return null;
            }

            
            _onSwordReturned?.Invoke();
            
            Destroy(gameObject);
        }

        #endregion
        
        protected override void Hit(Collider2D col)
        {
            StopProjectile();

            if (col.gameObject.isStatic is false)
                transform.SetParent(col.transform, true);

            GetComponent<Rigidbody2D>().isKinematic = true;
        }
    }
}