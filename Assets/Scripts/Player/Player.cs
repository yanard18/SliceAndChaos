using System;
using UnityEngine;

namespace DenizYanar
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private Collider2D _collider;

        [SerializeField] private PlayerSettings _settings;

        [SerializeField] private PlayerInput _input;


        private StateMachine _stateMachine;

        private void Awake()
        {
            _collider = GetComponentInChildren<Collider2D>();
            _rb = GetComponent<Rigidbody2D>();
            
            _stateMachine = new StateMachine();

            PlayerIdleState idle = new PlayerIdleState(_rb);
            PlayerMoveState move = new PlayerMoveState(_input, _rb);
            PlayerJumpState jump = new PlayerJumpState(_rb, 20.0f);
            PlayerFallState fall = new PlayerFallState();
            PlayerLandState land = new PlayerLandState();
            PlayerAirStrafeState airStrafe = new PlayerAirStrafeState(_rb, _input);

            _stateMachine.InitState(idle);

            To(idle, move, HasMovementInput());
            To(move, idle, HasNotMovementInput());
            To(idle, jump, IsJumpTriggered());
            To(move, jump, IsJumpTriggered());
            To(idle, fall, IsFalling());
            To(move, fall, IsFalling());
            To(jump, fall, IsFalling()); 
            To(fall, land, OnContactToGround());
            To(land, idle, HasNotMovementInput());
            To(land, move, HasMovementInput());
            To(fall,airStrafe,HasMovementInput());
            To(airStrafe, fall, HasNotMovementInput());
            To(airStrafe, land, OnContactToGround());
            To(jump, airStrafe, HasMovementInput());

            void To(State from, State to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);
            

            Func<bool> HasMovementInput() => () => Mathf.Abs(_input.HorizontalMovement) > 0;
            Func<bool> HasNotMovementInput() => () => _input.HorizontalMovement == 0;
            Func<bool> IsJumpTriggered() => () =>  _input.Jump.started;
            Func<bool> IsFalling() => () => _rb.velocity.y < 0;
            Func<bool> OnContactToGround() => () => IsTouchingToGround() != null;
            
        }

        private void Update()
        {
            _stateMachine.Tick();
        }

        private void FixedUpdate()
        {
            _stateMachine.PhysicsTick();
        }
        
        private float? IsTouchingToGround()
        {
            Bounds bounds = _collider.bounds;
            var spaceBetweenRays = bounds.size.x;
            Vector2 bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
            for (var i = 0; i < 2; i++)
            {
                RaycastHit2D hit = Physics2D.Raycast(
                    bottomLeft + Vector2.right * (spaceBetweenRays * i), 
                    Vector2.down, 0.1f, _settings.ObstacleLayerMask);

                if (hit)
                    return Vector2.Angle(hit.normal, Vector2.up) % 90f;
            }

            return null;
        }

        
    }
}
