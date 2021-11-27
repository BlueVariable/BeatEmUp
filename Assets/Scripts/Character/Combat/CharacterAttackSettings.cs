using System;
using UnityEngine;

namespace BLUE.ProjectX.Character
{
    [Serializable]
    public class CharacterAttackSettings
    {
        [SerializeField] private float _anticipationTime = 0.0f;
        public float AnticipationTime => _anticipationTime;

        [SerializeField] private float _attackTime = 0.0f;
        public float AttackTime => _attackTime;

        [SerializeField] private float _recoveryTime = 0.0f;
        public float RecoveryTime => _recoveryTime;
    }
}