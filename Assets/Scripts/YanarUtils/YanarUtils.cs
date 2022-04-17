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

        public static float FindAngleBetweenMouseAndPosition(Vector2 pos)
        {
            if (Camera.main is null) return 0;

            var dir = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition) - pos;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            return angle;
        }

        public static Vector2 FindDirectionBetweenTwoPositions(Vector2 startPosition, Vector2 endPosition)
        {
            return endPosition - startPosition;
        }
        
        
        
        
        
        
        
        public class Sequence
        {
            public readonly Action Method;
            public readonly float RoutineDuration;
            
            public Sequence(Action method, float routineDuration)
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