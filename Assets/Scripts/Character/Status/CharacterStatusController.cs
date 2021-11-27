using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BLUE.ProjectX.Character
{
    // TODO @BUGRA Enable/Disable Mechanism
    public class CharacterStatusController : MonoBehaviour
    {
        [SerializeField] private CharacterStatusBase[] _statuses = null;

        [SerializeField] private bool _isDebugEnabled = false;
        
        public List<ECharacterStatus> AppearedStatuses { get; private set; } = new List<ECharacterStatus>();

        private void LateUpdate()
        {
            ProcessCharacterStatuses();
        }

        private void ProcessCharacterStatuses()
        {
            List<ECharacterStatus> currentStatuses = new List<ECharacterStatus>();

            foreach (CharacterStatusBase status in _statuses)
            {
                bool statusResult = status.CheckStatus();

                if (statusResult)
                {
                    currentStatuses.Add(status.GetCharacterStatus());

                    if (status.BreakIfStatusAppears)
                    {
                        break;
                    }
                }
            }
            
            UpdateAppearedStatuses(currentStatuses);
        }

        private void UpdateAppearedStatuses(List<ECharacterStatus> currentStatuses)
        {
            List<ECharacterStatus> appearedStatuses = currentStatuses.Except(AppearedStatuses).ToList();
            
            List<ECharacterStatus> disappearedStatuses = AppearedStatuses.Except(currentStatuses).ToList();
            
            foreach (ECharacterStatus statusType in disappearedStatuses)
            {
                if (_isDebugEnabled)
                {
                    Debug.Log($"Character status `<color=red>{statusType}</color>` disappeared. @{Time.frameCount}");
                }

                CharacterStatusBase status = _statuses.SingleOrDefault(i => i.GetCharacterStatus() == statusType);
                
                status.StatusDisappeared();
            }

            foreach (ECharacterStatus statusType in appearedStatuses)
            {
                if (_isDebugEnabled)
                {
                    Debug.Log($"Character status `<color=green>{statusType}</color>` appeared.@{Time.frameCount}");
                }

                CharacterStatusBase status = _statuses.SingleOrDefault(i => i.GetCharacterStatus() == statusType);
                
                status.StatusAppeared();
            }

            AppearedStatuses = new List<ECharacterStatus>(currentStatuses);
        }
    }
}