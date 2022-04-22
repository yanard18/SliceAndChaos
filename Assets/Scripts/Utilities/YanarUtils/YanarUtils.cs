using System;
using System.Collections;
using System.Collections.Generic;
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

        public static float FindAngleBetweenPositionAndMouse(Vector2 startPos, Vector2 mousePos)
        {
            if (Camera.main == null) return 0f;

            var dir = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition) - startPos;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            return angle;
        }

        public static Vector2 FindDirectionBetweenPositionAndMouse(Vector2 startPos, Vector2 mousePos)
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

        public static Vector2 FindDirectionBetweenTwoPositions(Vector2 startPosition, Vector2 endPosition)
        {
            return (endPosition - startPosition).normalized;
        }

        public static Vector2 FindDisplacementBetweenTwoPosition(Vector2 startPosition, Vector2 endPosition)
        {
            return endPosition - startPosition;
        }


        public class Sequence
        {
            public readonly Action Method;
            public readonly float RoutineDuration;

            protected Sequence(Action method, float routineDuration)
            {
                Method = method;
                RoutineDuration = routineDuration;
            }
        }

        public class SequenceQueue
        {
            private readonly Queue<Sequence> _sequences = new();

            public IEnumerator Play()
            {
                while (_sequences.Count > 0)
                {
                    var sequence = _sequences.Dequeue();
                    sequence.Method.Invoke();
                    yield return new WaitForSeconds(sequence.RoutineDuration);
                }
            }

            public void Add(Sequence sequence)
            {
                _sequences.Enqueue(sequence);
            }
        }
    }
}