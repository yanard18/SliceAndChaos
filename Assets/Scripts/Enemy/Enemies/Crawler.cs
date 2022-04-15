using DenizYanar.PlayerSystem;
using UnityEngine;

namespace DenizYanar
{
    [RequireComponent(typeof(CrawlerBehaviour))]
    public class Crawler : MonoBehaviour
    {
        private CrawlerBehaviour _behaviour;
        private Rigidbody2D _rb;


        private void Awake()
        {
            _behaviour = GetComponent<CrawlerBehaviour>();
            _rb = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            _behaviour.OnAttack += Jump;
        }

        private void OnDisable()
        {
            _behaviour.OnAttack -= Jump;
        }

        private void Jump()
        {
            var p = GameObject.FindObjectOfType<Player>();

            Vector2 dir = p.transform.position - transform.position;
            dir = Vector2.ClampMagnitude(dir, 10f);
            
            var jumpDirection = new Vector2(dir.normalized.x * 20f, 15f);
            
            

            _rb.AddForce(jumpDirection, ForceMode2D.Impulse);
        }
    }
}