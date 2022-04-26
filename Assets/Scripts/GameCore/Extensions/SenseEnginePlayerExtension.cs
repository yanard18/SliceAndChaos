using UnityEngine;

namespace DenizYanar.SenseEngine
{
    public static class SenseEnginePlayerExtension
    {
        public static void PlayIfExist(this SenseEnginePlayer sense)
        {
            if(sense != null)
                sense.Play();
        }
        public static void PlayHitSEP(this SenseEnginePlayer sense, Vector3 playerPosition, Vector2 attackDir)
        {
            var spawner = sense.GetComponent<SenseInstantiateObject>();
            spawner.InstantiatePosition = playerPosition + (Vector3) attackDir * -10f;
            var angle = Mathf.Atan2(attackDir.y, attackDir.x) * Mathf.Rad2Deg;
            spawner.InstantiateRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            sense.Play();
        }

        

    }
}
