using UnityEngine;
using System;

namespace DenizYanar.Guns
{
    public abstract class GunInputReader : MonoBehaviour
    {
        public event Action e_OnFireStarted;
        public event Action e_OnFireCancelled;
        public event Action e_OnReload;

        public void InvokeOnFireStarted() => e_OnFireStarted?.Invoke();
        public void InvokeOnFireCancelled() => e_OnFireCancelled?.Invoke();
        public void InvokeOnReload() => e_OnReload?.Invoke();
    }
}
