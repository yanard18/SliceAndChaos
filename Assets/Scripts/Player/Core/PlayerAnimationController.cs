using UnityEngine;

namespace DenizYanar.PlayerSystem
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] private GameObject _playerBody;
        
        private Animator _animator;
        private Rigidbody2D _rb;
        private static readonly int Speed = Animator.StringToHash("Speed");

        private bool _lookingRight;
        private readonly Quaternion _left = Quaternion.Euler(Vector3.up * 180f);
        private readonly Quaternion _right = Quaternion.Euler(Vector3.zero);

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            HandleAnimator();
            HandleDirection();
        }

        private void HandleAnimator()
        {
            _animator.SetFloat(Speed, Mathf.Abs(_rb.velocity.x));
        }
        private void HandleDirection()
        {
            if (_lookingRight)
            {
                if(_rb.velocity.x < 0)
                    TurnLeft();
            }
            else
            {
                if(_rb.velocity.x > 0)
                    TurnRight();
            }
        }

        private void TurnRight()
        {
            _playerBody.transform.rotation = _right;
            _lookingRight = true;
        }

        private void TurnLeft()
        {
            _playerBody.transform.rotation = _left;
            _lookingRight = false;
        }
    }
}
