using UnityEngine;

namespace Extensions
{
    public static class Vector2Extension
    {
        public static Vector2 Direction(this Vector2 v0, Vector2 targetPos)
        {
            var dir = targetPos - v0;
            return dir;
        }


        public static Vector2 NormalizedDirection(this Vector2 startPos, Vector2 targetPos)
        {
            var dir = targetPos - startPos;
            dir.Normalize();
            return dir;
        }

        public static Vector3 Direction(this Vector3 v0, Vector3 targetPos)
        {
            var dir = targetPos - v0;
            return dir;
        }
        
        
    }
}