using System.Collections;
using UnityEngine;

namespace DenizYanar
{
    public class WallSlideData
    {
        public bool HasCooldown = false;
        public readonly Rigidbody2D RB;
        public readonly Collider2D Collider;

        public WallSlideData(Rigidbody2D rb, Collider2D collider)
        {
            RB = rb;
            Collider = collider;
        }
        
        public IEnumerator StartCooldown(float duration)
        {
            HasCooldown = true;
            yield return new WaitForSeconds(duration);
            HasCooldown = false;
        }
    }
}