using DenizYanar.Core;
using DenizYanar.Events;
using DenizYanar.External.Sense_Engine.Scripts.Core;
using UnityEngine;

namespace DenizYanar.PlayerSystem
{
    [RequireComponent(typeof(Health))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private SenseEnginePlayer _deathSense;
        [SerializeField] private VoidEventChannelSO _gameOverEvent;

        
        /*
        protected override void Death(Damage damage)
        {
            Debug.Log("Player killed by " + damage.Author.name);
            _deathSense.Play();
            _gameOverEvent.Invoke();
            Destroy(this.gameObject);
        }*/
    }
}
