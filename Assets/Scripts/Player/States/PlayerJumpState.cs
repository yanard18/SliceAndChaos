

using System.Collections;
using DenizYanar.Events;
using JetBrains.Annotations;
using UnityEngine;

namespace DenizYanar
{
    public class PlayerJumpState : State
    {
        private readonly JumpData _jumpData;
        private readonly Player _player;

        public PlayerJumpState(Player player, StringEventChannelSO nameInformerChannel = null, [CanBeNull] string stateName = null)
        {
            _player = player;
            _jumpData = player.JumpDataInstance;
            _stateName = stateName ?? GetType().Name;
            _stateNameInformerEventChannel = nameInformerChannel;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _jumpData.RB.velocity = new Vector2(_jumpData.RB.velocity.x, _jumpData.JumpForce);
            _jumpData.JumpCount--;
            _player.StartCoroutine(StartJumpCooldown(0.15f));
        }

        private IEnumerator StartJumpCooldown(float duration)
        {
            _jumpData.HasCooldown = true;
            yield return new WaitForSeconds(duration);
            _jumpData.HasCooldown = false;
        }

    }
}
