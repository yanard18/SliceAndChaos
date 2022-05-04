using DenizYanar.Movement;
using UnityEngine;

public class Stop : IMove
{
    private readonly Rigidbody2D m_Rb;

    public Stop(Rigidbody2D rb)
    {
        m_Rb = rb;
    }
    public void Move(Vector2 direction) => m_Rb.velocity = new Vector2(0f, m_Rb.velocity.y);
}
