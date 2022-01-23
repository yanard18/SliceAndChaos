using UnityEngine;

namespace DenizYanar
{
    [CreateAssetMenu(menuName = "Player Inputs")]
    public class PlayerInputs : ScriptableObject
    {
        public float HorizontalMovement;
        public bool Jump;
        public bool Shift;
        public bool Dive;
        public bool Attack1;
        public bool Attack2;

        public void ResetAllInputs()
        {
            Jump = false;
            Shift = false;
            Dive = false;
            Attack1 = false;
            Attack2 = false;
        }
    }
}
