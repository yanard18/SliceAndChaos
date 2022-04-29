using DenizYanar.SenseEngine;
using UnityEngine;

namespace DenizYanar.SenseEngine
{
[SenseEnginePath("GameObjects/Instantiate Object")]
    public class SenseInstantiateObject : Sense
    {
        [Header("Instantiate Settings")] 
        
        public GameObject InstantiateObject;


        public Transform InstantiateTransform;
        public Vector3 InstantiatePosition;

        public Quaternion InstantiateRotation;

        public bool MakeChildOfTransform;

        private void Awake()
        {
            Label = "Instantiate Object";
        }

        public override void Play()
        {
            if (InstantiateTransform != null)
                InstantiatePosition = InstantiateTransform.position;

            var obj = Instantiate(InstantiateObject, InstantiatePosition, InstantiateRotation);
            
            if(MakeChildOfTransform is true && InstantiateTransform != null)
                obj.transform.SetParent(InstantiateTransform);
        }
    }
}
