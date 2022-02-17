using UnityEngine;

namespace DenizYanar
{
    public class ApplicationManager : MonoBehaviour
    {
        [SerializeField] private int _targetFrameRate = 300;

        private void Awake()
        {
            Application.targetFrameRate = _targetFrameRate;
        }
    }
}
