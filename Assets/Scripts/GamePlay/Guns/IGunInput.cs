using UnityEngine;
using System;
using UnityEngine.UIElements;

namespace DenizYanar.Guns
{
    public interface IGunInput
    {
        void Fire();
        void Reload();
    }
}
