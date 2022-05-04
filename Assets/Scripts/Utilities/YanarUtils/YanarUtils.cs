using System.Collections.Generic;
using UnityEngine;

namespace DenizYanar.YanarPro
{
    public static class YanarUtils
    {
        public static bool IsMainCameraExist()
        {
            return Camera.main != null;
        }

        public static float FindAngleBetweenTwoPositions(Vector2 pos1, Vector2 pos2)
        {
            var dir = pos2 - pos1;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            return angle;
        }

        public static float FindAngleBetweenPositionAndMouse(Vector2 startPos, Vector2 mousePos)
        {
            if (Camera.main == null) return 0f;

            var dir = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition) - startPos;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            return angle;
        }

        public static Vector2 FindDirectionToMouse(Vector2 startPos, Vector2 mousePos)
        {
            if (Camera.main == null) return Vector2.zero;

            var dir = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition) - startPos;
            return dir.normalized;
        }

        public static Vector2 FindDisplacementBetweenPositionAndScreen(Vector2 startPos, Vector2 screenPos)
        {
            if (Camera.main == null) return Vector2.zero;

            var dir = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition) - startPos;
            return dir;
        }

        public static Vector2 Direction(Vector2 startPosition, Vector2 endPosition)
        {
            return (endPosition - startPosition).normalized;
        }

        public static Vector2 FindDisplacementBetweenTwoPosition(Vector2 startPosition, Vector2 endPosition)
        {
            return endPosition - startPosition;
        }

        public static Collider2D FindClosestTarget(Vector2 positionToCompare, IEnumerable<Collider2D> TTargets)
        {
            var closestDistance = Mathf.Infinity;
            var closestTarget = new Collider2D();
            foreach (var target in TTargets)
            {
                if (target == null) continue;
                var distance = Vector2.Distance(positionToCompare, target.transform.root.position);
                if (!(distance < closestDistance)) continue;
                closestDistance = distance;
                closestTarget = target;
            }

            return closestTarget;
        }
    }
}