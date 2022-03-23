using System.Collections;
using DenizYanar.Player;
using UnityEngine;
using DenizYanar.Projectiles;

namespace DenizYanar
{
    public class KatanaProjectile : Projectile
    {
        private MagnetController _magnet;
        
        [SerializeField] private float _turnBackDuration = 0.3f;

        #region Global Methods

        public void CallbackKatana() => StartCoroutine(KatanaCallback(_turnBackDuration));

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
            var magnet = GetComponentInChildren<MagnetController>();

            _magnet = magnet;
        }


        private void ConfigurePlayerInputForMagnet()
        {
            var magnetPlayerController = Author.GetComponent<PlayerMagnetInput>();
            magnetPlayerController.SetMagnetController(_magnet);
        }

        private IEnumerator KatanaCallback(float turnBackDuration)
        {
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

            var playerAttackController = Author.GetComponent<PlayerAttackController>();
            playerAttackController.IsSwordTurnedBack = true;
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