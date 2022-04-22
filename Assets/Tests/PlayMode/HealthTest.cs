using System.Collections;
using NUnit.Framework;
using UnityEngine;
using DenizYanar.DamageAndHealthSystem;
using UnityEngine.TestTools;

public class HealthTest
{
    [UnityTest]
    public IEnumerator IsDeathEventRaised()
    {
        var bDeath = false;
        
        var obj = new GameObject();
        var health = obj.AddComponent<Health>();

        health.e_OnDeath += delegate { bDeath = true; };
        health.TakeDamage(new Damage(99999f, new GameObject("Enemy")));
        
        yield return null;
        
        Assert.AreEqual(bDeath, true);
        
    }

    [UnityTest]
    public IEnumerator IsTakeDamageEventRaised()
    {
        var bTakeDamage = false;

        var obj = new GameObject();
        var health = obj.AddComponent<Health>();

        health.e_OnDamage += delegate { bTakeDamage = true; };
        health.TakeDamage(new Damage(10f, new GameObject("Enemy")));

        yield return null;
        
        Assert.AreEqual(bTakeDamage, true);
    }
}
