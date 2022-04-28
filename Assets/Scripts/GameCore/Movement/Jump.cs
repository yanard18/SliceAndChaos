using UnityEngine;
using System.Collections;

namespace GameCore.Movement
{
    public class Jump : IMove
    {
        private bool m_bHasJumpCooldown;
        private readonly Rigidbody2D m_Rb;
        private readonly float m_JumpForce;
        private readonly float m_CooldownDuration;
        private readonly MonoBehaviour m_Author;
        
        public Jump(Rigidbody2D rb, float jumpForce, float cooldownDuration, MonoBehaviour author)
        {
            m_Rb = rb;
            m_JumpForce = jumpForce;
            m_CooldownDuration = cooldownDuration;
            m_Author = author;
        }
        
        public void Move(Vector2 direction)
        {
            if(m_bHasJumpCooldown) return;
            m_Rb.AddForce(direction * m_JumpForce, ForceMode2D.Impulse);
            m_Author.StartCoroutine(StartJumpCooldown(m_CooldownDuration));
        }

        private IEnumerator StartJumpCooldown(float duration)
        {
            m_bHasJumpCooldown = true;
            yield return new WaitForSeconds(duration);
            m_bHasJumpCooldown = false;
        }
    }
}