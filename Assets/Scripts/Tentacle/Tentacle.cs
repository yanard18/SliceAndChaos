using UnityEngine;

namespace DenizYanar.Tentacles
{
    public class Tentacle : MonoBehaviour
    {
        private Vector3[] _segmentVelocity;
        
        
        [SerializeField] private int _length;
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private Vector3[] _segmentPoses;
        
    
        [SerializeField] private Transform _targetDir;
        [SerializeField] private float _targetDist;
        [SerializeField] private float _smoothSpeed;
        [SerializeField] private float _trailSpeed;

        [SerializeField] private float _wiggleFrequency;
        [SerializeField] private float _wiggleMagnitude;
        [SerializeField] private GameObject[] _bodyParts;
        


        private void Start()
        {
            _lineRenderer.positionCount = _length;
            _segmentPoses = new Vector3[_length];
            _segmentVelocity = new Vector3[_length];
        }

        private void Update()
        {

            _targetDir.localRotation =
                Quaternion.Euler(0, 0, Mathf.Sin(Time.time * _wiggleFrequency) * _wiggleMagnitude);
            
            _segmentPoses[0] = _targetDir.position;

            for (var i = 1; i < _segmentPoses.Length; i++)
            {
                _segmentPoses[i] = Vector3.SmoothDamp(_segmentPoses[i],
                    _segmentPoses[i - 1] + _targetDir.right * _targetDist, ref _segmentVelocity[i], _smoothSpeed + i / _trailSpeed);
                _bodyParts[i - 1].transform.position = _segmentPoses[i];
            }
            
            _lineRenderer.SetPositions(_segmentPoses);
        }
    }
}
