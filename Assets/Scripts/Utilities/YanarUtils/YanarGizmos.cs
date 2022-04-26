﻿using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;

namespace DenizYanar.YanarPro
{
    public static class YanarGizmos
    {
        public static void DebugDrawBox(Vector2 point, Vector2 size, float angle, Color color, float duration)
        {
            var orientation = Quaternion.Euler(0, 0, angle);

            // Basis vectors, half the size in each direction from the center.
            Vector2 right = orientation * Vector2.right * size.x / 2f;
            Vector2 up = orientation * Vector2.up * size.y / 2f;

            // Four box corners.
            var topLeft = point + up - right;
            var topRight = point + up + right;
            var bottomRight = point - up + right;
            var bottomLeft = point - up - right;

            // Now we've reduced the problem to drawing lines.
            Debug.DrawLine(topLeft, topRight, color, duration);
            Debug.DrawLine(topRight, bottomRight, color, duration);
            Debug.DrawLine(bottomRight, bottomLeft, color, duration);
            Debug.DrawLine(bottomLeft, topLeft, color, duration);
        }
        
        public static void DebugDrawFOV(Vector2 point, Vector2 size, float angle, Color color, float duration)
                {
                    var orientation = Quaternion.Euler(0, 0, angle);
        
                    // Basis vectors, half the size in each direction from the center.
                    Vector2 right = orientation * Vector2.right * size.x / 2f;
                    Vector2 up = orientation * Vector2.up * size.y / 2f;
        
                    // Four box corners.
                    var topLeft = point + up - right;
                    var topRight = point + up + right;
                    var bottomRight = point - up + right;
                    var bottomLeft = point - up - right;
        
                    // Now we've reduced the problem to drawing lines.
                    Debug.DrawLine(bottomRight, bottomLeft, color, duration);
                    Debug.DrawLine(bottomLeft, topLeft, color, duration);
                }

    }
}