using System.Collections;
using DenizYanar.FSM;
using UnityEngine;

namespace DenizYanar
{
    public class PlayerAttackSlashState : State
    {
        private float _startAngle;
        private float _direction;
        private readonly PlayerAttackController _player;
        private readonly GameObject _katana;

        public PlayerAttackSlashState(PlayerAttackController player, GameObject katana)
        {
            _player = player;
            _katana = katana;
        }
        
        public override void OnEnter()
        {
            base.OnEnter();
            _katana.SetActive(true);
            Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(_player.transform.position);
            _direction = Mathf.Sign(dir.x);
            _startAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            _startAngle += _direction > 0 ? 90 : -90;
            
            _katana.transform.rotation = Quaternion.AngleAxis(_startAngle, Vector3.forward);
            _katana.transform.localScale = _direction > 0 ? Vector3.one : new Vector3(1, -1, 1);

            _player.StartCoroutine(Slash(0.15f));
        }

        public override void OnExit()
        {
            base.OnExit();
            _player.IsSlashFinished = false;
            _katana.SetActive(false);
        }

        private IEnumerator Slash(float slashDuration)
        {
            var elapsedTime = 0f;
            var targetAngle = _direction > 0 ? _startAngle - 179 : _startAngle + 179;
            Quaternion startRotation = _katana.transform.rotation;
            while (elapsedTime < slashDuration)
            {
                elapsedTime += Time.deltaTime;
                _katana.transform.rotation = Quaternion.Lerp(startRotation, Quaternion.AngleAxis(targetAngle, Vector3.forward), elapsedTime / slashDuration);
                yield return null;
            }

            _player.IsSlashFinished = true;
            
        }
    }
}