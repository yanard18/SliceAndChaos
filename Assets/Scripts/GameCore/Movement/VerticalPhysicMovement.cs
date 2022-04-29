using DenizYanar.Movement;
using UnityEngine;

public class VerticalPhysicMovement : IMove 
{
    private readonly Rigidbody2D m_Rb;
    private readonly float m_MaxVelocity;
    private readonly float m_Acceleration;

    public VerticalPhysicMovement(Rigidbody2D rb, float maxVelocity, float acceleration)
    {
        m_Rb = rb;
        m_MaxVelocity = maxVelocity;
        m_Acceleration = acceleration;
    }

    public void Move(Vector2 direction)
    {
        if(direction.y == 0) return;
        
        var yDir = Mathf.Sign(direction.y);
        
        switch (yDir)
        {
            //+y
            case 1:
            {
                if (m_Rb.velocity.y < m_MaxVelocity)
                    m_Rb.AddForce(new Vector2(0, m_Acceleration), ForceMode2D.Force);
                break;
            }
            //-y
            case -1:
            {
                if (m_Rb.velocity.y > -m_MaxVelocity)
                    m_Rb.AddForce(new Vector2(0, -m_Acceleration), ForceMode2D.Force);
                break;
            }
        }
    }
}