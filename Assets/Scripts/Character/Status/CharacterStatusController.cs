using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BLUE.ProjectX.Character
{
    // TODO :: need enable disable mechanism
    public class CharacterStatusController : MonoBehaviour
    {
        [SerializeField] private CharacterStatusBase[] _statuses = null;

        [SerializeField] private bool _isDebugEnabled = false;
        
        public List<ECharacterStatus> AppearedStatuses { get; private set; } = new List<ECharacterStatus>();

        private void LateUpdate()
        {
            CheckCharacterStatuses();
        }

        private void CheckCharacterStatuses()
        {
            List<ECharacterStatus> currentStatuses = new List<ECharacterStatus>();

            foreach (CharacterStatusBase status in _statuses)
            {
                bool statusResult = status.CheckStatus();

                if (statusResult)
                {
                    currentStatuses.Add(status.GetCharacterStatus());

                    if (status.BreakIfStatusAppear)
                    {
                        break;
                    }
                }
            }
            
            UpdateAppearedStatuses(currentStatuses);
        }

        private void UpdateAppearedStatuses(List<ECharacterStatus> currentStatuses)
        {
            List<ECharacterStatus> appeared = currentStatuses.Except(AppearedStatuses).ToList();
            
            List<ECharacterStatus> disappeared = AppearedStatuses.Except(currentStatuses).ToList();
            
            foreach (ECharacterStatus eCharacterStatus in disappeared)
            {
                if (_isDebugEnabled)
                {
                    Debug.Log($"Character status disappeared <color=red>{eCharacterStatus}</color> @{Time.frameCount}.");
                }

                CharacterStatusBase status = _statuses.SingleOrDefault(i => i.GetCharacterStatus() == eCharacterStatus);
                
                status.DisappearStatus();
            }

            foreach (ECharacterStatus eCharacterStatus in appeared)
            {
                if (_isDebugEnabled)
                {
                    Debug.Log($"Character status appeared <color=green>{eCharacterStatus}</color> @{Time.frameCount}.");
                }

                CharacterStatusBase status = _statuses.SingleOrDefault(i => i.GetCharacterStatus() == eCharacterStatus);
                
                status.AppearStatus();
            }

            AppearedStatuses = new List<ECharacterStatus>(currentStatuses);
        }
    }
}