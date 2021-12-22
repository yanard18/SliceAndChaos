using UnityEngine;

namespace DenizYanar
{
    public class JumpData
    {
        private readonly int _maxJumpCount;
        
        
        public int JumpCount;
        public bool HasCooldown = false;
        public readonly float JumpForce;
        public readonly Rigidbody2D RB;

        public bool CanJump => JumpCount > 0 && HasCooldown == false;

        public void ResetJumpCount() => JumpCount = _maxJumpCount;

        public JumpData(int maxJumpCount, float jumpForce, Rigidbody2D rb)
        {
            _maxJumpCount = maxJumpCount;
            JumpCount = _maxJumpCount;
            JumpForce = jumpForce;
            RB = rb;
        }

    }
}