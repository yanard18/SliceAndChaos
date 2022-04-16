using System;
using DenizYanar.BodySystem;
using UnityEngine;

namespace DenizYanar.EnemySystem
{
    
    [RequireComponent(typeof(EnemyBehaviour), typeof(BodyController))]
    public class Enemy : MonoBehaviour
    {
        private BodyController _body;
        
        protected virtual void Awake()
        {
            _body = GetComponent<BodyController>();
        }

        protected virtual void OnEnable()
        {
            _body.OnBodyDestroyed += Death;
        }

        protected virtual void OnDisable()
        {
            _body.OnBodyDestroyed -= Death;
        }

        protected virtual void Death()
        {
            Debug.Log("Destroyed");
        }
    }
}
