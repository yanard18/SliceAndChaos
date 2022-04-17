using UnityEngine;

namespace DenizYanar.YanarPro
{
    public static class YanarUtils
    {
        public static float FindAngleBetweenTwoPositions(Vector2 pos1, Vector2 pos2)
        {
            var dir = pos2 - pos1;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            return angle;
        }

        public static float FindAngleBetweenMouseAndPosition(Vector2 pos)
        {
            if (Camera.main is null) return 0;

            var dir = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition) - pos;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            return angle;
        }

        public static Vector2 FindDirectionBetweenTwoPositions(Vector2 pos1, Vector2 pos2)
        {
            return pos2 - pos1;
        }
    }
}