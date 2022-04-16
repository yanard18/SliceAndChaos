using System;
using System.Collections.Generic;
using DenizYanar.Core;
using UnityEngine;

namespace DenizYanar.BodySystem
{
    public class BodyController : MonoBehaviour
    {
        private readonly Dictionary<string, BodyPart> _bodyPartsDictionary = new();


        [SerializeField] private BodyPart[] _bodyParts;

        private BodyPart _rootBodyPart;

        public event Action OnBodyDestroyed;




        private void Awake()
        {
            foreach (var part in _bodyParts)
            {
                if(!_bodyPartsDictionary.ContainsKey(part.PartName)) 
                    _bodyPartsDictionary.Add(part.PartName, part);

                if (!part.IsRoot) continue;
                
                if(_rootBodyPart == null) 
                    _rootBodyPart = part;
                else
                {
                    Debug.LogWarning("There can be only one root body part");
                }

            }
        }

        private void OnEnable()
        {
            foreach (var part in _bodyParts)
                part.OnDestroyed += OnBodyPartDestroyed;
        }

        private void OnDisable()
        {
            foreach (var part in _bodyParts)
                part.OnDestroyed -= OnBodyPartDestroyed;
        }

        private void OnBodyPartDestroyed(BodyPart part, Damage damage)
        {
            if(part.IsRoot) OnBodyDestroyed?.Invoke();
        }
    }
}
