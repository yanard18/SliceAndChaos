using UnityEngine;

namespace DenizYanar.Movement
{
 public class HorizontalPhysicMovement : IMove
 {
     private readonly Rigidbody2D m_Rb;
     private readonly float m_MaxVelocity;
     private readonly float m_Acceleration;
 
     public HorizontalPhysicMovement(Rigidbody2D rb, float maxVelocity, float acceleration)
     {
         m_Rb = rb;
         m_MaxVelocity = maxVelocity;
         m_Acceleration = acceleration;
     }
     public void Move(Vector2 direction)
     {
         if (direction.x == 0) return;
         
         var xDir = Mathf.Sign(direction.x);
 
         switch (xDir)
         {
             //+x
             case 1:
             {
                 if (m_Rb.velocity.x < m_MaxVelocity)
                     m_Rb.AddForce(new Vector2(m_Acceleration, 0), ForceMode2D.Force);
                 break;
             }
             //-x
             case -1:
             {
                 if (m_Rb.velocity.x > -m_MaxVelocity)
                     m_Rb.AddForce(new Vector2(-m_Acceleration, 0), ForceMode2D.Force);
                 break;
             }
         }
     }
 }   
}
