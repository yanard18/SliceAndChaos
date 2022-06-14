using UnityEngine;

namespace DenizYanar.Movement
{
    public class HorizontalInstantMovement : IMove
    {
        private readonly Rigidbody2D m_Rb;
        private readonly float m_Speed;

        public HorizontalInstantMovement(Rigidbody2D rb, float speed)
        {
            m_Rb = rb;
            m_Speed = speed;
        }

        public void Move(Vector2 direction)
        {
            if (direction.x == 0) return;

            var xDir = Mathf.Sign(direction.x);

            m_Rb.velocity = new Vector2(xDir * m_Speed, m_Rb.velocity.y);
        }
    }
}