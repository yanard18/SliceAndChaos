using DenizYanar.Core;
using DenizYanar.DamageAndHealthSystem;
using DenizYanar.EnemySystem;
using DenizYanar.PlayerSystem;
using GameCore;
using GameCore.Movement;
using UnityEngine;

public class Zombie : Enemy
{

    private bool m_bHasJumpCooldown;
    
    private IPathFind m_iPathFind;

    private IMove m_iMovement;
    private IMove m_iJump;

    private Rigidbody2D m_Rb;
    private GroundDetection m_GroundDetection;
    private WallDetection m_WallDetection;

    [SerializeField]
    private LayerMask m_ObstacleLayer;

    [SerializeField]
    private Collider2D m_Collider2D;
    

    protected override void Awake()
    {
        base.Awake();
        m_Rb = GetComponent<Rigidbody2D>();
        m_iPathFind = GetComponent<IPathFind>();
        m_iMovement = new HorizontalPhysicMovement(m_Rb, 5, 5);
        m_GroundDetection =
            new GroundDetection(m_Collider2D,2, m_ObstacleLayer);
        m_WallDetection = new WallDetection(m_Collider2D, 2, m_ObstacleLayer);
        m_iJump = new Jump(m_Rb, 10f, 2f, this);
    }

    private void Update()
    {
        if(Player.s_Instance == null) return;

        var direction = m_iPathFind.CalculateDirection(Player.s_Instance.transform.position);
        m_iMovement.Move(direction);
        if(m_GroundDetection.IsTouchingToGround() && m_WallDetection.DetectWall(direction))
            m_iJump.Move(Vector2.up);
        
        
    }

    protected override void EOnDeath(Damage damage)
    {
        throw new System.NotImplementedException();
    }

    protected override void EOnTakeDamage(Damage damage)
    {
        throw new System.NotImplementedException();
    }
}
