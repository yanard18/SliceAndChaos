using UnityEngine;

public class ZombiePathFind : MonoBehaviour, IPathFind
{
    public Vector2 CalculateDirection(Vector2 targetPos)
    {
        var dir = targetPos - (Vector2) transform.position;
        dir.Normalize();
        return new Vector2(dir.x, 0);
    }
}
